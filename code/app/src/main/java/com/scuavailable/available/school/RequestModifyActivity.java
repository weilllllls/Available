package com.scuavailable.available.school;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.Toast;

import com.scuavailable.available.R;
import com.scuavailable.available.network.RequestModifyWorker;

public class RequestModifyActivity extends AppCompatActivity implements RequestModifyWorker.OnGetNetDataListener {
    private static final String TAG = "RequestModifyActivity";

    EditText studentIDEt,courseIDEt,sectionIDEt,reasonEt;
    Button mSubmitBtn;
    ImageButton backBtn;
    Context mContext;
    RequestModifyWorker.OnGetNetDataListener mListener;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_request_modify);
        Log.e(TAG,"进入");
        mContext = this;
        mListener = this;
        initViews();

    }

    private void initViews() {
        studentIDEt = findViewById(R.id.et_request_modify_student_id);
        courseIDEt = findViewById(R.id.et_request_modify_course_id);
        sectionIDEt = findViewById(R.id.et_request_modify_section);
        mSubmitBtn = findViewById(R.id.btn_request_modify_submit);
        backBtn = findViewById(R.id.ib_request_modify_back);
        reasonEt = findViewById(R.id.et_request_modify_reason);

        backBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });
        mSubmitBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //发送给请求
                String studentID = studentIDEt.getText().toString();
                String courseID = courseIDEt.getText().toString();
                String sectionID = sectionIDEt.getText().toString();
                String reason = reasonEt.getText().toString();
                RequestModifyHelper requestModifyHelper = new RequestModifyHelper(mContext,mListener,studentID,courseID,sectionID,reason);
                requestModifyHelper.postRequest();
                Toast.makeText(mContext,"发送成功",Toast.LENGTH_SHORT).show();
                finish();
            }
        });
    }


    @Override
    public void onSuccess(String json) {

    }
}
