from __future__ import print_function
from __future__ import division

import argparse
import random
import torch
import torch.backends.cudnn as cudnn
import torch.optim as optim
import torch.utils.data
from torch.autograd import Variable
import numpy as np
# from warpctc_pytorch import CTCLoss
from torch.nn import CTCLoss
# from torch.nn.functional import ctc_loss
import os
import utils
import dataset
from pathlib import Path
import models.crnn as net
import params
import time
from tensorboardX import SummaryWriter
from itertools import cycle
from tool import my_transform
from torchvision import transforms

parser = argparse.ArgumentParser()
# parser.add_argument('-train', '--trainroot', required=True, help='path to train dataset')
parser.add_argument('-train', '--trainroot', help='path to train dataset', default="dataset/2k")
# parser.add_argument('-train2', '--trainroot2', help='path to train dataset', default="dataset/train_2k")
# parser.add_argument('-val', '--valroot', required=True, help='path to val dataset')
parser.add_argument('-val', '--valroot', help='path to val dataset', default="dataset/2k")
# parser.add_argument('-val2', '--valroot2', help='path to val dataset', default="dataset/test_2k")
args = parser.parse_args()

if not os.path.exists(params.expr_dir):
    os.makedirs(params.expr_dir)

# ensure everytime the random is the same
random.seed(params.manualSeed)
np.random.seed(params.manualSeed)
torch.manual_seed(params.manualSeed)

cudnn.benchmark = True

if torch.cuda.is_available() and not params.cuda:
    print("WARNING: You have a CUDA device, so you should probably set cuda in params.py to True")

# *************************************************
"""
数据增强
"""
transforms0 = transforms.Compose([
        ])

transforms1 = transforms.Compose([
    transforms.RandomRotation(1.5),
])
transforms2 = transforms.Compose([
    my_transform.AddSaltPepperNoise(0.05)
])

transforms3 = transforms.Compose([
    my_transform.AddGaussianNoise(mean=0, variance=1, amplitude=20)
])

trans = [transforms0, transforms0, transforms0, transforms1, transforms2, transforms3]

# -----------------------------------------------
"""
In this block
    Get train and val data_loader
"""
def data_loader():
    # train
    train_dataset = dataset.lmdbDataset(root=args.trainroot, trans=trans)
    # train_dataset_360 = dataset.lmdbDataset(root=args.trainroot)
    # assert train_dataset_360
    # train_dataset_2k = dataset.lmdbDataset(root=args.trainroot2)
    # assert train_dataset_2k

    if not params.random_sample:
        sampler = dataset.randomSequentialSampler(train_dataset, params.batchSize)
        # sampler = dataset.randomSequentialSampler(train_dataset_360, params.batchSize)
        # sampler = dataset.randomSequentialSampler(train_dataset_2k, params.batchSize)
    else:
        sampler = None
    train_loader = torch.utils.data.DataLoader(train_dataset, batch_size=params.batchSize, \
            shuffle=True, sampler=sampler, num_workers=int(params.workers), \
            collate_fn=dataset.alignCollate(imgH=params.imgH, imgW=params.imgW, keep_ratio=params.keep_ratio))

    # train_loader_360 = torch.utils.data.DataLoader(train_dataset_360, batch_size=params.batchSize, \
    #                                            shuffle=True, sampler=sampler, num_workers=int(params.workers), \
    #                                            collate_fn=dataset.alignCollate(imgH=params.imgH, imgW=params.imgW,
    #                                                                            keep_ratio=params.keep_ratio))
    #
    # train_loader_2k = torch.utils.data.DataLoader(train_dataset_2k, batch_size=params.batchSize, \
    #                                            shuffle=True, sampler=sampler, num_workers=int(params.workers), \
    #                                            collate_fn=dataset.alignCollate(imgH=params.imgH, imgW=params.imgW,
    #                                                                            keep_ratio=params.keep_ratio))
    
    # val
    val_dataset = dataset.lmdbDataset(root=args.valroot, transform=dataset.resizeNormalize((params.imgW, params.imgH)))
    assert val_dataset
    # val_dataset_360 = dataset.lmdbDataset(root=args.valroot, transform=dataset.resizeNormalize((params.imgW, params.imgH)))
    # assert val_dataset_360
    # val_dataset_2k = dataset.lmdbDataset(root=args.valroot2, transform=dataset.resizeNormalize((params.imgW, params.imgH)))
    # assert val_dataset_2k

    val_loader = torch.utils.data.DataLoader(val_dataset, shuffle=True, batch_size=params.batchSize, num_workers=int(params.workers))
    # val_loader_360 = torch.utils.data.DataLoader(val_dataset_360, shuffle=True, batch_size=params.batchSize, num_workers=int(params.workers))
    # val_loader_2k = torch.utils.data.DataLoader(val_dataset_2k, shuffle=True, batch_size=params.batchSize, num_workers=int(params.workers))

    return train_loader, val_loader
    # return train_loader_360, train_loader_2k, val_loader_360, val_loader_2k

