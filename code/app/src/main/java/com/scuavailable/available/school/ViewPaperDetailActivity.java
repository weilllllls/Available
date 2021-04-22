package com.scuavailable.available.school;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.github.chrisbanes.photoview.PhotoView;
import com.scuavailable.available.R;
import com.scuavailable.available.network.RequestUpdateWorker;
import com.squareup.picasso.Picasso;

public class ViewPaperDetailActivity extends AppCompatActivity implements RequestUpdateWorker.OnGetNetDataListener{
    private static final String TAG = "ViewPaperDetailActivity";

    ImageButton mBackIb;
    TextView mStudentIDTv,mCourseIDTv,mSectionIDTv,mScoreOriginTv;
    PhotoView mPaperUp,mPaperDown;
    Button mSubmitBtn;
    EditText mScoreUpdateEt,mReasonUpdateEt;
    Context mContext;
    RequestUpdateWorker.OnGetNetDataListener mListener;
    String mTeacherID,mStudentID,mCourseID,mSectionID;
    int mScore;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_view_paper_detail);
        Log.e(TAG,"进入");
        mContext = this;
        mListener = this;
        initViews();
    }

    private void initViews() {
        OriginTestBean originTestBean = new OriginTestBean();
        originTestBean.setStudentID("2018141463010");
        originTestBean.setCourseID("311153050");
        originTestBean.setSectionID("2");
        originTestBean.setScore(78);
        //--------------------------------------------------
        mBackIb = findViewById(R.id.ib_detail_back);
        mStudentIDTv = findViewById(R.id.tv_paper_detail_studentid);
        mCourseIDTv = findViewById(R.id.tv_paper_detail_courseid);
        mSectionIDTv = findViewById(R.id.tv_paper_detail_sectionid);
        mScoreOriginTv = findViewById(R.id.tv_paper_detail_score);
        mPaperUp = findViewById(R.id.iv_view_detail_paper_up);
        mPaperDown = findViewById(R.id.iv_view_detail_paper_down);
        mScoreUpdateEt = findViewById(R.id.et_request_update_score);
        mReasonUpdateEt = findViewById(R.id.et_request_update_reason);
        mSubmitBtn = findViewById(R.id.btn_request_update_submit);

        mTeacherID = originTestBean.getTeacherID();
        mStudentID = originTestBean.getStudentID();
        mCourseID = originTestBean.getCourseID();
        mSectionID = originTestBean.getSectionID();
        mScore = originTestBean.getScore();

        mStudentIDTv.setText(mStudentID);
        mCourseIDTv.setText(mCourseID);
        mSectionIDTv.setText(mSectionID);
        mScoreOriginTv.setText(String.valueOf(mScore));
        Picasso.get().load("http://47.98.202.193:8080/upload/test1.jpg").into(mPaperUp);
        Picasso.get().load("http://47.98.202.193:8080/upload/test2.jpg").into(mPaperDown);

        mSubmitBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(TextUtils.isEmpty(mScoreUpdateEt.getText())){
                    Toast.makeText(mContext,"分数栏不能为空",Toast.LENGTH_SHORT).show();
                }else {
                    String scoreStr = mScoreUpdateEt.getText().toString();
                    int score = Integer.valueOf(scoreStr).intValue();
                    String reason = mReasonUpdateEt.getText().toString();
                    RequestUpdateBean requestUpdateBean = new RequestUpdateBean();
                    requestUpdateBean.setTeacherID(mTeacherID);
                    requestUpdateBean.setCourseID(mCourseID);
                    requestUpdateBean.setSectionID(mSectionID);
                    requestUpdateBean.setReason(reason);
                    requestUpdateBean.setScore(score);
                    RequestUpdateHelper requestUpdateHelper = new RequestUpdateHelper(mContext,mListener,requestUpdateBean);
                    requestUpdateHelper.postRequest();
                    Toast.makeText(mContext,"发送成功",Toast.LENGTH_SHORT).show();
                    finish();
                }
            }
        });

        mBackIb.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });

    }


    @Override
    public void onSuccess(String json) {

    }
}
