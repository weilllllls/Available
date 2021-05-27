import os
import time
import numpy as np
import torch
from torch.autograd import Variable
import utils
import dataset
from PIL import Image
import cv2

import models.crnn as crnn
import params
import argparse
from metrics import calculate_task2

# parser = argparse.ArgumentParser()
# parser.add_argument('-m', '--model_path', type = str, required = True, help = 'crnn model path')
# parser.add_argument('-i', '--image_path', type = str, required = True, help = 'demo image path')
# args = parser.parse_args()

# model_path = "checkpoint/10w/netCRNN_136_60_0.7528935185185185.pth"
model_path = "checkpoint/110w/netCRNN_0_420_0.7867457301051051.pth"
# model_path = "output/crnn/2k/netCRNN_118_30_0.86669921875.pth" # Word accuracy:0.888  Char accuracy:0.754247649563641
file_locations = []
file_name = []
BASE_DIR = r"../TestData/ocr/recognition/images_2k/images_2"
# BASE_DIR = r"/home/wyj/PycharmProjects/OCR/TestData/ocr/recognition/images_2k/padding_images"
with open(r'../TestData/ocr/recognition/images_2k/image_info_A_2000.txt', 'r', encoding='utf-8') as f:
    for line in f.readlines():
        name = line.split('\t')[0]
        file = os.path.join(BASE_DIR, name)
        file_locations.append(file)
        file_name.append(name)


# net init
nclass = len(params.alphabet) + 1
model = crnn.CRNN(params.imgH, params.nc, nclass, params.nh)
if torch.cuda.is_available():
    model = model.cuda()

# load model
print('loading pretrained model from %s' % model_path)
if params.multi_gpu:
    model = torch.nn.DataParallel(model)
model.load_state_dict(torch.load(model_path))

converter = utils.strLabelConverter(params.alphabet)

transformer = dataset.resizeNormalize((100, 32))

total = 0
with open(r'./output/task2_wyj_2k_train.txt', 'w', encoding='utf-8') as f:
    for i, img_file in enumerate(file_locations):
        started = time.time()
        # image = Image.open(img_file).convert('L')
        img = cv2.imread(img_file)

        h, w, c = img.shape
        #
        # fisrt step: resize the height and width of image to (32, x)
        img = cv2.resize(img, None, fx=32 / h, fy=32 / h,
                         interpolation=cv2.INTER_CUBIC)
        #
        # second step: keep the ratio of image's text same with training
        h, w, c = img.shape
        #
        if w < 280:
            a = np.full((h, 280 - w, 3), 0, dtype=np.uint8)
            # print(a.shape)
            img = np.concatenate([img, a], axis=1)
        h, w, c = img.shape

        # image.show()
        # print(image.size)

        # w, h = image.size
        #
        # image = np.asarray(image)
        # # print(image)
        # # print(image.shape)
        # if w < 280:
        #     a = np.full((h, 280 - w), 0)
        #     image = np.concatenate([image, a], axis=1)
        #
        # image = Image.fromarray(np.uint8(image))
        # image.show()
        image = Image.fromarray(cv2.cvtColor(img, cv2.COLOR_BGR2RGB))
        image = image.convert('L')

        image = transformer(image)
        if torch.cuda.is_available():
            image = image.cuda()
        image = image.view(1, *image.size())
        image = Variable(image)

        model.eval()
        preds = model(image)

        _, preds = preds.max(2)
        preds = preds.transpose(1, 0).contiguous().view(-1)

        preds_size = Variable(torch.LongTensor([preds.size(0)]))
        raw_pred = converter.decode(preds.data, preds_size.data, raw=True)
        sim_pred = converter.decode(preds.data, preds_size.data, raw=False)
        print('%-20s => %-20s' % (raw_pred, sim_pred))

        finished = time.time()

        f.write(file_name[i] + '\t' + sim_pred + '\n')
        print('elapsed time: {0}'.format(finished - started))
        total += finished - started
print("total: {}s".format(total))

calculate_task2(r'./output/task2_wyj_2k_train.txt',
                r'../TestData/ocr/recognition/images_2k/image_info_A_2000.txt')



