package com.scuavailable.available.network;

//import android.app.ProgressDialog;
import android.content.Context;
import android.os.AsyncTask;

import java.io.IOException;

import okhttp3.FormBody;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;



public class RequestUpdateWorker extends AsyncTask<String,Void,String> {
    private final OkHttpClient client = new OkHttpClient();
    Context context;
    OnGetNetDataListener listener;
//    ProgressDialog progressDialog;
    String mRequestInfo;
    boolean isShow = false;

    public RequestUpdateWorker(Context context, OnGetNetDataListener listener,boolean isShow,String requestInfo) {
        this.context = context;
        this.listener = listener;
//        this.progressDialog = progressDialog;
        this.isShow = isShow;
        mRequestInfo = requestInfo;
//        initDialog();
    }

//    private void initDialog(){
//        progressDialog = new ProgressDialog(context);
//        progressDialog.setTitle("提示");
//        progressDialog.setMessage("正在发送中.....");
//    }

    public interface OnGetNetDataListener{
        public void onSuccess(String json);
    }
    //    运行在主线程，进行控件的初始化
    @Override
    protected void onPreExecute() {
        super.onPreExecute();
//        if (isShow) {
//            progressDialog.show();
//        }
    }
    //    运行在主线程得到doinbackground的数据，进行控件的更新
    @Override
    protected void onPostExecute(String s) {
        super.onPostExecute(s);
//        if (isShow){
//            progressDialog.dismiss();
//        }

        listener.onSuccess(s);
    }



    @Override
    protected String doInBackground(String... strings) {
        RequestBody formBody = new FormBody.Builder()
                .add("requestUpdateData", mRequestInfo)
                .build();

        Request request = new Request.Builder()
                .url("http://47.98.202.193:8080/AvailableService/SubmitInfo")
                .post(formBody)
                .build();

        try (Response response = client.newCall(request).execute()) {
            if (!response.isSuccessful()) throw new IOException("Unexpected code " + response);

            System.out.println(response.body().string());
        } catch (IOException e) {
            e.printStackTrace();
        }
        return "ok";
    }
}
