package com.scuavailable.available.login;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextUtils;
import android.text.TextWatcher;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.Toast;
import android.widget.Toolbar;

import com.scuavailable.available.MainActivity;
import com.scuavailable.available.R;
import com.scuavailable.available.db.DBManager;
import com.scuavailable.available.register.RegisterActivity;

public class LoginActivity extends AppCompatActivity {

    ImageButton backIb;
    ImageButton mDeleteUserNameIb;
    EditText usernameEt,passwordEt;
    Button loginBtn,registerBtn;
    private Context mContext;
    private SharedPreferences user_pref;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        mContext = this;
        initViews();
    }


    private void initViews() {
        mDeleteUserNameIb = findViewById(R.id.ib_delete_username);
        usernameEt = findViewById(R.id.et_login_username);
        passwordEt = findViewById(R.id.et_password);
        backIb = findViewById(R.id.ib_login_back);
        loginBtn = findViewById(R.id.btn_login);
        registerBtn = findViewById(R.id.btn_jump_register);
        //设置返回监听
        backIb.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(mContext, MainActivity.class));
                finish();
            }
        });
        registerBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(mContext, RegisterActivity.class));
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
        loginBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
            //判断网络是否可用
                login();
            }
        });
    }

    private void login() {
        String username = usernameEt.getText().toString();
        String password = passwordEt.getText().toString();
        if(TextUtils.isEmpty(username)){
            Toast.makeText(mContext,R.string.login_info_error_username,Toast.LENGTH_SHORT).show();
            return;
        }
        if(TextUtils.isEmpty(password)){
            Toast.makeText(mContext,R.string.login_info_error_password,Toast.LENGTH_SHORT).show();
            return;
        }
        //检验账户密码是否正确
        //跳转主界面 记录登陆状态
        if(!DBManager.checkPassword(username,password)){
            Toast.makeText(mContext,R.string.login_info_error_account,Toast.LENGTH_SHORT).show();
            return;
        }
        user_pref = getSharedPreferences("user_pref", Context.MODE_PRIVATE);
        SharedPreferences.Editor edit = user_pref.edit();
        edit.putString("username",username);
        edit.putBoolean("login_status",true);
        edit.commit();
        Intent intent = new Intent(mContext, MainActivity.class);
        startActivity(intent);
        finish();
    }


}
