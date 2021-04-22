package com.scuavailable.available.school;

import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import com.google.gson.Gson;
import com.scuavailable.available.network.RequestModifyWorker;
import com.scuavailable.available.network.RequestUpdateWorker;

public class RequestUpdateHelper {
    private static final String TAG = "RequestUpdateHelper";
    private SharedPreferences user_pref;
    private Context mContext;
    private RequestUpdateBean mRequestUpdateBean;
    private String mRequestUpdateBeanJson;
    private RequestUpdateWorker mRequestUpdateWorker;

    public RequestUpdateHelper(Context context, RequestUpdateWorker.OnGetNetDataListener listener,RequestUpdateBean requestUpdateBean) {
        mContext = context;
        user_pref = mContext.getSharedPreferences("user_pref",Context.MODE_PRIVATE);
        mRequestUpdateBean = requestUpdateBean;
        String teacherID = user_pref.getString("username", "error");
        Gson gson = new Gson();
        mRequestUpdateBeanJson = gson.toJson(requestUpdateBean);
        mRequestUpdateWorker = new RequestUpdateWorker(context,listener,true,mRequestUpdateBeanJson);
        Log.e(TAG,mRequestUpdateBeanJson);
    }

    public void postRequest(){
        try {
            mRequestUpdateWorker.execute();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
