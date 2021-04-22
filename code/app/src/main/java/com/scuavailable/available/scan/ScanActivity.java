package com.scuavailable.available.scan;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;
import androidx.viewpager.widget.ViewPager;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.ImageFormat;
import android.graphics.Rect;
import android.graphics.SurfaceTexture;
import android.graphics.drawable.BitmapDrawable;
import android.hardware.camera2.CameraAccessException;
import android.hardware.camera2.CameraCaptureSession;
import android.hardware.camera2.CameraCharacteristics;
import android.hardware.camera2.CameraDevice;
import android.hardware.camera2.CameraManager;
import android.hardware.camera2.CameraMetadata;
import android.hardware.camera2.CaptureRequest;
import android.hardware.camera2.TotalCaptureResult;
import android.hardware.camera2.params.StreamConfigurationMap;
import android.media.Image;
import android.media.ImageReader;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.HandlerThread;
import android.util.Log;
import android.util.Size;
import android.util.SparseIntArray;
import android.view.Surface;
import android.view.TextureView;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;


import com.rd.PageIndicatorView;
import com.scuavailable.available.R;
import com.scuavailable.available.customizedView.AutoFitTextureView;
import com.scuavailable.available.util.AvaUtils;

import org.checkerframework.common.value.qual.StringVal;
import org.opencv.osgi.OpenCVNativeLoader;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.nio.ByteBuffer;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.List;


public class ScanActivity extends AppCompatActivity {

    private static String TAG = "ScanActivity";
    public enum ScanMode{
        COUNTMODE,LETTERMODE,NUMBERMODE;
    }
    private boolean runDigitClassifier = false;
    private boolean runLetterClassifier = false;

    int LETTER_SETTING_REQUEST_CODE = 1;
    int LETTER_SETTING_OK = 100;
    //
    private ScanMode MODE_CODE = ScanMode.COUNTMODE;
    private Context mContext;
    ImageButton mBackIb;
    AutoFitTextureView mTextureView;
    ViewPager mViewPager;
    PageIndicatorView mPageIndicatorView;
    IndicatorPagerAdapter indicatorPagerAdapter;
    ImageView mDisplayIv;
    View mUpperLine,mLowerLine,mVerticalLine;
    TextView mIndexTv,mInformTv;
    ImageButton mCountTakeIb,mLetterTakeIb,mNumberTakeIb,mLetterSettingIb,mLetterNextIb;

    private List<View> mBottomViewList;



    private static final SparseIntArray ORIENTATIONS = new SparseIntArray();
    static {
        ORIENTATIONS.append(Surface.ROTATION_0, 90);
        ORIENTATIONS.append(Surface.ROTATION_90, 0);
        ORIENTATIONS.append(Surface.ROTATION_180, 270);
        ORIENTATIONS.append(Surface.ROTATION_270, 180);
    }

    private String cameraId;
    protected CameraDevice cameraDevice;
    protected CameraCaptureSession cameraCaptureSessions;
    protected CaptureRequest captureRequest;
    protected CaptureRequest.Builder captureRequestBuilder;
    private Size imageDimension;

    private ImageReader imageReader;
    private String mFileFolderName;
    private String mFilename;
    private static final int REQUEST_CAMERA_PERMISSION = 200;
    private Handler mBackgroundHandler;
    private HandlerThread mBackgroundThread;

    View countInflate,letterInflate,numberInflate;

    private DigitProcessService digitProcessService;
    private LetterProcessService letterProcessService;


    private final Object lock = new Object();
    private static float canvasWidth = 100;
    private static float canvasHeight = 100;

