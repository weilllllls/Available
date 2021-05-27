import os
import json

str = []
with open("/home/wyj/PycharmProjects/OCR/crnn_chinese_characters_rec/lib/dataset/txt/wyj_dict.txt", "r",
          encoding="utf-8") as file:
    for line in file.readlines():
        str.append(line.replace("\n", ""))


def pro_train():
    with open("/home/wyj/Public/OCR_competition/OCR_competition_dataset/scsd/train.txt", "r", encoding="utf-8") as f:
        with open("/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/dataset/txt/train_360.txt", "w",
                  encoding="utf-8") as f2:
            data = f.readlines()
            for line in data:
                label_str = ""
                img_name = line.strip().split(" ", 1)[0]
                label = line.strip().split(" ", 1)[1]
                label_idx = label.split(" ")
                print(img_name)
                print(label_idx)
                for idx in label_idx:
                    # print(idx)
                    # print(type(idx))
                    idx = int(idx)
                    # print(type(idx))
                    label_str += str[idx]
                    # print(str.index(char))
                img_parent_path = "/home/wyj/Public/OCR_competition/OCR_competition_dataset/scsd/images"
                img_path = os.path.join(img_parent_path, img_name)
                f2.write(img_path)
                f2.write("\n")
                f2.write(label_str)
                f2.write("\n")


def pro_test():
    with open("/home/wyj/Public/OCR_competition/OCR_competition_dataset/scsd/test.txt", "r", encoding="utf-8") as f:
        with open("/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/dataset/txt/test_360.txt", "w",
                  encoding="utf-8") as f2:
            data = f.readlines()
            for line in data:
                label_str = ""
                img_name = line.strip().split(" ", 1)[0]
                label = line.strip().split(" ", 1)[1]
                label_idx = label.split(" ")
                print(img_name)
                print(label_idx)
                for idx in label_idx:
                    # print(idx)
                    # print(type(idx))
                    idx = int(idx)
                    # print(type(idx))
                    label_str += str[idx]
                    # print(str.index(char))
                img_parent_path = "/home/wyj/Public/OCR_competition/OCR_competition_dataset/scsd/images"
                img_path = os.path.join(img_parent_path, img_name)
                f2.write(img_path)
                f2.write("\n")
                f2.write(label_str)
                f2.write("\n")


def pro_10w_json():
    with open("/home/wyj/Public/text_renderer-master/example_data/output/100w/labels.json", "r", encoding="utf-8") as f:
        with open("/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/dataset/txt/100w/100w.txt", "w", encoding="utf-8") as f2:
            dict_10w = json.load(f)
            # print(dict_10w)
            num_samples = dict_10w["num-samples"]
            dict_labels = dict_10w["labels"]
            # print(num_samples)
            # print(dict_labels["000000000"])
            for key, value in dict_labels.items():
                img_name = key + ".jpg"
                img_parent_path = "/home/wyj/Public/text_renderer-master/example_data/output/100w/images"
                img_path = os.path.join(img_parent_path, img_name)
                f2.write(img_path)
                f2.write("\n")
                f2.write(value)
                f2.write("\n")

def dataset_split_10w():
    cnt = 0
    with open("/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/dataset/txt/10w/10w.txt", "r", encoding="utf-8") as f:
        with open("/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/dataset/txt/10w/10w_train.txt", "w",
                  encoding="utf-8") as f2:
            with open("/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/dataset/txt/10w/10w_test.txt", "w",
                      encoding="utf-8") as f3:
                for line in f.readlines():
                    cnt += 1
                    if cnt <= 230624:
                        f2.write(line)
                    else:
                        f3.write(line)

def dataset_split_100w():
    cnt = 0
    with open("/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/dataset/txt/100w/100w.txt", "r", encoding="utf-8") as f:
        with open("/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/dataset/txt/100w/100w_train.txt", "w",
                  encoding="utf-8") as f2:
            with open("/home/wyj/PycharmProjects/OCR/crnn-pytorch-master/dataset/txt/100w/100w_test.txt", "w",
                      encoding="utf-8") as f3:
                for line in f.readlines():
                    cnt += 1
                    if cnt <= 850000 * 2:
                        f2.write(line)
                    else:
                        f3.write(line)


if __name__ == '__main__':
    # pro_train()
    # pro_test()
    # pro_10w_json()
    # dataset_split_10w()
    dataset_split_100w()