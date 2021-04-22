package com.scuavailable.available;


import android.annotation.SuppressLint;
import android.content.Context;
import android.content.res.AssetFileDescriptor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;

import org.tensorflow.lite.Interpreter;

import java.io.FileInputStream;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.MappedByteBuffer;
import java.nio.channels.FileChannel;
import java.util.ArrayList;
import java.util.Objects;

public class TFLiteLoader {
    Interpreter tflite;
    Context context;

    public TFLiteLoader(Context context){
        this.context = context;
        try {
            tflite = new Interpreter(loadModelFile());
        } catch (Exception ex) {
            ex.printStackTrace();
        }


    }

    public int process(Bitmap bitmap){
        Bitmap resized = Bitmap.createScaledBitmap(bitmap, 28, 28, true);
        ByteBuffer buff = bitmapToModelsMatchingByteBuffer(resized);
        return runInferenceOnFloatModel(buff);
    }


    @SuppressLint("DefaultLocale")
    private int runInferenceOnFloatModel(ByteBuffer byteBufferToClassify) {
        float[][] result = new float[1][10];
        tflite.run(byteBufferToClassify, result);
        int maxLoc = 0;
        float[] resultVals = result[0];
        float maxVal = resultVals[0];
        int length = resultVals.length;
        for (int i = 0; i < length; i++) {
//            Log.e("DIGIT PROCESS REUSLTcc:",String.valueOf(resultVals[i]) + "~~~" + String.valueOf(maxVal)+"~~~~"+String.valueOf(i));

            if(resultVals[i] > maxVal){
                maxVal = resultVals[i];
                maxLoc = i;
            }
        }
        return maxLoc;

    }


    private MappedByteBuffer loadModelFile() throws IOException {
        AssetFileDescriptor fileDescriptor = context.getAssets().openFd("mnist_model.tflite");
        FileInputStream inputStream = new FileInputStream(fileDescriptor.getFileDescriptor());
        FileChannel fileChannel = inputStream.getChannel();
        long startOffset = fileDescriptor.getStartOffset();
        long declaredLength = fileDescriptor.getDeclaredLength();
        return fileChannel.map(FileChannel.MapMode.READ_ONLY, startOffset, declaredLength);
    }

    private ByteBuffer bitmapToModelsMatchingByteBuffer(Bitmap bitmap) {
        int SIZE = 28;
        ByteBuffer byteBuffer = ByteBuffer.allocateDirect(SIZE * SIZE * 4);
        byteBuffer.order(ByteOrder.nativeOrder());
        int[] intValues = new int[SIZE * SIZE];
        bitmap.getPixels(intValues, 0, bitmap.getWidth(), 0, 0, bitmap.getWidth(), bitmap.getHeight());
        int pixel = 0;
        for (int i = 0; i < SIZE; ++i) {
            for (int j = 0; j < SIZE; ++j) {
                int pixelVal = intValues[pixel++];
                for (float channelVal : pixelToChannelValue(pixelVal)) {
                    byteBuffer.putFloat(channelVal);
                }
            }
        }
        return byteBuffer;
    }

    private float[] pixelToChannelValue(int pixel) {
        float[] singleChannelVal = new float[1];
        float rChannel = (pixel >> 16) & 0xFF;
        float gChannel = (pixel >> 8) & 0xFF;
        float bChannel = (pixel) & 0xFF;
        singleChannelVal[0] = (rChannel + gChannel + bChannel) / 3 / 255.f;
        return singleChannelVal;
    }
}