package com.scuavailable.available.scan;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.ImageButton;

import com.rengwuxian.materialedittext.MaterialEditText;
import com.scuavailable.available.R;

import java.util.ArrayList;

public class LetterSettingActivity extends AppCompatActivity {

    MaterialEditText  mRightAnswersEt;
    Button mConfirmBtn;
    ImageButton mBackBtn;
    int LETTER_SETTING_OK = 100;
    ArrayList<CompoundButton> selected;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_letter_setting);
        selected = new ArrayList<>();
        initViews();
    }

    private void initViews() {
        mRightAnswersEt = findViewById(R.id.et_letter_setting_answers);
        mConfirmBtn = findViewById(R.id.btn_letter_setting_confirm);
        mBackBtn = findViewById(R.id.ib_letter_setting_back);
        mBackBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });
        mConfirmBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //获取文本内字符串
                String answers = mRightAnswersEt.getText().toString();
                int checkIDs[] = {R.id.checkbox_A,R.id.checkbox_B,R.id.checkbox_C,R.id.checkbox_D,R.id.checkbox_E,R.id.checkbox_F,R.id.checkbox_G,R.id.checkbox_H};
                //整合最后被选中的字母
                String answerRange="";
                for(int checkID : checkIDs){
                    CheckBox ck = findViewById(checkID);
                    boolean checked = ck.isChecked();
                    if(checked){
                        String s = ck.getText().toString();
                        answerRange += s;
                    }
                }
                Intent intent = new Intent();
                intent.putExtra("rightAnswers",answers);
                intent.putExtra("answerRange",answerRange);
                setResult(LETTER_SETTING_OK,intent);
                finish();
            }
        });
    }
}
