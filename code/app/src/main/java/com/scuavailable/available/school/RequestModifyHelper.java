package com.scuavailable.available.school;

import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import com.google.gson.Gson;
import com.scuavailable.available.network.RequestModifyWorker;


public class RequestModifyHelper {
    private static final String TAG = "RequestModifyHelper";
    private SharedPreferences user_pref;
    private Context mContext;
    private RequestModifyBean mRequestModifyBean;
    private String mRequestModifyBeanJson;
    private RequestModifyWorker mRequestModifyWorker;
    public RequestModifyHelper(Context context, RequestModifyWorker.OnGetNetDataListener listener, String studentID, String courseID, String sectionID, String reason) {
        mContext = context;
        user_pref = mContext.getSharedPreferences("user_pref",Context.MODE_PRIVATE);
        String teacherID = user_pref.getString("username", "error");
        mRequestModifyBean = new RequestModifyBean();
        mRequestModifyBean.setTeacherID(teacherID);
        mRequestModifyBean.setStudentID(studentID);
        mRequestModifyBean.setCourseID(courseID);
        mRequestModifyBean.setSectionID(sectionID);
        mRequestModifyBean.setReason(reason);
        Gson gson = new Gson();
        mRequestModifyBeanJson = gson.toJson(mRequestModifyBean);
        mRequestModifyWorker = new RequestModifyWorker(context,listener,true,mRequestModifyBeanJson);
        Log.e(TAG,mRequestModifyBeanJson);
    }

    public void postRequest(){
        try {
            mRequestModifyWorker.execute();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
