package com.scuavailable.available.register;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextUtils;
import android.text.TextWatcher;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.Toast;

import com.scuavailable.available.MainActivity;
import com.scuavailable.available.R;
import com.scuavailable.available.db.DBManager;
import com.scuavailable.available.login.LoginActivity;

public class RegisterActivity extends AppCompatActivity {

    ImageButton backIb;
    ImageButton mDeleteUserNameIb;
    EditText usernameEt,passwordEt, passwordAgainEt;
    Button registerBtn;
    private Context mContext;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
        mContext = this;
        initViews();
    }





    private void initViews() {
        mDeleteUserNameIb = findViewById(R.id.ib_regiter_delete_username);
        usernameEt = findViewById(R.id.et_register_username);
        passwordEt = findViewById(R.id.et_register_password);
        passwordAgainEt = findViewById(R.id.et_register_password_again);
        backIb = findViewById(R.id.ib_register_back);
        registerBtn = findViewById(R.id.btn_register);

        //设置返回监听
        backIb.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(mContext, MainActivity.class));
                finish();
            }
        });
        //设置删除用户名
        usernameEt.setOnFocusChangeListener(new View.OnFocusChangeListener() {
            @Override
            public void onFocusChange(View v, boolean hasFocus) {
                if(hasFocus && usernameEt.getText().length()>0){
                    //显示
                    mDeleteUserNameIb.setVisibility(View.VISIBLE);
                }else{
                    mDeleteUserNameIb.setVisibility(View.GONE);
                }
            }
        });

        usernameEt.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                passwordEt.setText("");
                if(s.length()>0){
                    mDeleteUserNameIb.setVisibility(View.VISIBLE);
                }else{
                    mDeleteUserNameIb.setVisibility(View.GONE);
                }
            }

            @Override
            public void afterTextChanged(Editable s) {
            }
        });
        //一键删除
        mDeleteUserNameIb.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                usernameEt.setText("");
                passwordEt.setText("");
                mDeleteUserNameIb.setVisibility(View.GONE);
                usernameEt.setFocusable(true);
                usernameEt.setFocusableInTouchMode(true);
                usernameEt.requestFocus();

            }
        });

        //设置登陆
        registerBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //判断网络是否可用
                register();
            }
        });
    }

    private void register() {
        String username = usernameEt.getText().toString();
        String password = passwordEt.getText().toString();
        String passwordAgain = passwordAgainEt.getText().toString();

        if(TextUtils.isEmpty(username)){
            Toast.makeText(mContext,R.string.register_info_error_username,Toast.LENGTH_SHORT).show();
            return;
        }
        if(TextUtils.isEmpty(password)){
            Toast.makeText(mContext,R.string.register_info_error_password,Toast.LENGTH_SHORT).show();
            return;
        }
        if(TextUtils.isEmpty(passwordAgain)){
            Toast.makeText(mContext,R.string.register_info_error_password_again,Toast.LENGTH_SHORT).show();
            return;
        }
        if(!TextUtils.equals(password,passwordAgain)){
            Toast.makeText(mContext,R.string.register_info_error_password_not_same,Toast.LENGTH_SHORT).show();
            return;
        }
        //检验账户密码是否正确
        //跳转主界面 记录登陆状态
        //注册到数据库当中
        DBManager.addUser(username,password);

        Intent intent = new Intent(mContext, LoginActivity.class);
        startActivity(intent);
        finish();
    }
}
