package com.available.servlet;

import com.available.entity.RequestInfo;
import com.available.service.RequestInfoService;
import com.google.gson.Gson;
import org.json.JSONObject;

import java.io.IOException;

public class RequestInfoServlet extends javax.servlet.http.HttpServlet {
    protected void doPost(javax.servlet.http.HttpServletRequest request, javax.servlet.http.HttpServletResponse response) throws javax.servlet.ServletException, IOException {
        //Android端请求提交获取学生数据的请求
//        1. 发送请求：
//        post方式： 访问/requestInfo
//        字段：
//        - 用户名(学工号124578)
//                - 密码(root)
//                - 学生学号(2018141463010)
//                - 课程号(311200)
//                - 课序号(2)
//                - 考试类型(normal) (***这字段参数需要确认一下***)
//        - 时间(***这字段参数需要确认一下***)
//
//        (发送请求后，服务器保存该数据，并返回成功)

        request.setCharacterEncoding("UTF-8");
        String dataJson = request.getParameter("requestModifyData");
        System.out.println(dataJson);
        Gson gson = new Gson();
        RequestInfo requestInfo = gson.fromJson(dataJson, RequestInfo.class);
        //保存在数据库中
        RequestInfoService requestInfoService = new RequestInfoService();
        requestInfoService.addRequestInfo(requestInfo);
    }

    protected void doGet(javax.servlet.http.HttpServletRequest request, javax.servlet.http.HttpServletResponse response) throws javax.servlet.ServletException, IOException {
        doPost(request,response);
    }
}
