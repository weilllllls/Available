package com.scuavailable.available.scan;

import android.graphics.Bitmap;
import android.util.Log;

import org.opencv.android.Utils;
import org.opencv.core.Core;
import org.opencv.core.Mat;
import org.opencv.core.MatOfPoint;
import org.opencv.core.Point;
import org.opencv.core.Rect;
import org.opencv.core.Size;
import org.opencv.imgproc.Imgproc;

import java.util.ArrayList;
import java.util.List;

import static org.opencv.core.Core.BORDER_CONSTANT;
import static org.opencv.core.Core.bitwise_and;
import static org.opencv.core.Core.bitwise_not;
import static org.opencv.core.Core.copyMakeBorder;
import static org.opencv.imgproc.Imgproc.COLOR_RGB2BGR;
import static org.opencv.imgproc.Imgproc.MORPH_RECT;
import static org.opencv.imgproc.Imgproc.cvtColor;
import static org.opencv.imgproc.Imgproc.dilate;
import static org.opencv.imgproc.Imgproc.erode;
import static org.opencv.imgproc.Imgproc.getStructuringElement;
import static org.opencv.imgproc.Imgproc.resize;


public class LetterSeparator {
    private static final String TAG="LetterSeparator";
    //boundRects里面存储着每一个字母的坐标位置
    private List<Rect> boundRects;
    //resultMats存的是每个字母的图片
    private List<Mat> resultMats;
    private int orinHeight;
    private int orinWidth;
    private int startX;
    private int startY;
    private int cutWidth;
    private int cutHeight;



    public LetterSeparator(){
    }

    public void letterSeparate(Bitmap bitmap){

        Mat srcOrin = new Mat();
        Utils.bitmapToMat(bitmap,srcOrin,false);
        orinHeight = srcOrin.height();
        orinWidth = srcOrin.width();
        startX =(int)(0.20*orinWidth);
        startY = (int)(0.20*orinHeight);
        cutWidth = orinWidth;
        cutHeight = (int)(0.28*orinHeight);
        Mat src = new Mat(srcOrin,new Rect(startX,startY,cutWidth-startX,cutHeight-startY));
        cvtColor(src,src,COLOR_RGB2BGR);
        Imgproc.cvtColor(src, src, Imgproc.COLOR_BGR2GRAY);
        Core.bitwise_not(src,src);
        Imgproc.threshold(src, src, 100, 255, Imgproc.THRESH_BINARY);

        int horizontalSize = (int)(orinWidth/20);
        Mat maskLine;
        maskLine = getHorizontalLines(src,horizontalSize);
        bitwise_not(maskLine,maskLine);
        //去除直线
        bitwise_and(src,maskLine,src);
//        Imgproc.medianBlur(src, src, 3);

        List<MatOfPoint> contours = new ArrayList<>();
        Mat hierarchy = new Mat();
        Imgproc.findContours(src, contours,hierarchy, Imgproc.RETR_EXTERNAL, Imgproc.CHAIN_APPROX_NONE);
        //boundRects里面存储着每一个字母的坐标位置
        boundRects = new ArrayList<Rect>();
        //resultMats存的是每个字母图片
        resultMats = new ArrayList<Mat>();
        for (int i = 0; i < contours.size(); i++) {
            Rect curRect = Imgproc.boundingRect(contours.get(i));
            Mat curMat;
            curMat = src.submat(curRect);
            //寻找单个字母并去除噪声
            int srcH, srcW;
            srcH = src.rows();
            srcW = src.cols();
            if (isAreaOk(srcH, srcW, curRect)) {
                Mat element = Imgproc.getStructuringElement(Imgproc.MORPH_RECT,new Size(5,5));
                Imgproc.dilate(curMat, curMat, element, new Point(-1, -1));
                element.release();
                //转换成符合mnist格式
                int extendPiexl = 0;
                extendPiexl = Math.abs(curMat.rows() - curMat.cols()) / 2;
                copyMakeBorder(curMat, curMat, 7, 7, extendPiexl + 7, extendPiexl + 7, BORDER_CONSTANT);
                resize(curMat, curMat, new Size(28, 28));
                Core.bitwise_not(curMat,curMat);
                resultMats.add(curMat);
                boundRects.add(curRect);

            }
        }

    }

    private Mat getHorizontalLines(Mat src, int lineSize){
        Mat element = getStructuringElement(MORPH_RECT, new Size(lineSize, 1));
        Mat img = src.clone();

        erode(img,img,element);
        dilate(img,img,element);
        return img;
    }


    boolean isAreaOk(int h, int w, Rect dst)
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
    public List<Rect> getBoundRects() {
        return boundRects;
    }

    public List<Mat> getResultMats() {
        return resultMats;
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
}