    ArrayList<Character> rightAnswers;
    ArrayList<Character> rightAnswersSection;
    int answerIndex = 0;
    int ANSWER_NUM_PER_LINE = 5;
    ArrayList<Integer> answersRange;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_scan);
        new OpenCVNativeLoader().init();
        Log.i(TAG,"Create");
        mContext =  this;
        initViews();
        initData();
        initPager();
        initService();

    }

    private void initData() {
        rightAnswers = new ArrayList<Character>();
//        rightAnswers.add('A');
//        rightAnswers.add('B');
//        rightAnswers.add('C');
//        rightAnswers.add('D');
//        rightAnswers.add('A');
        rightAnswersSection = new ArrayList<Character>();
//        rightAnswersSection.add('A');
//        rightAnswersSection.add('B');
//        rightAnswersSection.add('C');
//        rightAnswersSection.add('D');
//        rightAnswersSection.add('A');
        answersRange = new ArrayList<Integer>();
        answersRange.add(0);
        answersRange.add(1);
        answersRange.add(2);
        answersRange.add(3);
        answerIndex = 0;
        configAnswerSection();
    }

    private void initService() {
            letterProcessService = new LetterProcessService(this);
            digitProcessService = new DigitProcessService(mContext);
    }

    private void initPager() {


        mBottomViewList = new ArrayList<>();
        mBottomViewList.add(countInflate);
        mBottomViewList.add(letterInflate);
        mBottomViewList.add(numberInflate);

        indicatorPagerAdapter = new IndicatorPagerAdapter(mContext,mBottomViewList);
        mViewPager.setAdapter(indicatorPagerAdapter);

        mViewPager.addOnPageChangeListener(new ViewPager.OnPageChangeListener() {
            @Override
            public void onPageScrolled(int position, float positionOffset, int positionOffsetPixels) {

            }

            @Override
            public void onPageSelected(int position) {
                Log.e(TAG,"positon::" + String.valueOf(position));

                mPageIndicatorView.setSelection(position);

                switch (position){
                    case 0:
                        Log.e(TAG,"份数检测模式");
                        stopCurrentMode();
                        startCountMode();
                        //份数检测
                        break;
                    case 1:
                        Log.e(TAG,"客观题模式");
                        stopCurrentMode();
                        startLetterMode();
                        //客观题识别
                        break;
                    case 2:
                        //分数统计
                        Log.e(TAG,"分数统计模式");
                        stopCurrentMode();
                        startNumberMode();
                        break;
                    default:
                        Log.e(TAG,"default模式");
                        break;

                }
            }

            @Override
            public void onPageScrollStateChanged(int state) {

            }
        });
    }

    private void stopCurrentMode() {

        mUpperLine.setVisibility(View.GONE);
        mLowerLine.setVisibility(View.GONE);
        mDisplayIv.setVisibility(View.GONE);
        mVerticalLine.setVisibility(View.GONE);
        mInformTv.setVisibility(View.GONE);
        mDisplayIv.setImageDrawable(null);

    }

    private void startLetterMode() {

        MODE_CODE = ScanMode.LETTERMODE;

        int upperHeight = (int)(mTextureView.getHeight()*0.20);
        int lowerHeight = (int)(mTextureView.getHeight()*0.28);
        int leftWidth = (int)(mTextureView.getWidth()*0.20);

        int verticalHeight = lowerHeight-upperHeight;
        RelativeLayout.LayoutParams params =(RelativeLayout.LayoutParams) mVerticalLine.getLayoutParams();
        params.height = verticalHeight;
        mVerticalLine.setLayoutParams(params);
        mVerticalLine.setTranslationY(upperHeight);
        mVerticalLine.setTranslationX(leftWidth);
        mUpperLine.setTranslationY(upperHeight);
        mLowerLine.setTranslationY(lowerHeight);

        mUpperLine.setVisibility(View.VISIBLE);
        mLowerLine.setVisibility(View.VISIBLE);
        mDisplayIv.setVisibility(View.VISIBLE);
        mVerticalLine.setVisibility(View.VISIBLE);

        mInformTv.setTranslationY(upperHeight-mInformTv.getTextSize()*3);
        mInformTv.setText("请将客观题答题置于框框内");
        mInformTv.setVisibility(View.VISIBLE);
    }

    private void startCountMode() {
        MODE_CODE = ScanMode.COUNTMODE;

    }

    private void startNumberMode() {
        MODE_CODE = ScanMode.NUMBERMODE;
        int upperHeight = (int)(mTextureView.getHeight()*0.13);
        int lowerHeight = (int)(mTextureView.getHeight()*0.18);
        mUpperLine.setTranslationY(upperHeight);
        mLowerLine.setTranslationY(lowerHeight);
        mUpperLine.setVisibility(View.VISIBLE);
        mLowerLine.setVisibility(View.VISIBLE);
        mDisplayIv.setVisibility(View.VISIBLE);

        mInformTv.setTranslationY(upperHeight-mInformTv.getTextSize()*2);
        mInformTv.setText("请将分数栏置于框框内");
        mInformTv.setVisibility(View.VISIBLE);

        Log.e(TAG,"upper " + String.valueOf(upperHeight) + "lower " + String.valueOf(lowerHeight));


//        BitmapFactory.Options opts = new BitmapFactory.Options();
//        opts.inPreferredConfig = Bitmap.Config.ARGB_8888;
//        Bitmap bitmap = BitmapFactory.decodeResource(getResources(),R.drawable.test_digit,opts);
//        digitProcessService.processFrame(bitmap);

    }

    private void initViews() {
        mBackIb = findViewById(R.id.ib_scan_back);
        mTextureView = findViewById(R.id.textureview_scan);

        mDisplayIv = findViewById(R.id.iv_display_scan);
        mUpperLine = findViewById(R.id.view_upper_ROI_line_scan);
        mLowerLine = findViewById(R.id.view_lower_ROI_line_scan);
        mVerticalLine = findViewById(R.id.view_vertical_ROI_line_scan);

        mTextureView.setSurfaceTextureListener(textureListener);

        countInflate = getLayoutInflater().inflate(R.layout.item_scan_count_bottom,null);
        letterInflate = getLayoutInflater().inflate(R.layout.item_scan_letter_bottom,null);
        numberInflate = getLayoutInflater().inflate(R.layout.item_scan_number_bottom,null);

        mCountTakeIb=  countInflate.findViewById(R.id.ib_item_scan_count_take);
        mLetterTakeIb  =  letterInflate.findViewById(R.id.ib_item_scan_letter_take);
        mNumberTakeIb  = numberInflate.findViewById(R.id.ib_item_scan_number_take);
        mLetterSettingIb = letterInflate.findViewById(R.id.ib_item_scan_letter_setting);
        mLetterNextIb = letterInflate.findViewById(R.id.ib_item_scan_letter_next);
        mIndexTv = letterInflate.findViewById(R.id.tv_item_scan_letter_index);

        mInformTv = findViewById(R.id.tv_scan_inform);
        mViewPager = findViewById(R.id.vp_scan);

        mPageIndicatorView = findViewById(R.id.pageIndicatorView);

        mBackIb.setOnClickListener(scanClickListener);
        mCountTakeIb.setOnClickListener(scanClickListener);
        mLetterTakeIb.setOnClickListener(scanClickListener);
        mNumberTakeIb.setOnClickListener(scanClickListener);
        mLetterSettingIb.setOnClickListener(scanClickListener);
        mLetterNextIb.setOnClickListener(scanClickListener);

    }




    View.OnClickListener scanClickListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            Log.e(TAG,"click view id" + String.valueOf(v.getId()));

            switch (v.getId()) {
                case R.id.ib_scan_back:
                    finish();
                    break;
                case R.id.ib_item_scan_count_take:
//                    takePicture();
                    paperCount();
                    break;
                case R.id.ib_item_scan_letter_take:
//                    Toast.makeText(mContext,"ib_item_scan_letter_take",Toast.LENGTH_SHORT).show();
                    letterTake();
                    break;
                case R.id.ib_item_scan_number_take:
//                    Toast.makeText(mContext,"ib_item_scan_number_take",Toast.LENGTH_SHORT).show();
                    numberTake();
                    break;
                case R.id.ib_item_scan_letter_setting:
//                    Toast.makeText(mContext,"ib_item_scan_letter_setting",Toast.LENGTH_SHORT).show();
                    Intent intent = new Intent(mContext, LetterSettingActivity.class);
                    startActivityForResult(intent,LETTER_SETTING_REQUEST_CODE);
                    break;
                case R.id.ib_item_scan_letter_next:
                    configAnswerSection();
                    break;
            }
        }
    };

    private void numberTake() {
        if ( cameraDevice == null) {
            return;
        }

        Bitmap backBitmap = mTextureView.getBitmap();
        mDisplayIv.setDrawingCacheEnabled(true);
        Bitmap frontBitmap = Bitmap.createBitmap(mDisplayIv.getDrawingCache());
        mDisplayIv.setDrawingCacheEnabled(false);

        Bitmap merge = mergeBitmap(backBitmap, frontBitmap);
        String path = storeImage(merge,"number_take_");
        Intent intent = new Intent(mContext, ScanNumberShowActivity.class);
        intent.putExtra("filepath",path);
        startActivity(intent);
    }

    private void letterTake() {
        if ( cameraDevice == null) {
            return;
        }
        Bitmap backBitmap = mTextureView.getBitmap();
        mDisplayIv.setDrawingCacheEnabled(true);
        Bitmap frontBitmap = Bitmap.createBitmap(mDisplayIv.getDrawingCache());
        mDisplayIv.setDrawingCacheEnabled(false);

        Bitmap merge = mergeBitmap(backBitmap, frontBitmap);
        String path = storeImage(merge,"number_take_");
        Intent intent = new Intent(mContext, ScanLetterShowActivity.class);
        intent.putExtra("filepath",path);
        startActivity(intent);
    }

    private void paperCount() {
        if ( cameraDevice == null) {
            return;
        }

        Bitmap bitmap = mTextureView.getBitmap();
        String path = storeImage(bitmap,"paper_count_");
        Intent intent = new Intent(mContext, PaperCountActivity.class);
        intent.putExtra("filepath",path);
        startActivity(intent);
    }
    public static Bitmap mergeBitmap(Bitmap backBitmap, Bitmap frontBitmap) {

        if (backBitmap == null || backBitmap.isRecycled()
                || frontBitmap == null || frontBitmap.isRecycled()) {
            Log.e(TAG, "backBitmap=" + backBitmap + ";frontBitmap=" + frontBitmap);
            return null;
        }
        Bitmap bitmap = backBitmap.copy(Bitmap.Config.ARGB_8888, true);
        Canvas canvas = new Canvas(bitmap);
        Rect baseRect  = new Rect(0, 0, backBitmap.getWidth(), backBitmap.getHeight());
        Rect frontRect = new Rect(0, 0, frontBitmap.getWidth(), frontBitmap.getHeight());
        canvas.drawBitmap(frontBitmap, frontRect, baseRect, null);
        return bitmap;
    }
    private String storeImage(Bitmap image,String nickname) {
        File pictureFile = getOutputMediaFile(nickname);
        if (pictureFile == null) {
            Log.d(TAG,
                    "Error creating media file, check storage permissions: ");// e.getMessage());
            return null;
        }
        try {
            FileOutputStream fos = new FileOutputStream(pictureFile);
            image.compress(Bitmap.CompressFormat.PNG, 100, fos);
            Log.e(TAG,"SAVE IT !"+pictureFile);
            fos.close();
            Log.e(TAG,pictureFile.getAbsolutePath()+"  " + pictureFile.getName() + "  "+ pictureFile.getPath()  );
        } catch (FileNotFoundException e) {
            Log.d(TAG, "File not found: " + e.getMessage());
        } catch (IOException e) {
            Log.d(TAG, "Error accessing file: " + e.getMessage());
        }finally {
            return pictureFile.getPath();
        }

    }

    /** Create a File for saving an image or video */
    private  File getOutputMediaFile(String nickname){
        // To be safe, you should check that the SDCard is mounted
        // using Environment.getExternalStorageState() before doing this.
        File mediaStorageDir = new File(Environment.getExternalStorageDirectory()
                + "/Android/data/"
                + getApplicationContext().getPackageName()
                + "/Files");

        // This location works best if you want the created images to be shared
        // between applications and persist after your app has been uninstalled.

        // Create the storage directory if it does not exist
        if (! mediaStorageDir.exists()){
            if (! mediaStorageDir.mkdirs()){
                return null;
            }
        }
        // Create a media file name
        String timeStamp = new SimpleDateFormat("ddMMyyyy_HHmm").format(new Date());
        File mediaFile;
        String mImageName="Available_"+ nickname+timeStamp +".jpg";
        mediaFile = new File(mediaStorageDir.getPath() + File.separator + mImageName);
        return mediaFile;
    }


    TextureView.SurfaceTextureListener textureListener = new TextureView.SurfaceTextureListener() {
        @Override
        public void onSurfaceTextureAvailable(SurfaceTexture surface, int width, int height) {
            //open your camera here
            openCamera();
        }
        @Override
        public void onSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height) {
            // Transform you image captured size according to the surface width and height
        }
        @Override
        public boolean onSurfaceTextureDestroyed(SurfaceTexture surface) {
            return false;
        }
        @Override
        public void onSurfaceTextureUpdated(SurfaceTexture surface) {
        }
    };
    private final CameraDevice.StateCallback stateCallback = new CameraDevice.StateCallback() {
        @Override
        public void onOpened(CameraDevice camera) {
            //This is called when the camera is open
            Log.e(TAG, "onOpened");
            cameraDevice = camera;
            createCameraPreview();
        }
        @Override
        public void onDisconnected(CameraDevice camera) {
            cameraDevice.close();
            cameraDevice = null;
        }
        @Override
        public void onError(CameraDevice camera, int error) {
            cameraDevice.close();
            cameraDevice = null;
        }
    };

    final CameraCaptureSession.CaptureCallback captureCallbackListener = new CameraCaptureSession.CaptureCallback() {
        @Override
        public void onCaptureCompleted(CameraCaptureSession session, CaptureRequest request, TotalCaptureResult result) {
            super.onCaptureCompleted(session, request, result);
            createCameraPreview();
        }
    };

    protected void startBackgroundThread() {
        mBackgroundThread = new HandlerThread("Camera Background");
        mBackgroundThread.start();
        mBackgroundHandler = new Handler(mBackgroundThread.getLooper());

        runDigitClassifier = false;
        runLetterClassifier = false;
        if(MODE_CODE == ScanMode.NUMBERMODE){
            synchronized (lock) {
                runDigitClassifier = true;
            }
        }
        if(MODE_CODE == ScanMode.LETTERMODE){
            synchronized (lock) {
                runLetterClassifier = true;
            }
        }
        mBackgroundHandler.post(periodicClassify);
    }

    protected void stopBackgroundThread() {
        mBackgroundThread.quitSafely();
        try {
            mBackgroundThread.join();
            mBackgroundThread = null;
            mBackgroundHandler = null;
            synchronized (lock) {
                runDigitClassifier = false;
                runLetterClassifier = false;
            }
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }


    protected void takePicture() {
        if(null == cameraDevice) {
            Log.e(TAG, "cameraDevice is null");
            return;
        }
        CameraManager manager = (CameraManager) getSystemService(Context.CAMERA_SERVICE);
        try {
            CameraCharacteristics characteristics = manager.getCameraCharacteristics(cameraDevice.getId());
            Size[] jpegSizes = null;
            if (characteristics != null) {
                jpegSizes = characteristics.get(CameraCharacteristics.SCALER_STREAM_CONFIGURATION_MAP).getOutputSizes(ImageFormat.JPEG);
            }
            int width = 640;
            int height = 480;
            if (jpegSizes != null && 0 < jpegSizes.length) {
                width = jpegSizes[0].getWidth();
                height = jpegSizes[0].getHeight();
            }
            ImageReader reader = ImageReader.newInstance(width, height, ImageFormat.JPEG, 1);
            List<Surface> outputSurfaces = new ArrayList<Surface>(2);
            outputSurfaces.add(reader.getSurface());
            outputSurfaces.add(new Surface(mTextureView.getSurfaceTexture()));
            final CaptureRequest.Builder captureBuilder = cameraDevice.createCaptureRequest(CameraDevice.TEMPLATE_STILL_CAPTURE);
            captureBuilder.addTarget(reader.getSurface());
            captureBuilder.set(CaptureRequest.CONTROL_MODE, CameraMetadata.CONTROL_MODE_AUTO);
            // Orientation
            int rotation = getWindowManager().getDefaultDisplay().getRotation();
            captureBuilder.set(CaptureRequest.JPEG_ORIENTATION, ORIENTATIONS.get(rotation));
            ImageReader.OnImageAvailableListener readerListener = new ImageReader.OnImageAvailableListener() {
                @Override
                public void onImageAvailable(ImageReader reader) {
                    Image image = null;
                    try {
                        image = reader.acquireLatestImage();
                        if (image == null){
                            Log.e(TAG,"get null");
                            return;
                        }
                        ByteBuffer buffer = image.getPlanes()[0].getBuffer();
                        byte[] bytes = new byte[buffer.remaining()];
                        buffer.get(bytes);
                        //调用jni进行计数
                        // 返回的坐标放入intent
                        // byte[]放入intent 跳转
                        save(bytes);
                    } catch (FileNotFoundException e) {
                        e.printStackTrace();
                    } catch (IOException e) {
                        e.printStackTrace();
                    } finally {
                        if (image != null) {
                            image.close();
                        }
                    }
                }
                private void save(byte[] bytes) throws IOException {
                    Date date = new Date();
                    SimpleDateFormat format = new SimpleDateFormat("yyyyMMddHHmmss");
                    mFilename = "paper" +format.format(date) + ".jpg";
                    mFileFolderName = AvaUtils.getCachePath(mContext) + "/paperCount/";
                    Log.e(TAG,mFileFolderName);
                    Log.e(TAG,mFilename);
                    File fileFolder = new File(mFileFolderName);
                    if(!fileFolder.exists()){
                        Log.e(TAG,"create filedirectory");
                        fileFolder.mkdirs();
                    }
                    File jpgFile = new File(fileFolder,mFilename);
                    OutputStream output = null;
                    try {
                        output = new FileOutputStream(jpgFile);
                        output.write(bytes);
                        Log.e(TAG,"save");
                    } finally {
                        if (null != output) {
                            output.close();
                        }
                    }
                }
            };
            reader.setOnImageAvailableListener(readerListener, mBackgroundHandler);
            final CameraCaptureSession.CaptureCallback captureListener = new CameraCaptureSession.CaptureCallback() {
                @Override
                public void onCaptureCompleted(CameraCaptureSession session, CaptureRequest request, TotalCaptureResult result) {
                    super.onCaptureCompleted(session, request, result);
//                    createCameraPreview();
                    Intent intent = new Intent(mContext, PaperCountActivity.class);
                    intent.putExtra("filename",mFilename);
                    intent.putExtra("filefolder",mFileFolderName);
                    startActivity(intent);
                }
            };
            cameraDevice.createCaptureSession(outputSurfaces, new CameraCaptureSession.StateCallback() {
                @Override
                public void onConfigured(CameraCaptureSession session) {
                    try {
                        session.capture(captureBuilder.build(), captureListener, mBackgroundHandler);
                    } catch (CameraAccessException e) {
                        e.printStackTrace();
                    }
                }
                @Override
                public void onConfigureFailed(CameraCaptureSession session) {
                }
            }, mBackgroundHandler);
        } catch (CameraAccessException e) {
            e.printStackTrace();
        }
    }


    protected void createCameraPreview() {
        try {
            SurfaceTexture texture = mTextureView.getSurfaceTexture();
            assert texture != null;
            texture.setDefaultBufferSize(imageDimension.getWidth(), imageDimension.getHeight());
            Surface surface = new Surface(texture);
            captureRequestBuilder = cameraDevice.createCaptureRequest(CameraDevice.TEMPLATE_PREVIEW);
            captureRequestBuilder.addTarget(surface);
            cameraDevice.createCaptureSession(Arrays.asList(surface), new CameraCaptureSession.StateCallback(){
                @Override
                public void onConfigured(@NonNull CameraCaptureSession cameraCaptureSession) {
                    //The camera is already closed
                    if (null == cameraDevice) {
                        return;
                    }
                    // When the session is ready, we start displaying the preview.
                    cameraCaptureSessions = cameraCaptureSession;
                    updatePreview();
                }
                @Override
                public void onConfigureFailed(@NonNull CameraCaptureSession cameraCaptureSession) {
                    Toast.makeText(ScanActivity.this, "Configuration change", Toast.LENGTH_SHORT).show();
                }
            }, null);
        } catch (CameraAccessException e) {
            e.printStackTrace();
        }
    }

    private void openCamera() {
        CameraManager manager = (CameraManager) getSystemService(Context.CAMERA_SERVICE);
        Log.e(TAG, "is camera open");
        try {
            cameraId = manager.getCameraIdList()[0];
            CameraCharacteristics characteristics = manager.getCameraCharacteristics(cameraId);
            StreamConfigurationMap map = characteristics.get(CameraCharacteristics.SCALER_STREAM_CONFIGURATION_MAP);
            assert map != null;
            imageDimension = map.getOutputSizes(SurfaceTexture.class)[0];


            // Add permission for camera and let user grant the permission
            if (ActivityCompat.checkSelfPermission(this, Manifest.permission.CAMERA) != PackageManager.PERMISSION_GRANTED
                    && ActivityCompat.checkSelfPermission(this, Manifest.permission.WRITE_EXTERNAL_STORAGE) != PackageManager.PERMISSION_GRANTED
                    && ActivityCompat.checkSelfPermission(this,Manifest.permission.READ_EXTERNAL_STORAGE) != PackageManager.PERMISSION_GRANTED)
             {
                ActivityCompat.requestPermissions(ScanActivity.this, new String[]{Manifest.permission.CAMERA, Manifest.permission.WRITE_EXTERNAL_STORAGE,Manifest.permission.READ_EXTERNAL_STORAGE}, REQUEST_CAMERA_PERMISSION);
                return;
            }
            manager.openCamera(cameraId, stateCallback, null);
        } catch (CameraAccessException e) {
            e.printStackTrace();
        }
        Log.e(TAG, "openCamera X");
    }
    protected void updatePreview() {
        if(null == cameraDevice) {
            Log.e(TAG, "updatePreview error, return");
        }
        captureRequestBuilder.set(CaptureRequest.CONTROL_MODE, CameraMetadata.CONTROL_MODE_AUTO);
        try {
            cameraCaptureSessions.setRepeatingRequest(captureRequestBuilder.build(), null, mBackgroundHandler);
        } catch (CameraAccessException e) {
            e.printStackTrace();
        }
    }
    private void closeCamera() {
        if (null != cameraDevice) {
            cameraDevice.close();
            cameraDevice = null;
        }
        if (null != imageReader) {
            imageReader.close();
            imageReader = null;
        }
    }
    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        if (requestCode == REQUEST_CAMERA_PERMISSION) {
            if (grantResults[0] == PackageManager.PERMISSION_DENIED) {
                // close the app
                Toast.makeText(ScanActivity.this, "Sorry!!!, you can't use this app without granting permission", Toast.LENGTH_LONG).show();
                finish();
            }
        }
    }
    @Override
    protected void onResume() {
        super.onResume();
        Log.e(TAG, "onResume");
        startBackgroundThread();
        if (mTextureView.isAvailable()) {
            openCamera();
        } else {
            mTextureView.setSurfaceTextureListener(textureListener);
        }

    }
    @Override
    protected void onPause() {
        Log.e(TAG, "onPause");
        closeCamera();
        stopBackgroundThread();
        super.onPause();
    }


    private Runnable periodicClassify =
            new Runnable() {
                @Override
                public void run() {
                    synchronized (lock) {
                        if (MODE_CODE == ScanMode.NUMBERMODE) {
//                            Log.e(TAG,"NUMBERperiodicClassify");
                            classifyNumberFrame();
                        }
                        if(MODE_CODE == ScanMode.LETTERMODE){
//                            Log.e(TAG,"LetterRperiodicClassify");
                            classifyLetterFrame();
                        }
                    }
                    mBackgroundHandler.post(periodicClassify);
                }
            };

    private void classifyLetterFrame() {
        if (letterProcessService == null  || cameraDevice == null) {
//            Log.e(TAG,"classifyError");
            return;
        }

        Bitmap bitmap = mTextureView.getBitmap();
        canvasWidth = mTextureView.getWidth();
        canvasHeight = mTextureView.getHeight();
        mDisplayIv.getLayoutParams().width = mTextureView.getWidth();
        mDisplayIv.getLayoutParams().height = mTextureView.getHeight();

        final Bitmap processedBitmap = letterProcessService.processFrame(bitmap, canvasWidth, canvasHeight,rightAnswersSection,answersRange);
        bitmap.recycle();
        if(processedBitmap != null){
//            Log.e(TAG,"LETTER NOT NULL");
            mDisplayIv.post(new Runnable() {
                @Override
                public void run() {
                    mDisplayIv.setImageBitmap(processedBitmap);
                }
            });
        }else{
//            Log.e(TAG,"LETTER NULL");
        }
    }

    private void classifyNumberFrame() {
        if (digitProcessService == null  || cameraDevice == null) {
//            Log.e(TAG,"classifyError");
            return;
        }

        Bitmap bitmap = mTextureView.getBitmap();
        canvasWidth = mTextureView.getWidth();
        canvasHeight = mTextureView.getHeight();
        mDisplayIv.getLayoutParams().width = mTextureView.getWidth();
        mDisplayIv.getLayoutParams().height = mTextureView.getHeight();

        final Bitmap processedBitmap = digitProcessService.processFrame(bitmap, canvasWidth, canvasHeight);
        bitmap.recycle();
        if(processedBitmap != null){
            mDisplayIv.post(new Runnable() {
                @Override
                public void run() {
                    mDisplayIv.setImageBitmap(processedBitmap);
                }
            });
        }


    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if(requestCode == LETTER_SETTING_REQUEST_CODE && resultCode == LETTER_SETTING_OK){
            String rightAnswersStr = data.getStringExtra("rightAnswers");
            String answerRangeStr = data.getStringExtra("answerRange");
            Log.e(TAG,"onActivityResult");
            setAnswers(rightAnswersStr,answerRangeStr);
        }
    }

    private void setAnswers(String rightAnswersStr, String answerRangeStr) {

        rightAnswers.clear();
        answersRange.clear();
        rightAnswersSection.clear();
        answerIndex = 0;
        Log.e(TAG,"Change rightAnswersStr:"+rightAnswersStr);
        Log.e(TAG,"Change answerRangeStr:"+answerRangeStr);
        for (int i = 0; i < rightAnswersStr.length(); i++) {
            char c = rightAnswersStr.charAt(i);;
            rightAnswers.add(c);
        }
        for (int i = 0; i < answerRangeStr.length(); i++) {
            char c = answerRangeStr.charAt(i);
            int ci = c - 'A';
            answersRange.add(ci);
        }
        configAnswerSection();
    }

    private void configAnswerSection() {
        if(rightAnswers.isEmpty()){
            return;
        }
        rightAnswersSection.clear();
        int answerNumber = rightAnswers.size();
        //maxIndex也是组数 例如maxIndex = 4; 则组为0,1,2,3
        int maxIndex = answerNumber / ANSWER_NUM_PER_LINE;
        if(maxIndex*ANSWER_NUM_PER_LINE < answerNumber){
            maxIndex++;
        }
        if(answerIndex == maxIndex){
            //第一组
            answerIndex = 0;
        }
        if(answerIndex == (maxIndex-1)){
            //最后一组
            for (int i = answerIndex*ANSWER_NUM_PER_LINE; i < answerNumber; i++) {
                rightAnswersSection.add(rightAnswers.get(i));
            }
        }else{
            for (int i = answerIndex*ANSWER_NUM_PER_LINE; i < (answerIndex+1)*ANSWER_NUM_PER_LINE; i++) {
                rightAnswersSection.add(rightAnswers.get(i));
            }
        }
        mIndexTv.setText(String.valueOf(answerIndex+1)+"/"+String.valueOf(maxIndex));
        answerIndex++;
    }


}