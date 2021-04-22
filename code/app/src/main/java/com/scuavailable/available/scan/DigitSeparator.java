package com.scuavailable.available.scan;
import android.graphics.Bitmap;
import android.util.Log;

import org.opencv.android.Utils;
import org.opencv.core.*;
import org.opencv.imgcodecs.Imgcodecs;
import org.opencv.imgproc.Imgproc;

import java.util.ArrayList;
import java.util.List;

import static org.opencv.core.Core.BORDER_CONSTANT;
import static org.opencv.core.Core.copyMakeBorder;
import static org.opencv.imgproc.Imgproc.COLOR_BGR2BGR565;
import static org.opencv.imgproc.Imgproc.COLOR_BGR2RGB;
import static org.opencv.imgproc.Imgproc.COLOR_RGB2BGR;
import static org.opencv.imgproc.Imgproc.cvtColor;
import static org.opencv.imgproc.Imgproc.resize;


public class DigitSeparator {
    private static final String TAG="DigitSeparator";
    //boundRects里面存储着每一个框框的坐标位置
    private List<Rect> boundRects;
    //resultMats存的是每个框框的图片
    private List<Mat> resultMats;
    //resultMatsNum存的是每个框框的图片中有多少个数字
    private List<Integer> resultMatsNum;
    //resultMats存的是所有框框所拆分的数字图片
    private List<Mat> resultSingleMats;

    private int orinHeight;



    private int orinWidth;
    private int startX;
    private int startY;
    private int cutWidth;
    private int cutHeight;

    public DigitSeparator(){
    }

    public void digitSeparate(Bitmap bitmap){
        Mat orinSrc = new Mat();

        Utils.bitmapToMat(bitmap,orinSrc,false);
        orinHeight = orinSrc.height();
        orinWidth = orinSrc.width();
        startX = 0;
        startY = (int)(0.13*orinHeight);
        cutWidth = orinWidth;
        cutHeight = (int)(0.18*orinHeight);
        Log.e(TAG,String.valueOf(startY) + "  " + String.valueOf(cutHeight) + " " + String.valueOf(orinHeight));

        Mat src = new Mat(orinSrc,new Rect(startX,startY,cutWidth-startX,cutHeight-startY));
        cvtColor(src,src,COLOR_RGB2BGR);

        Mat srcClone = src.clone();

        colorFilter(src);
        erodeImage(src);
        Imgproc.cvtColor(src, src, Imgproc.COLOR_BGR2GRAY);
        Core.bitwise_not(src,src);
        Imgproc.threshold(src, src, 100, 255, Imgproc.THRESH_BINARY);


        List<MatOfPoint> contours = new ArrayList<>();
        Mat hierarchy = new Mat();
        Imgproc.findContours(src, contours,hierarchy, Imgproc.RETR_EXTERNAL, Imgproc.CHAIN_APPROX_NONE);
        //boundRects里面存储着每一个框框的坐标位置
        boundRects = new ArrayList<Rect>();
        //resultMats存的是每个框框的图片
        resultMats = new ArrayList<Mat>();
        //resultMatsNum存的是每个框框的图片中有多少个数字
        resultMatsNum = new ArrayList<Integer>();
        //resultMats存的是所有框框所拆分的数字图片
        resultSingleMats = new ArrayList<Mat>();


        for (int i = 0; i < contours.size(); i++) {
            Rect curRect = Imgproc.boundingRect(contours.get(i));

            Mat temp;
            temp = srcClone.submat(curRect);
            //去除边框噪声处理
            colorRemover(temp);
            //二值化图片
            Mat binImgPiece = temp.clone();
            Imgproc.cvtColor(binImgPiece, binImgPiece, Imgproc.COLOR_BGR2GRAY);
            Imgproc.threshold(binImgPiece, binImgPiece, 0, 255, Imgproc.THRESH_BINARY_INV + Imgproc.THRESH_OTSU);
            //寻找单个数字并去除噪声
            int srcH, srcW;
            srcH = binImgPiece.rows();
            srcW = binImgPiece.cols();
            List<MatOfPoint> contoursSingle = new ArrayList<MatOfPoint>();
            // 单个框框有的数字的个数
            int numberCount = 0;

            Imgproc.findContours(binImgPiece, contoursSingle, hierarchy,Imgproc.RETR_EXTERNAL, Imgproc.CHAIN_APPROX_NONE);
            //contoursSingle存的是一个框框内寻找方框的结果，其中包括噪声
            //去噪
            for (int j = 0; j < contoursSingle.size(); j++) {
                Rect tempRect = Imgproc.boundingRect(contoursSingle.get(j));
                if (isAreaOk(srcH, srcW, tempRect)) {
                    //转换成符合mnist格式
                    Mat singleImg;
                    singleImg = binImgPiece.submat(tempRect).clone();
//                    dilateImage(singleImg);
                    int extendPiexl = 0;
                    extendPiexl = Math.abs(singleImg.rows() - singleImg.cols()) / 2;
                    copyMakeBorder(singleImg, singleImg, 7, 7, extendPiexl + 7, extendPiexl + 7, BORDER_CONSTANT);
                    resize(singleImg, singleImg, new Size(28, 28));
                    resultSingleMats.add(singleImg);
                    numberCount++;
                }
            }
            if(numberCount != 0){
                //存在数字
                boundRects.add(curRect);
                resultMats.add(temp);
                resultMatsNum.add(numberCount);
            }

        }
        //
//        for (int i = 0; i < contours.size(); i++) {
//        }
//
//        for (int i = 0; i < resultSingleMats.size(); i++) {
//        }
    }


