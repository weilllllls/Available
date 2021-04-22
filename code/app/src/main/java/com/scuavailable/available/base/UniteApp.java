package com.scuavailable.available.base;

import android.app.Application;

import com.scuavailable.available.db.DBManager;

public class UniteApp extends Application {
    @Override
    public void onCreate() {
        super.onCreate();
        DBManager.initDB(this);
    }
}
