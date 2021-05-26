package com.zqb.servlet;

import com.zqb.service.DataService;
import org.json.JSONObject;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

@WebServlet(name = "DataServlet")
public class DataServlet extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        //用于存储数据，其中包括字段包括常规信息以及文件的URL

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
        dataService.saveData(deviceId,deviceName,createTime,attachmentFileUrl);

//        JSONObject json = new JSONObject();
//        PrintWriter writer = response.getWriter();
//        writer.print(json);
//        writer.flush();
//        writer.close();

    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        doPost(request,response);
    }
}
