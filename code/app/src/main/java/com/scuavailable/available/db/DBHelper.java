package com.scuavailable.available.db;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class DBHelper extends SQLiteOpenHelper {

    public DBHelper(Context context){
        super(context,"available.db",null,1);
    }



    @Override
    public void onCreate(SQLiteDatabase db) {
//          创建表的操作
        String sql = "create table userinfo(_id integer primary key autoincrement, username varchar(30) unique not null, password varchar(40) not null)";
        db.execSQL(sql);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {

    }
}