    public List<Rect> getBoundRects() {
        return boundRects;
    }

    public List<Mat> getResultMats() {
        return resultMats;
    }

    public List<Integer> getResultMatsNum() {
        return resultMatsNum;
    }

    public List<Mat> getResultSingleMats() {
        return resultSingleMats;
    }
    public int getOrinHeight() {
        return orinHeight;
    }

    public int getOrinWidth() {
        return orinWidth;
    }

    public int getStartX() {
        return startX;
    }

    public int getStartY() {
        return startY;
    }

    public int getCutWidth() {
        return cutWidth;
    }

    public int getCutHeight() {
        return cutHeight;
    }

    private Mat colorFilter(Mat srcImage)
    {

        Mat srcImage_hsv = new Mat(srcImage.rows(),srcImage.cols(),srcImage.type());
        Imgproc.cvtColor(srcImage,srcImage_hsv,Imgproc.COLOR_BGR2HSV);
        int nl = srcImage.rows();
        int nc = srcImage.cols();
        double[] pixelColor = {255,255,255};
        for (int m = 0; m < nl; m++)
        {

            for (int n = 0; n < nc; n++)
            {
                //&& (srcImage_hsv.at<Vec3b>(m, n)[1]>43)
                //以下代码是提取红色部分

                if (!((((srcImage_hsv.get(m, n)[0] >= 0) && (srcImage_hsv.get(m, n)[0] <= 15)) || (srcImage_hsv.get(m, n)[0] >= 125) && (srcImage_hsv.get(m, n)[0] <= 180)) && (srcImage_hsv.get(m, n)[2] >= 46) && (srcImage_hsv.get(m, n)[1] >= 43)))
                {
                    srcImage.put(m, n,pixelColor);
                }

            }

        }
        srcImage_hsv.release();
        return srcImage;
    }


    private Mat colorRemover(Mat srcImage)
    {
        Mat srcImage_hsv = new Mat(srcImage.rows(),srcImage.cols(),srcImage.type());
        Imgproc.cvtColor(srcImage,srcImage_hsv,Imgproc.COLOR_BGR2HSV);
        int nl = srcImage.rows();
        int nc = srcImage.cols();
        double[] srcBackgroundColor = srcImage.get(1, 1);

        for (int m = 0; m < nl; m++)
        {

            for (int n = 0; n < nc; n++)
            {
                //以下代码是删除绿色部分
                if ((srcImage_hsv.get(m, n)[0] >= 30) && (srcImage_hsv.get(m, n)[0] <= 105))
                {
                    srcImage.put(m, n,srcBackgroundColor);

                }

            }

        }
        srcImage_hsv.release();
        return srcImage;
    }




    private Mat dilateImage(Mat srcImage)
    {

        Mat element = Imgproc.getStructuringElement(Imgproc.MORPH_RECT,new Size(5,5));
        Imgproc.dilate(srcImage, srcImage, element, new Point(-1, -1));
        element.release();
        return srcImage;
    }

    private Mat erodeImage(Mat srcImage)
    {
        Mat element = Imgproc.getStructuringElement(Imgproc.MORPH_RECT,new Size(5,5));
        Imgproc.erode(srcImage, srcImage, element, new Point(-1, -1), 3);
        element.release();
        return srcImage;
    }

    private boolean isAreaOk(int h, int w, Rect dst)
    {
        int dstH = dst.height;
        int dstW = dst.width;
        if (dstH < 3 || dstW < 3) {
            return false;
        }
        if(dstH < 15 && dstW < 15){
            return false;
        }
        if (h - dstH < 3 || w - dstW < 3) {
            return false;
        }
        if (dst.area() < (w * h / 64)) {
            return false;
        }
        return true;
    }
}
