package com.zqb.servlet;

import com.zqb.service.DataService;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@WebServlet(name = "UpdateDataServlet")
public class UpdateDataServlet extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        request.setCharacterEncoding("UTF-8");
        String id = request.getParameter("id");
        String deviceId = request.getParameter("device_id");
        String deviceName = request.getParameter("device_name");
        String attachmentFileUrl = request.getParameter("attachment_file_id");
        String createTime = request.getParameter("create_time");
        System.out.println(deviceId);
        System.out.println(deviceName);
        System.out.println(attachmentFileUrl);
        System.out.println(createTime);
        DataService dataService = new DataService();
        dataService.updateData(deviceId,deviceName,createTime,attachmentFileUrl);
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        doPost(request,response);
    }
}
