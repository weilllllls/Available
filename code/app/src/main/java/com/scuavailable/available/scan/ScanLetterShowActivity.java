package com.scuavailable.available.scan;

import androidx.appcompat.app.AppCompatActivity;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ImageButton;

import com.github.chrisbanes.photoview.PhotoView;
import com.scuavailable.available.R;

public class ScanLetterShowActivity extends AppCompatActivity {
    private PhotoView mPhotoView;
    private ImageButton mBackBtn;
    private Bitmap mBitmap;
    private static String TAG = "ScanLetterShowActivity";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_scan_letter_show);
        initViews();
        initImage();
    }

    private void initImage() {
        Bundle extras = getIntent().getExtras();
//        String filename = extras.getString("filename");
//        String filefolder = extras.getString("filefolder");
        String filepath = extras.getString("filepath");

        mBitmap = BitmapFactory.decodeFile(filepath);
        if(mBitmap == null){
            Log.e(TAG,"this is none");
        }
//        mNalBitmap = BitmapFactory.decodeResource(getResources(),R.drawable.test);
        mPhotoView.setImageBitmap(mBitmap);
    }

    private void initViews() {
        mPhotoView = findViewById(R.id.photo_view_letter_show);
        mBackBtn = findViewById(R.id.ib_letter_show_back);
        mBackBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });


    }
}
