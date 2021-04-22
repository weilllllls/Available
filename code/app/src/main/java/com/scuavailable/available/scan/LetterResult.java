package com.scuavailable.available.scan;

import android.util.Log;

import org.opencv.core.Rect;

import java.util.ArrayList;

public class LetterResult implements Comparable{
    private final String TAG="LetterResult";
    private final int mPrediction;
    private final float mProbability;
    private final long mTimeCost;


    private ArrayList<Integer> mProbsRange;

    private Rect mBoundRect;

    public LetterResult(float[] probs, long timeCost, ArrayList<Integer> probsRange) {

        mProbsRange = probsRange;
        mPrediction = argmax(probs);
        mProbability = probs[mPrediction];
        mTimeCost = timeCost;
    }

    public int getPrediction() {
        return mPrediction;
    }

    public Rect getBoundRect() {
        return mBoundRect;
    }

    public void setBoundRect(Rect BoundRect) {
        this.mBoundRect = BoundRect;
    }

    public float getProbability() {
        return mProbability;
    }

    public long getTimeCost() {
        return mTimeCost;
    }

    private int argmax(float[] probs) {
        int maxIdx = -1;
        float maxProb = 0.0f;
        for (int i = 0; i < probs.length; i++) {
            Log.e(TAG,String.valueOf(probs[i])+"  " + String.valueOf(i));
            if (probs[i] > maxProb && mProbsRange.contains(i)) {
                maxProb = probs[i];
                maxIdx = i;
            }
        }
        return maxIdx;
    }

    @Override
    public int compareTo(Object o) {

        LetterResult lr = (LetterResult) o;
        if (this.mBoundRect.br().x > lr.getBoundRect().br().x) {
            return 1;
        }else if (this.mBoundRect.br().x < lr.getBoundRect().br().x) {
            return -1;
        }else {
            return 0;
        }
    }
}