train_loader, val_loader = data_loader()
# train_loader_360, train_loader_2k, val_loader_360, val_loader_2k = data_loader()

# -----------------------------------------------
"""
In this block
    Net init
    Weight init
    Load pretrained model
"""
def weights_init(m):
    classname = m.__class__.__name__
    if classname.find('Conv') != -1:
        m.weight.data.normal_(0.0, 0.02)
    elif classname.find('BatchNorm') != -1:
        m.weight.data.normal_(1.0, 0.02)
        m.bias.data.fill_(0)

def net_init():
    nclass = len(params.alphabet) + 1
    crnn = net.CRNN(params.imgH, params.nc, nclass, params.nh)
    crnn.apply(weights_init)
    if params.pretrained != '':
        print('loading pretrained model from %s' % params.pretrained)
        if params.multi_gpu:
            crnn = torch.nn.DataParallel(crnn)
        crnn.load_state_dict(torch.load(params.pretrained))
    
    return crnn

crnn = net_init()
print(crnn)

# -----------------------------------------------
"""
In this block
    Init some utils defined in utils.py
"""
# Compute average for `torch.Variable` and `torch.Tensor`.
loss_avg = utils.averager()

# Convert between str and label.
converter = utils.strLabelConverter(params.alphabet)

# -----------------------------------------------
"""
In this block
    criterion define
"""
criterion = CTCLoss()

# -----------------------------------------------
"""
In this block
    Init some tensor
    Put tensor and net on cuda
    NOTE:
        image, text, length is used by both val and train
        becaues train and val will never use it at the same time.
"""
image = torch.FloatTensor(params.batchSize, 3, params.imgH, params.imgH)
text = torch.LongTensor(params.batchSize * 5)
length = torch.LongTensor(params.batchSize)

if params.cuda and torch.cuda.is_available():
    criterion = criterion.cuda()
    image = image.cuda()
    text = text.cuda()

    crnn = crnn.cuda()
    if params.multi_gpu:
        crnn = torch.nn.DataParallel(crnn, device_ids=range(params.ngpu))

image = Variable(image)
text = Variable(text)
length = Variable(length)

# -----------------------------------------------
"""
In this block
    Setup optimizer
"""
if params.adam:
    optimizer = optim.Adam(crnn.parameters(), lr=params.lr, betas=(params.beta1, 0.999))
elif params.adadelta:
    optimizer = optim.Adadelta(crnn.parameters())
else:
    optimizer = optim.RMSprop(crnn.parameters(), lr=params.lr)

# -----------------------------------------------
"""
In this block
    Dealwith lossnan
    NOTE:
        I use different way to dealwith loss nan according to the torch version. 
"""
if params.dealwith_lossnan:
    if torch.__version__ >= '1.1.0':
        """
        zero_infinity (bool, optional):
            Whether to zero infinite losses and the associated gradients.
            Default: ``False``
            Infinite losses mainly occur when the inputs are too short
            to be aligned to the targets.
        Pytorch add this param after v1.1.0 
        """
        criterion = CTCLoss(zero_infinity = True)
        # criterion = CTCLoss(zero_infinity = False)
    else:
        """
        only when
            torch.__version__ < '1.1.0'
        we use this way to change the inf to zero
        """
        crnn.register_backward_hook(crnn.backward_hook)

