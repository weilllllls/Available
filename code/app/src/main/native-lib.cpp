#include <jni.h>
#include <string>
#include <iostream>
#include <vector>
#include <utility>
#include <string>
#include <map>
#include <android/native_window_jni.h>

#include "opencv2/opencv.hpp"
#include <opencv2/core.hpp>
#include <opencv2/imgproc.hpp>
#include <opencv2/highgui.hpp>
#include "opencv2/imgcodecs.hpp"

using namespace cv;
using namespace std;


extern "C"
JNIEXPORT jintArray JNICALL
Java_com_scuavailable_available_util_JNIUtils_getCountPaperLocation(JNIEnv *env, jclass clazz,
                                                                     jintArray buf, jint w,
                                                                     jint h) {
    // JNIEnv是一个指向JNI方法的指针
    jint *cbuf;
    cbuf = env->GetIntArrayElements(buf, JNI_FALSE); // 读取输入参数
    if (cbuf == NULL) {
        return 0;
    }
    Mat src(h, w, CV_8UC4, (unsigned char*) cbuf);

    if (src.empty()) {
        jintArray jarrtemp = env->NewIntArray(1);
        jint *pjarrtemp = env->GetIntArrayElements(jarrtemp,NULL);
        pjarrtemp[0] = 0;
        env->ReleaseIntArrayElements(jarrtemp,pjarrtemp,0);
        src.release();
        return jarrtemp;
    }

    //transform source img to gray if not
    cvtColor(src, src, COLOR_BGR2GRAY);
//    if (src.channels() == 3) {
//        cvtColor(src, gray, COLOR_BGR2GRAY);
//    }
//    else {
//        gray = src;
//    }

    // Apply adaptiveThreshold at the bitwise_not of gray, notice the ~ symbol
    // 自适应阈值化，图片最大像素值255，选定一块区域，计算这块区域的高斯均值，再减去一个偏移量，得到最终的阈值
    adaptiveThreshold(~src, src, 255, ADAPTIVE_THRESH_GAUSSIAN_C, THRESH_BINARY, 15, -2);
    //show binary img

    GaussianBlur(src, src, Size(3, 3), 0);
    //threshold(gray, blurImg, 0, 255, THRESH_OTSU);


    // Create the images that will use to extract the horizontal and vertical lines
    Mat vertical = src.clone();
    // Specify size on vertical axis
    int vertical_size = vertical.rows / 50;
    // Create structure element for extracting vertical lines through morphology operations
    Mat verticalStructure = getStructuringElement(MORPH_RECT, Size(1, vertical_size));
    // Apply morphology operations
    erode(vertical, vertical, verticalStructure, Point(-1, -1));
    dilate(vertical, vertical, verticalStructure, Point(-1, -1));
    verticalStructure.release();
    // Show extracted vertical lines

    //增强效果
    threshold(vertical, vertical, 0, 255, THRESH_BINARY | THRESH_OTSU);
    //计算条数

    //1.先分成多切片
    int height = vertical.rows;
    int width = vertical.cols;

    int scaleLowHeight = (int)height * 0.3;
    int scaleHighHeight = (int)height * 0.7;
    int scaleUserfulHeight = scaleHighHeight - scaleLowHeight;

    //根据图片大小进行分割
    int piecesLengthUnit = 25;
    int buffLengthUnit = (int)scaleUserfulHeight / 10;
    if (buffLengthUnit <= piecesLengthUnit) {
        buffLengthUnit = 30;
    }
    // 5个单位长度作缓冲区
    //切片的份数
    int piecesNumberUnit = (int)scaleUserfulHeight / buffLengthUnit;
    //每份的长度
    int heightUnit = (int)scaleUserfulHeight / piecesNumberUnit;

    //根据份数进行算法选择
    vector<Mat> pieces;
    vector<pair<int, int>> heightMarkList;
    int tempLowHeight = scaleLowHeight;

    if (piecesNumberUnit >= 10) {
        for (int i = 0; i < 10; i++) {
            Mat tempPiece = vertical(Range(tempLowHeight, tempLowHeight + piecesLengthUnit), Range::all());
            pieces.push_back(tempPiece);
            int tempLowHighHeight = tempLowHeight + piecesLengthUnit;
            heightMarkList.push_back(make_pair(tempLowHeight, tempLowHighHeight));
            tempLowHeight += heightUnit;
        }
    }
    else if (piecesNumberUnit > 0) {
        for (int i = 0; i < piecesNumberUnit; i++) {
            Mat tempPiece = vertical(Range(tempLowHeight, tempLowHeight + piecesLengthUnit), Range::all());
            pieces.push_back(tempPiece);
            int tempLowHighHeight = tempLowHeight + piecesLengthUnit;
            heightMarkList.push_back(make_pair(tempLowHeight, tempLowHighHeight));
            tempLowHeight += heightUnit;
        }
    }
    else {
        Mat tempPiece = vertical(Range(scaleLowHeight, scaleHighHeight), Range::all());
        pieces.push_back(tempPiece);
        heightMarkList.push_back(make_pair(scaleLowHeight, scaleHighHeight));
    }

    //计数
    // 达到threshHigh判定为线段
    // markHeight标记图片的位置
    vector<vector<int>> piecesTargetColList;
    vector<int> piecesCountNumber;
    //遍历前面分片得到的图片，计算每个图片的数目
    size_t piecesNumber = pieces.size();
    for (size_t i = 0; i < piecesNumber; i++) {
        Mat apiece = pieces[i];
        int pWidth = apiece.cols;
        Mat Vdist = Mat::zeros(1, pWidth, CV_32FC1);
        reduce(apiece, Vdist, 0, REDUCE_AVG, CV_32FC1);

        // 选出最大值和最小值
        double minVal, maxVal;
        int minIdx[2] = {};
        int maxIdx[2] = {};
        minMaxIdx(Vdist, &minVal, &maxVal, minIdx, maxIdx);
        double threshHigh = ((minVal + maxVal) / 4);
        double threshLow = ((minVal + maxVal) / 16);
        int countNumber = 0;
        bool belowThresh = true;
        int curCol = 0;
        vector<int> targetColList;

        //比较每一横坐标与阈值判断是否为线段
        size_t VdistLength = Vdist.cols;
        for (size_t j = 0; j < VdistLength; j++) {
            double proVal = Vdist.at<float>(Point(j, 0));
            if (proVal >= threshHigh) {
                if (belowThresh) {
                    countNumber++;
                    targetColList.push_back(curCol);
                    belowThresh = false;
                }
            }
            else if (proVal <= threshLow) {
                if (belowThresh == false) {
                    belowThresh = true;
                }
            }
            curCol += 1;
        }
        piecesTargetColList.push_back(targetColList);
        piecesCountNumber.push_back(countNumber);
    }

    //piecesTargetColList存放piecesCountNumber中每一个点对应的坐标

    //计算众数得到唯一结果
    //求众数及其索引
    int maxCount = 0;
    int numPos = 0;
    map<int,int> m;
    for (int i = 0; i < piecesCountNumber.size(); i++) {
        int num = piecesCountNumber[i];
        m[num]++;
        int curNum = m[num];
        if(curNum > maxCount){
            maxCount = curNum;
            numPos = i;
        }
    }


    // maxCount为众数 numPos为该众数的index
    // 至此每一个点及其坐标已经获得
    // candidate 为数量 piecesTargetColList[numPos]为每一点的横坐标 heightMarkList[numPos]为该区间的纵坐标范围
//    整理数据
    int dataSize = 3+piecesTargetColList[numPos].size();
    int retData[dataSize];
    retData[0] = dataSize;
    retData[1] = maxCount;
    retData[2] = (int)((heightMarkList[numPos].first+heightMarkList[numPos].second)/2);
    copy(piecesTargetColList[numPos].begin(),piecesTargetColList[numPos].end(),retData+3);
    jintArray jarr = env->NewIntArray(dataSize);
    jint *pjarr = env->GetIntArrayElements(jarr,NULL);
    for(int i = 0; i <dataSize; i++){
        pjarr[i] = retData[i];
    }
    env->ReleaseIntArrayElements(jarr,pjarr,0);
    return jarr;
}