package com.zqb.servlet;

import com.zqb.dao.RecordDao;
import com.zqb.service.RecordService;
import org.json.JSONException;
import org.json.JSONObject;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

@WebServlet(name = "GetRecordServlet")
public class GetRecordServlet extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

        //开始查询数据库
        //注意：如果遇到问题，Tomcat的日志在C:\Tomcat\logs\stdout.log，可以查看
        RecordService recordService = new RecordService();
        List jsonList = recordService.getRecord();
        //////////数据库查询完毕，得到了json数组jsonList//////////
        //jsonList.clear();
        //下面开始构建返回的json
        JSONObject jsonObj=new JSONObject();
        try {
            jsonObj.put("aaData",jsonList);
            jsonObj.put("result_msg","ok");	//如果发生错误就设置成"error"等
            jsonObj.put("result_code",0);	//返回0表示正常，不等于0就表示有错误产生，错误代码
        } catch (JSONException e) {
            e.printStackTrace();
        }

        System.out.println("最后构造得到的json是："+jsonObj.toString());
        response.setContentType("text/html; charset=UTF-8");
        try {
            response.getWriter().print(jsonObj);
            response.getWriter().flush();
            response.getWriter().close();
        } catch (IOException e) {
            e.printStackTrace();
        }
        System.out.println("返回结果给调用页面了。");
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        doPost(request,response);
    }
}