# -----------------------------------------------

def val(net, criterion):
    print('Start val')

    for p in crnn.parameters():
        p.requires_grad = False

    net.eval()
    val_iter = iter(val_loader)

    i = 0
    n_correct = 0
    loss_avg = utils.averager() # The blobal loss_avg is used by train

    max_iter = len(val_loader)

    for i in range(max_iter):

        print(f'{i}/{max_iter}:<---')

        data = val_iter.next()
        i += 1
        cpu_images, cpu_texts = data
        batch_size = cpu_images.size(0)
        utils.loadData(image, cpu_images)
        t, l = converter.encode(cpu_texts)
        utils.loadData(text, t)
        utils.loadData(length, l)

        preds = crnn(image)
        # print(f"preds:{preds}")
        preds_size = Variable(torch.LongTensor([preds.size(0)] * batch_size))
        # print(f"preds_size:{preds_size}")
        # cost = criterion(preds, text, preds_size, length) / batch_size
        cost = criterion(preds, text, preds_size, length)
        # print(f"cost:{cost}")
        loss_avg.add(cost)

        _, preds = preds.max(2)
        preds = preds.transpose(1, 0).contiguous().view(-1)
        # print(f"preds:{preds}")
        # print(f"preds.data:{preds.data}")
        # print(f"preds_size.data:{preds_size.data}")
        sim_preds = converter.decode(preds.data, preds_size.data, raw=False)
        # print(f"sim_preds:{sim_preds}")
        cpu_texts_decode = []
        for i in cpu_texts:
            cpu_texts_decode.append(i.decode('utf-8', 'strict'))
        for pred, target in zip(sim_preds, cpu_texts_decode):
            if pred == target:
                n_correct += 1

    raw_preds = converter.decode(preds.data, preds_size.data, raw=True)[:params.n_val_disp]
    # print(f"raw_preds:{raw_preds}")
    # print(f"sim_preds:{sim_preds}")
    # print(f"cpu_texts_decode:{cpu_texts_decode}")
    for raw_pred, pred, gt in zip(raw_preds, sim_preds, cpu_texts_decode):
        print('%-20s => %-20s, gt: %-20s' % (raw_pred, pred, gt))

    accuracy = n_correct / float(max_iter * params.batchSize)
    print(float(max_iter * params.batchSize))
    print('Val loss: %f, accuray: %f' % (loss_avg.val(), accuracy))
    return loss_avg.val(), accuracy


