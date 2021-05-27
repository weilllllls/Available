import cv2
import numpy as np

# output_dir = "/home/wyj/Public/text_renderer-master/example_data/output/100w/images/"
# cnt = 0
# with open(r'../dataset/txt/100w/100w_train.txt', 'r', encoding='utf-8') as f:
#     for line in f.readlines():
#         cnt += 1
#         if cnt % 2 == 0:
#             continue
#         img_path = line.strip()
#         print(img_path)
#         img_name = img_path.split("/")[-1]
#         # print(img_name)
#         img = cv2.imread(img_path)
#         # print(img)
#         # print(img.shape)
#
#         # cv2.imshow("origin", img)
#         # cv2.waitKey(0)
#         h, w, c = img.shape
#         #
#         # fisrt step: resize the height and width of image to (32, x)
#         img = cv2.resize(img, None, fx=32 / h, fy=32 / h,
#                          interpolation=cv2.INTER_CUBIC)
#         #
#         # second step: keep the ratio of image's text same with training
#         h, w, c = img.shape
#         #
#         if w < 280:
#             a = np.full((h, 280 - w, 3), 0, dtype=np.uint8)
#             # print(a.shape)
#             img = np.concatenate([img, a], axis=1)
#         h, w, c = img.shape
#         #
#         # print(img)
#         # print(img.shape)
#         # cv2.imshow("padding", img)
#         # cv2.waitKey(0)
#         cv2.imwrite(output_dir + img_name, img)

# cnt = 0
# with open(r'../dataset/txt/100w/100w_test.txt', 'r', encoding='utf-8') as f:
#     for line in f.readlines():
#         cnt += 1
#         if cnt % 2 == 0:
#             continue
#         img_path = line.strip()
#         print(img_path)
#         img_name = img_path.split("/")[-1]
#         # print(img_name)
#         img = cv2.imread(img_path)
#
#         h, w, c = img.shape
#         #
#         # fisrt step: resize the height and width of image to (32, x)
#         img = cv2.resize(img, None, fx=32 / h, fy=32 / h,
#                          interpolation=cv2.INTER_CUBIC)
#         #
#         # second step: keep the ratio of image's text same with training
#         h, w, c = img.shape
#         #
#         if w < 280:
#             a = np.full((h, 280 - w, 3), 0, dtype=np.uint8)
#             # print(a.shape)
#             img = np.concatenate([img, a], axis=1)
#         h, w, c = img.shape
#
#         cv2.imwrite(output_dir + img_name, img)

# BASE_DIR = "/home/wyj/PycharmProjects/OCR/TestData/ocr/recognition/images_2k/images_2/"
# with open(r'/home/wyj/PycharmProjects/OCR/TestData/ocr/recognition/images_2k/image_info_A_2000.txt', 'r', encoding='utf-8') as f:
#     for line in f.readlines():
#         sline = line.strip()
#         # print(sline)
#         img_name = sline.split("\t")[0]
#         # print(img_name)
#         img_path = BASE_DIR + img_name
#         print(img_path)
#         img = cv2.imread(img_path)
#
#         h, w, c = img.shape
#         #
#         # fisrt step: resize the height and width of image to (32, x)
#         img = cv2.resize(img, None, fx=32 / h, fy=32 / h,
#                          interpolation=cv2.INTER_CUBIC)
#         #
#         # second step: keep the ratio of image's text same with training
#         h, w, c = img.shape
#         #
#         if w < 280:
#             a = np.full((h, 280 - w, 3), 0, dtype=np.uint8)
#             # print(a.shape)
#             img = np.concatenate([img, a], axis=1)
#         h, w, c = img.shape
#         output_dir = "/home/wyj/PycharmProjects/OCR/TestData/ocr/recognition/images_2k/padding_images/"
#         cv2.imwrite(output_dir + img_name, img)
#
# BASE_DIR = "/home/wyj/PycharmProjects/OCR/TestData/ocr/recognition/images_2k/padding_images/"
# with open(r'/home/wyj/PycharmProjects/OCR/TestData/ocr/recognition/images_2k/image_info_A_2000.txt', 'r', encoding='utf-8') as f:
#     with open("/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/dataset/txt/2k.txt", 'w', encoding='utf-8') as f2:
#         for line in f.readlines():
#             sline = line.strip()
#             # print(sline)
#             img_name = sline.split("\t")[0]
#             label = sline.split("\t")[1]
#             # print(img_name)
#             img_path = BASE_DIR + img_name
#             print(img_path)
#             f2.write(img_path)
#             f2.write("\n")
#             f2.write(label)
#             f2.write("\n")

BASE_DIR = "/home/wyj/OCR/TestData/ocr/recognition/images_2k/images_2/"
with open(r'/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/output/task2_wyj_2k_train.txt', 'r', encoding='utf-8') as f:
    with open(r'/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/output/task2.txt', 'w', encoding='utf-8') as f2:
        for line in f.readlines():
            sline = line.strip()
            print(sline)
            img_name = sline.split("\t")[0]
            print(img_name)
            img_path = BASE_DIR + img_name
            print(img_path)
            img_label = sline.split("\t")[1]
            print(img_label)
            f2.write(img_path + "\t" + img_label + "\n")




