package com.scuavailable.available.scan;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Matrix;
import android.graphics.Paint;
import android.graphics.Point;
import android.graphics.RectF;
import android.graphics.drawable.BitmapDrawable;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.util.Size;
import android.view.View;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;

import com.github.chrisbanes.photoview.OnMatrixChangedListener;
import com.github.chrisbanes.photoview.OnPhotoTapListener;
import com.github.chrisbanes.photoview.PhotoView;
import com.scuavailable.available.R;
import com.scuavailable.available.util.JNIUtils;

import java.util.ArrayList;
import java.util.List;

public class PaperCountActivity extends AppCompatActivity {

    private static String TAG = "PaperCountActivity";
    ImageButton mBackIb;
//    private TextView mCurrMatrixTv;
    private TextView mCountNumberTv;
//    private Matrix mCurrentDisplayMatrix = null;

//    static final String PHOTO_TAP_TOAST_STRING = "Photo Tap! X: %.2f %% Y:%.2f %% ID: %d";
//    static final String SCALE_TOAST_STRING = "Scaled to: %.2ff";
//    static final String FLING_LOG_STRING = "Fling velocityX: %.2f, velocityY: %.2f";
    static final String INDICATE_COUNT_NUMBER = "共有 %d 张";
    private PhotoView mPhotoView;
    private Bitmap mNalBitmap;
    private List<Point>  dotList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_paper_count);
        dotList = new ArrayList<>();
        initViews();

    }

    private void initViews() {
//        mCurrMatrixTv = findViewById(R.id.tv_current_matrix);
        mCountNumberTv = findViewById(R.id.tv_count_number);
        mPhotoView = (PhotoView) findViewById(R.id.photo_view_count);
        Bundle extras = getIntent().getExtras();
//        String filename = extras.getString("filename");
//        String filefolder = extras.getString("filefolder");
        String filepath = extras.getString("filepath");

        mNalBitmap = BitmapFactory.decodeFile(filepath);
        if(mNalBitmap == null){
            Log.e(TAG,"this is none");
        }
//        mNalBitmap = BitmapFactory.decodeResource(getResources(),R.drawable.test);
        mPhotoView.setImageBitmap(mNalBitmap);
//        mPhotoView.setOnMatrixChangeListener(new MatrixChangeListener());
        mPhotoView.setOnPhotoTapListener(new PhotoTapListener());
        countBitmap(mNalBitmap);
        drawBitmap();





        mBackIb = findViewById(R.id.ib_count_back);
        mBackIb.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.e(TAG,"点击退出");
                finish();
            }
        });
    }

    private void drawBitmap() {
        BitmapFactory.Options options = new BitmapFactory.Options();
//        BitmapDrawable bitmapDrawable = (BitmapDrawable) mPhotoView.getDrawable();
//        Bitmap bitmap = bitmapDrawable.getBitmap();
        Log.i(TAG,String.valueOf(options.inSampleSize));

        int height = mNalBitmap.getHeight();
        int width = mNalBitmap.getWidth();
        Log.i(TAG,"draw height " + String.valueOf(height) + " width " + String.valueOf(width));

        //添加点
        Paint dotPaint = new Paint();
        dotPaint.setAntiAlias(true);
        dotPaint.setColor(Color.GREEN);
        Bitmap workingBitmap = Bitmap.createBitmap(mNalBitmap);
        Bitmap mutableBitmap = workingBitmap.copy(Bitmap.Config.ARGB_8888, true);

        Canvas canvas = new Canvas(mutableBitmap);
        for (int i = 0; i < dotList.size(); i++) {
            canvas.drawCircle(dotList.get(i).x, dotList.get(i).y, 15, dotPaint);
        }

        mPhotoView.setImageBitmap(mutableBitmap);
        mCountNumberTv.setText(String.format(INDICATE_COUNT_NUMBER, dotList.size()));

    }

    private void countBitmap(Bitmap bmp) {
        int onePointDistance = 15;
        int lastX = 0;
        int thisX = -1;
        int width = bmp.getWidth();
        int height = bmp.getHeight();
        int[] pix = new int[width * height];
        bmp.getPixels(pix, 0, width, 0, 0, width, height);
        int[] resultDots = JNIUtils.getCountPaperLocation(pix,width,height);
        if(resultDots.length > 2){
            Log.e(TAG,"DOTS LENGTH:" + String.valueOf(resultDots.length));
            int dotY = resultDots[2];
            for (int i = 3; i < resultDots.length; i++) {
                if(Math.abs(resultDots[i]-lastX)<onePointDistance){
                    continue;
                }
                lastX = resultDots[i];
                Point p = new Point(resultDots[i],dotY);
                dotList.add(p);
                Log.e(TAG,"first dot : X"+ String.valueOf(p.x) + "Y:" + String.valueOf(p.y));
            }
        }

    }

    private class PhotoTapListener implements OnPhotoTapListener {

        @Override
        public void onPhotoTap(ImageView view, float x, float y) {
            //x 与 y 有进行过归一化
            //获取图片宽高
            BitmapFactory.Options options = new BitmapFactory.Options();
            BitmapDrawable bitmapDrawable = (BitmapDrawable) mPhotoView.getDrawable();
            Bitmap bitmap = bitmapDrawable.getBitmap();
            int height = bitmap.getHeight();
            int width = bitmap.getWidth();
            Log.i(TAG," Touch height " + String.valueOf(height) + " width " + String.valueOf(width));
            int xPos = (int)(x*width);
            int yPos = (int)(y*height);
            Log.i(TAG," Touch xPos " + String.valueOf(xPos) + " yPos " + String.valueOf(yPos));
            Log.i(TAG," Touch x " + String.valueOf(x) + " y " + String.valueOf(y));

            //修改点
            Point p = new Point(xPos,yPos);
            boolean isRemove = false;
            for (int i = 0; i < dotList.size(); i++) {
                if(isDotIntersect(p,dotList.get(i))){
                    dotList.remove(i);
                    isRemove = true;
                    break;
                }
            }
            if(!isRemove){
                dotList.add(p);
            }
            //重新绘制
            drawBitmap();


            //改变计数

        }
    }



//    private class MatrixChangeListener implements OnMatrixChangedListener {
//        @Override
//        public void onMatrixChanged(RectF rect) {
//            mCurrMatrixTv.setText(rect.toString());
//        }
//    }

    private boolean isDotIntersect(Point p1,Point p2){
        int distanceThreshold = 20;
        Log.e(TAG,"p1 x "+ String.valueOf(p1.x) + " p1 y" + String.valueOf(p1.y) );
        Log.e(TAG,"p2 x "+ String.valueOf(p1.x) + " p2 y" + String.valueOf(p1.y) );
        int d1 = Math.abs(p1.x - p2.x);
        int d2 = Math.abs(p1.y - p2.y);
        Log.e(TAG,"d1   "+ String.valueOf(d1) + " d2  " + String.valueOf(d2) );

        if(d1 <= distanceThreshold && d2 <= distanceThreshold){
            Log.e(TAG,"is intersect");
            return true;
        }else{
            Log.e(TAG,"is not intersect");
            return false;
        }
    }

}
