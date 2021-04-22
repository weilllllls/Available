package com.scuavailable.available.scan;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.os.Environment;
import android.util.Log;
import android.widget.Toast;

import org.opencv.android.Utils;
import org.opencv.core.Mat;
import org.opencv.core.Rect;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class LetterProcessService {

    private static final String TAG= "LetterProcessService";
    private LetterClassifier mLetterClassifier;
    private LetterSeparator mLetterSeparator;
    private LetterResult letterResult;
    private char[] alpha = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

    private Activity mActivity;
    private static float canvasWidth = 100;
    private static float canvasHeight = 100;

    private int startX;
    private int startY;



    public LetterProcessService(Activity activity){
        this.mActivity = activity;
        mLetterSeparator = new LetterSeparator();
        try {
            mLetterClassifier = new LetterClassifier(activity);
        } catch (IOException e) {
            Log.e(TAG, "init(): Failed to create Classifier", e);
        }
    }

    public Bitmap processFrame(Bitmap frame, float canvasWidth, float canvasHeight, ArrayList<Character> rightAnswers, ArrayList<Integer> answersRange){
//        storeImage(frame,"frame");
        this.canvasWidth =canvasWidth;
        this.canvasHeight = canvasHeight;

        mLetterSeparator.letterSeparate(frame);
        List<Mat> resultMats = mLetterSeparator.getResultMats();
        List<Rect> boundRects = mLetterSeparator.getBoundRects();
        List<LetterResult> letterResults = new ArrayList<LetterResult>();
        //如果识别结果和答案个数不一致 =》 直接返回
//        for (int i = 0; i < answersRange.size(); i++) {
//            Integer integer = answersRange.get(i);
//            Log.e(TAG,"shuzi "+String.valueOf(integer));
//        }
//        for (int i = 0; i < rightAnswers.size(); i++) {
//            Character character = rightAnswers.get(i);
//            Log.e(TAG,"zimnu "+String.valueOf(character));
//        }
        startX = mLetterSeparator.getStartX();
        startY = mLetterSeparator.getStartY();

        for (int i = 0; i < resultMats.size(); i++) {
            Mat rawMat = resultMats.get(i);
            Bitmap rawBitmap = Bitmap.createBitmap(rawMat.cols(),rawMat.rows(), Bitmap.Config.RGB_565);
            Utils.matToBitmap(rawMat, rawBitmap);
            letterResult = mLetterClassifier.classify(rawBitmap,answersRange);
            letterResult.setBoundRect(boundRects.get(i));
            letterResults.add(letterResult);
//            storeImage(rawBitmap,String.valueOf(alpha[letterResult.getPrediction()]));
        }
        if(letterResults.size() != rightAnswers.size()){
//            Log.e(TAG,"答案个数不匹配");
            Log.e(TAG,String.valueOf(letterResults.size()) + "  "+String.valueOf(rightAnswers.size()));
            return null;
        }
        Log.e(TAG,"答案匹配");
        //整理答案的顺序
        //打印整理前
//        for (int i = 0; i < letterResults.size(); i++) {
//            Log.e(TAG,"整理前 " + String.valueOf(letterResults.get(i).getPrediction()));
//        }
        Collections.sort(letterResults);
//        for (int i = 0; i < letterResults.size(); i++) {
//            Log.e(TAG,"整理后 " + String.valueOf(letterResults.get(i).getPrediction()));
//        }

        final Bitmap croppedBitmap = Bitmap.createBitmap((int) canvasWidth, (int) canvasHeight, Bitmap.Config.ARGB_8888);
        final Canvas canvas = new Canvas(croppedBitmap);


        Paint paintRightBox = new Paint();
        Paint paintWrongBox = new Paint();
        Paint paintPaperAnswer = new Paint();
        Paint paintRightAnswer = new Paint();
        Paint paintWrongRightAnswer = new Paint();

        //正确显示绿色
        //错误显示红色
        //正确答案在上方 颜色 绿色
        //错误答案下方 蓝色识别结果  正确答案在上面 红色


        paintRightBox.setStyle(Paint.Style.STROKE);
        paintRightBox.setColor(Color.argb(200,25,113,99));
        paintRightBox.setAntiAlias(true);
        paintRightBox.setStrokeWidth(5.0f);

        paintWrongBox.setStyle(Paint.Style.STROKE);
        paintWrongBox.setColor(Color.argb(200,215,56,94));
        paintWrongBox.setAntiAlias(true);
        paintWrongBox.setStrokeWidth(5.0f);

        paintPaperAnswer.setColor(Color.argb(200,61,126,166));
        paintPaperAnswer.setStyle(Paint.Style.FILL);
        paintPaperAnswer.setAntiAlias(true);
        paintPaperAnswer.setTextSize(60f);

        paintRightAnswer.setColor(Color.argb(200,25,113,99));
        paintRightAnswer.setStyle(Paint.Style.FILL);
        paintRightAnswer.setAntiAlias(true);
        paintRightAnswer.setTextSize(60f);

        paintWrongRightAnswer.setColor(Color.argb(200,215,56,94));
        paintWrongRightAnswer.setStyle(Paint.Style.FILL);
        paintWrongRightAnswer.setAntiAlias(true);
        paintWrongRightAnswer.setTextSize(60f);



        //绘制答案
        for (int m = 0; m <letterResults.size(); m++) {
            LetterResult curLetterResult = letterResults.get(m);
            Rect location = curLetterResult.getBoundRect();
            //判断该框框所识别的是正确的还是错误的
            int prediction = curLetterResult.getPrediction();
            if(prediction == -1){
                return null;
            }
            Character recognizedLetter = new Character(alpha[prediction]);
            Character rightLetter = rightAnswers.get(m);
            if(recognizedLetter.equals(rightLetter)){
                //正确
                //绘制框框
                canvas.drawRect(new Double(location.tl().x+startX).floatValue(),
                        new Double(location.tl().y+startY).floatValue(),
                        new Double(location.br().x+startX).floatValue(),
                        new Double(location.br().y+startY).floatValue(),paintRightBox);

                canvas.drawText(rightLetter.toString(), new Double(location.tl().x+startX).floatValue(), new Double(location.tl().y+startY-50).floatValue(), paintRightAnswer);

            }else{
                //错误
                //绘制框框
                canvas.drawRect(new Double(location.tl().x+startX).floatValue(),
                        new Double(location.tl().y+startY).floatValue(),
                        new Double(location.br().x+startX).floatValue(),
                        new Double(location.br().y+startY).floatValue(),paintWrongBox);

                //上面绘制正确答案
                canvas.drawText(rightLetter.toString(), new Double(location.tl().x+startX).floatValue(), new Double(location.tl().y+startY-50).floatValue(), paintWrongRightAnswer);

                //下方绘制识别结果
                canvas.drawText(recognizedLetter.toString(), new Double(location.tl().x+startX).floatValue(), new Double(location.br().y+startY+50).floatValue(), paintPaperAnswer);
            }
        }

        return croppedBitmap;
    }

//    private void storeImage(Bitmap image,String filelittlename) {
//        File pictureFile = getOutputMediaFile(filelittlename);
//        if (pictureFile == null) {
//            Log.d(TAG,
//                    "Error creating media file, check storage permissions: ");// e.getMessage());
//            return;
//        }
//        try {
//            FileOutputStream fos = new FileOutputStream(pictureFile);
//            image.compress(Bitmap.CompressFormat.PNG, 100, fos);
//            Log.e(TAG,"SAVE IT !"+pictureFile);
//            fos.close();
//        } catch (FileNotFoundException e) {
//            Log.d(TAG, "File not found: " + e.getMessage());
//        } catch (IOException e) {
//            Log.d(TAG, "Error accessing file: " + e.getMessage());
//        }
//    }
//
//    /** Create a File for saving an image or video */
//    private  File getOutputMediaFile(String filelittlename){
//        // To be safe, you should check that the SDCard is mounted
//        // using Environment.getExternalStorageState() before doing this.
//        File mediaStorageDir = new File(Environment.getExternalStorageDirectory()
//                + "/Android/data/"
//                + mActivity.getApplicationContext().getPackageName()
//                + "/Files");
//
//        // This location works best if you want the created images to be shared
//        // between applications and persist after your app has been uninstalled.
//
//        // Create the storage directory if it does not exist
//        if (! mediaStorageDir.exists()){
//            if (! mediaStorageDir.mkdirs()){
//                return null;
//            }
//        }
//        // Create a media file name
//        String timeStamp = new SimpleDateFormat("ddMMyyyy_HHmm").format(new Date());
//        File mediaFile;
//        String mImageName="MI_"+ timeStamp +filelittlename+".jpg";
//        mediaFile = new File(mediaStorageDir.getPath() + File.separator + mImageName);
//        return mediaFile;
//    }
}
