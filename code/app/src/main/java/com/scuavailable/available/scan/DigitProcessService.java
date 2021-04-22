package com.scuavailable.available.scan;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.RectF;
import android.util.Log;
import android.widget.ImageView;

import com.scuavailable.available.R;
import com.scuavailable.available.TFLiteLoader;

import org.checkerframework.checker.units.qual.C;
import org.opencv.android.Utils;
import org.opencv.core.Mat;
import org.opencv.core.Rect;

import java.util.ArrayList;
import java.util.List;

import javax.security.auth.login.LoginException;

public class DigitProcessService {

    private DigitSeparator digitSeparator;
    private TFLiteLoader tfLiteLoader;
    private Context context;
    private ImageView imageView;
    private static float canvasWidth = 100;
    private static float canvasHeight = 100;
    private int startY;
    private String TAG = "DigitProcessService";

    public DigitProcessService(Context context){
        digitSeparator = new DigitSeparator();
        tfLiteLoader = new TFLiteLoader(context);
        this.context = context;

    }

    public Bitmap processFrame(Bitmap frame,float canvasWidth, float canvasHeight){
        this.canvasWidth =canvasWidth;
        this.canvasHeight = canvasHeight;
//
//
//        BitmapFactory.Options opts = new BitmapFactory.Options();
//        opts.inPreferredConfig = Bitmap.Config.ARGB_8888;
//        frame = BitmapFactory.decodeResource(context.getResources(), R.drawable.test_digit,opts);

        //数字定位与提取
        digitSeparator.digitSeparate(frame);
        this.startY = digitSeparator.getStartY();
        //数字识别
        //boundRects里面存储着每一个框框的坐标位置
        //resultMats存的是每个框框的图片
        //resultMatsNum存的是每个框框的图片中有多少个数字
        //resultSingleMats;存的是所有框框所拆分的数字图片
        List<Mat> resultSingleMats = digitSeparator.getResultSingleMats();
        List<Integer> resultPredictNum = new ArrayList<Integer>();
        for (int i = 0; i < resultSingleMats.size(); i++) {
            Mat rawMat = resultSingleMats.get(i);
            Bitmap rawBitmap = Bitmap.createBitmap(rawMat.cols(),rawMat.rows(), Bitmap.Config.RGB_565);
            Utils.matToBitmap(rawMat, rawBitmap);
            int processResult = tfLiteLoader.process(rawBitmap);
            resultPredictNum.add(processResult);
        }

        //将单个数字合并成一个完整的数字
        List<Integer> finalNums = new ArrayList<Integer>();
        List<Integer> resultMatsNum = digitSeparator.getResultMatsNum();
        Log.e("TAG","总共有"+String.valueOf(resultMatsNum.size()) + "个数字");
        if(resultMatsNum.size() <=0){
            return null;
        }
        int hasReadCount = 0;
        for (int i = 0; i < resultMatsNum.size(); i++) {
            //图片中的位数
            int number = resultMatsNum.get(i);
            if(number <= 0){
                continue;
            }
            Log.e(TAG,"resultMatsNum.get(i)" + String.valueOf(number));
            //读取该位数长度的数字
            String strNum = "";
            for (int j = hasReadCount; j <hasReadCount+number; j++) {
                Integer curNum = resultPredictNum.get(j);
                Log.e(TAG,"resultPredictNum.get(j))" + String.valueOf(curNum));
                strNum += String.valueOf(curNum);
            }
            int finalNum = Integer.parseInt(strNum);
            finalNums.add(finalNum);
            hasReadCount += number;
        }

        //绘制每一帧 并呈现出来

        //每个框框的位置
        List<Rect> boundRects = digitSeparator.getBoundRects();

        final Bitmap croppedBitmap = Bitmap.createBitmap((int) canvasWidth, (int) canvasHeight, Bitmap.Config.ARGB_8888);
        final Canvas canvas = new Canvas(croppedBitmap);

        int totalScore = 0;

        Paint paintBox = new Paint();
        Paint paintNumber = new Paint();

        paintBox.setStyle(Paint.Style.STROKE);
        paintBox.setColor(Color.argb(200,154,179,245));
        paintBox.setAntiAlias(true);
        paintBox.setStrokeWidth(5.0f);

        paintNumber.setColor(Color.argb(200,15,48,87));
        paintNumber.setStyle(Paint.Style.FILL);
        paintNumber.setAntiAlias(true);
        paintNumber.setTextSize(40f);

        for (int m = 0; m <boundRects.size(); m++) {
            Rect location = boundRects.get(m);
            Integer score = finalNums.get(m);
            totalScore += score;
            canvas.drawRect(new Double(location.tl().x).floatValue(),
                    new Double(location.tl().y+startY).floatValue(),
                    new Double(location.br().x).floatValue(),
                    new Double(location.br().y+startY).floatValue(),paintBox);
            canvas.drawText(String.valueOf(score), new Double(location.tl().x).floatValue(), new Double(location.br().y+startY+50).floatValue(), paintNumber);
        }
        //绘制总成绩
        Paint paintTotalScore = new Paint();
        paintTotalScore.setColor(Color.argb(200,215,56,94));
        paintTotalScore.setStyle(Paint.Style.FILL);
        paintTotalScore.setAntiAlias(true);
        paintTotalScore.setTextSize(90f);
        Rect lastLocation = boundRects.get(boundRects.size()-1);

        canvas.drawText(String.valueOf(totalScore), new Double(lastLocation.br().x+25).floatValue(), new Double(lastLocation.tl().y+startY+200).floatValue(), paintTotalScore);

        return croppedBitmap;

    }

}