def val_with_2k(net, criterion):
    print('Start val')

    for p in crnn.parameters():
        p.requires_grad = False

    net.eval()

    n_correct = 0
    loss_avg = utils.averager() # The blobal loss_avg is used by train

    max_iter = len(val_loader_360)

    for i, data in enumerate(zip(val_loader_360, cycle(val_loader_2k))):

        print(f'{i}/{max_iter}:<---')

        data_360 = data[0]
        data_2k = data[1]

        cpu_images_360, cpu_texts_360 = data_360
        cpu_images_2k, cpu_texts_2k = data_2k

        cpu_images = torch.cat([cpu_images_360, cpu_images_2k], dim=0)
        cpu_texts = cpu_texts_360 + cpu_texts_2k

        batch_size = cpu_images.size(0)
        utils.loadData(image, cpu_images)
        t, l = converter.encode(cpu_texts)
        utils.loadData(text, t)
        utils.loadData(length, l)

        preds = crnn(image)
        # print(f"preds:{preds}")
        preds_size = Variable(torch.LongTensor([preds.size(0)] * batch_size))
        # print(f"preds_size:{preds_size}")
        cost = criterion(preds, text, preds_size, length) / batch_size
        # print(f"cost:{cost}")
        loss_avg.add(cost)

        _, preds = preds.max(2)
        preds = preds.transpose(1, 0).contiguous().view(-1)
        # print(f"preds:{preds}")
        # print(f"preds.data:{preds.data}")
        # print(f"preds_size.data:{preds_size.data}")
        sim_preds = converter.decode(preds.data, preds_size.data, raw=False)
        # print(f"sim_preds:{sim_preds}")
        cpu_texts_decode = []
        for i in cpu_texts:
            cpu_texts_decode.append(i.decode('utf-8', 'strict'))
        for pred, target in zip(sim_preds, cpu_texts_decode):
            if pred == target:
                n_correct += 1

    raw_preds = converter.decode(preds.data, preds_size.data, raw=True)[:params.n_val_disp]
    # print(f"raw_preds:{raw_preds}")
    # print(f"sim_preds:{sim_preds}")
    # print(f"cpu_texts_decode:{cpu_texts_decode}")
    for raw_pred, pred, gt in zip(raw_preds, sim_preds, cpu_texts_decode):
        print('%-20s => %-20s, gt: %-20s' % (raw_pred, pred, gt))

    accuracy = n_correct / float(max_iter * params.batchSize)
    print('Val loss: %f, accuray: %f' % (loss_avg.val(), accuracy))
    return loss_avg.val(), accuracy


def train(net, criterion, optimizer, train_iter):
    for p in crnn.parameters():
        p.requires_grad = True
    crnn.train()

    data = train_iter.next()
    cpu_images, cpu_texts = data
    batch_size = cpu_images.size(0)
    utils.loadData(image, cpu_images)
    t, l = converter.encode(cpu_texts)
    # global text, length
    utils.loadData(text, t)
    utils.loadData(length, l)
    
    optimizer.zero_grad()
    preds = crnn(image)

    preds = preds.log_softmax(2)
    # print(f"preds:{preds}")
    # print(f"labels:{text}")
    # text = text.cuda()
    # length = length.cuda()

    preds_size = Variable(torch.LongTensor([preds.size(0)] * batch_size))
    # preds_size = preds_size.cuda()
    # cost = criterion(preds, text, preds_size, length) / batch_size
    cost = criterion(preds, text, preds_size, length)
    # crnn.zero_grad()
    cost.backward()
    optimizer.step()
    return cost


def train_with_2k(net, criterion, optimizer, data):
    for p in crnn.parameters():
        p.requires_grad = True
    crnn.train()

    data_360cc = data[0]
    data_2k = data[1]

    # cpu_images, cpu_texts = data
    cpu_images_360, cpu_texts_360 = data_360cc
    cpu_images_2k, cpu_texts_2k = data_2k

    cpu_images = torch.cat([cpu_images_360, cpu_images_2k], dim=0)
    cpu_texts = cpu_texts_360 + cpu_texts_2k

    batch_size = cpu_images.size(0)
    utils.loadData(image, cpu_images)
    t, l = converter.encode(cpu_texts)
    # global text, length
    utils.loadData(text, t)
    utils.loadData(length, l)

    optimizer.zero_grad()
    preds = crnn(image)

    preds = preds.log_softmax(2)
    # print(f"preds:{preds}")
    # print(f"labels:{text}")
    # text = text.cuda()
    # length = length.cuda()

    preds_size = Variable(torch.LongTensor([preds.size(0)] * batch_size))
    # preds_size = preds_size.cuda()
    cost = criterion(preds, text, preds_size, length)
    # crnn.zero_grad()
    cost.backward()
    optimizer.step()
    return cost


