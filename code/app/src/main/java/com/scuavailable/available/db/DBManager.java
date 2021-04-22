package com.scuavailable.available.db;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.text.TextUtils;
import android.util.Log;

import java.util.ArrayList;
import java.util.List;

public class DBManager {
    private static final String TAG = "DBManager";

    public static SQLiteDatabase database;
    public static void initDB(Context context){
        DBHelper dbHelper = new DBHelper(context);
        database = dbHelper.getWritableDatabase();
    }

    /*增加用户*/
    public static long addUser(String username, String password){
        ContentValues values = new ContentValues();
        values.put("username",username);
        values.put("password",password);
        return database.insert("userinfo",null,values);
    }

    /*获取用户匹配用户密码*/
    public static boolean checkPassword(String username, String password){
        Cursor cursor = database.query("userinfo", null, "username=?", new String[]{username}, null, null, null);

        if (cursor.getCount()>0) {
            cursor.moveToFirst();
            String rightPassword = cursor.getString(cursor.getColumnIndex("username"));
            Log.e(TAG,"1");
            if(TextUtils.equals(rightPassword,password)){
                Log.e(TAG,"2");
                return true;
            }else {
                Log.e(TAG,"3");

                return false;
            }
        }
        return false;
    }

}
