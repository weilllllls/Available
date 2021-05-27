package com.available.servlet;

import com.available.entity.RequestInfo;
import com.available.entity.RequestUpdateBean;
import com.available.service.RequestInfoService;
import com.available.service.RequestUpdateService;
import com.google.gson.Gson;

import java.io.IOException;

public class SubmitInfoServlet extends javax.servlet.http.HttpServlet {
    protected void doPost(javax.servlet.http.HttpServletRequest request, javax.servlet.http.HttpServletResponse response) throws javax.servlet.ServletException, IOException {
        request.setCharacterEncoding("UTF-8");
        String dataJson = request.getParameter("requestUpdateData");
        System.out.println(dataJson);
        Gson gson = new Gson();
        RequestUpdateBean requestUpdateBean = gson.fromJson(dataJson, RequestUpdateBean.class);
        //保存在数据库中
        RequestUpdateService requestUpdateService = new RequestUpdateService();
        requestUpdateService.addRequestUpdate(requestUpdateBean);
    }

    protected void doGet(javax.servlet.http.HttpServletRequest request, javax.servlet.http.HttpServletResponse response) throws javax.servlet.ServletException, IOException {
        doPost(request,response);
    }
}