# if __name__ == "__main__":
#     min_loss = 1000000
#
#     root_output_dir = Path(params.output_dir)
#     time_str = time.strftime('%Y-%m-%d-%H-%M')
#     tensorboard_log_dir = root_output_dir / time_str / 'log'
#
#     # writer dict
#     writer_dict = {
#         'writer': SummaryWriter(log_dir=tensorboard_log_dir),
#         'train_global_steps': 0,
#         'valid_global_steps': 0,
#     }
#
#     best_acc = 0.5
#     is_best = False
#
#     for epoch in range(params.nepoch):
#
#         for i, data in enumerate(zip(train_loader_360, cycle(train_loader_2k))):
#             cost = train_with_2k(crnn, criterion, optimizer, data)
#             loss_avg.add(cost)
#
#
#             if i % params.displayInterval == 0:
#                 print('[%d/%d][%d/%d] Loss: %f' %
#                       (epoch, params.nepoch, i, len(train_loader_360), loss_avg.val()))
#                 # print(loss_avg)
#
#                 writer = writer_dict['writer']
#                 global_steps = writer_dict['train_global_steps']
#                 writer.add_scalar('train_loss', loss_avg.val(), global_steps)
#                 writer_dict['train_global_steps'] = global_steps + 1
#
#                 loss_avg.reset()
#
#             if i % params.valInterval == 0:
#                 val_loss, val_accuracy = val_with_2k(crnn, criterion)
#
#                 is_best = val_accuracy > best_acc
#                 best_acc = max(val_accuracy, best_acc)
#
#                 print("is best:", is_best)
#                 print("best acc is:", best_acc)
#
#                 writer = writer_dict['writer']
#                 global_steps = writer_dict['valid_global_steps']
#                 writer.add_scalar('valid_loss', val_loss, global_steps)
#                 writer.add_scalar('valid_acc', val_accuracy, global_steps)
#                 writer_dict['valid_global_steps'] = global_steps + 1
#
#             # do checkpointing
#             if i % params.saveInterval == 0 and is_best == True:
#                 torch.save(crnn.state_dict(), '{0}/netCRNN_{1}_{2}_{3}.pth'.format(params.expr_dir, epoch, i, best_acc))
#
#     writer_dict['writer'].close()


if __name__ == "__main__":
    min_loss = 1000000

    root_output_dir = Path(params.output_dir)
    time_str = time.strftime('%Y-%m-%d-%H-%M')
    tensorboard_log_dir = root_output_dir / time_str / 'log'

    # writer dict
    writer_dict = {
        'writer': SummaryWriter(log_dir=tensorboard_log_dir),
        'train_global_steps': 0,
        'valid_global_steps': 0,
    }

    best_acc = 0.5
    is_best = False

    for epoch in range(params.nepoch):
        train_iter = iter(train_loader)
        i = 0
        while i < len(train_loader):
            cost = train(crnn, criterion, optimizer, train_iter)
            loss_avg.add(cost)
            i += 1

            if i % params.displayInterval == 0:
                print('[%d/%d][%d/%d] Loss: %f' %
                      (epoch, params.nepoch, i, len(train_loader), loss_avg.val()))
                # print(loss_avg)

                writer = writer_dict['writer']
                global_steps = writer_dict['train_global_steps']
                writer.add_scalar('train_loss', loss_avg.val(), global_steps)
                writer_dict['train_global_steps'] = global_steps + 1

                loss_avg.reset()

            if i % params.valInterval == 0:
                val_loss, val_accuracy = val(crnn, criterion)

                is_best = val_accuracy > best_acc
                best_acc = max(val_accuracy, best_acc)

                print("is best:", is_best)
                print("best acc is:", best_acc)

                writer = writer_dict['writer']
                global_steps = writer_dict['valid_global_steps']
                writer.add_scalar('valid_loss', val_loss, global_steps)
                writer.add_scalar('valid_acc', val_accuracy, global_steps)
                writer_dict['valid_global_steps'] = global_steps + 1

            # do checkpointing
            if i % params.saveInterval == 0 and is_best == True:
                torch.save(crnn.state_dict(), '{0}/netCRNN_{1}_{2}_{3}.pth'.format(params.expr_dir, epoch, i, best_acc))

    writer_dict['writer'].close()