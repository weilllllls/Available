package com.scuavailable.available.scan;

import android.content.Context;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.viewpager.widget.PagerAdapter;

import com.scuavailable.available.R;

import java.util.List;

public class IndicatorPagerAdapter extends PagerAdapter {
    Context context;
    List<View> bottomList;
    final String TAG = "INDICATOR";
    public IndicatorPagerAdapter(Context context, List<View> bottomList) {
        this.context = context;
        this.bottomList = bottomList;
    }




    @Override
    public int getCount() {
        return bottomList.size();
    }

    @Override
    public boolean isViewFromObject(@NonNull View view, @NonNull Object object) {
        return view==object;
    }

    @NonNull
    @Override
    public Object instantiateItem(@NonNull ViewGroup container, int position) {
        View bottomView = bottomList.get(position);
        Log.e(TAG,"position"+ String.valueOf(position));
        container.addView(bottomView);
        return bottomView;
    }

    @Override
    public void destroyItem(@NonNull ViewGroup container, int position, @NonNull Object object) {
        View bottomView = bottomList.get(position);
        container.removeView(bottomView);
    }
}
