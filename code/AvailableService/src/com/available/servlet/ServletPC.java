package com.available.servlet;

import com.available.dao.DatabaseConnector;
import com.available.entity.Request;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonElement;
import org.json.JSONObject;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.time.LocalDateTime;
import java.util.ArrayList;

import com.google.gson.Gson;

public class ServletPC extends HttpServlet  {

    //static boolean flag = false;

    DatabaseConnector db = new DatabaseConnector();

    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

    try {
        ResultSet res = db.FindAndProcess();
        if (res == null)
            throw new ServletException();

        ArrayList array = new ArrayList();

        PreparedStatement statement = db.preparedStatement;
        while (res.next()) {
            int id = res.getInt(1);
            String teacher_id = res.getString(2);
            String student_id = res.getString(3);
            String course_id = res.getString(4);
            String section_id = res.getString(5);
            int score = res.getInt(6);

            String test_type = res.getString(7);
            String test_time = res.getString(8);
            String test_room = res.getString(9);
            String test_building = res.getString(10);
            String request_time = res.getString(11);

            String reason = res.getString(12);

            int request_type = res.getInt(13);
            int is_valid = res.getInt(14);

            statement.setInt(1,id);
            statement.executeUpdate();

            Request r = new Request();
            r.Course_Index = Integer.decode(section_id);
            r.Course_Number = course_id;
            r.Index = id;
            r.Reason = reason+";请求修改成绩为:"+score;
            r.Request_State = 0;
            r.Request_Time = request_time;
            r.Student_Number = student_id;
            r.Request_Type = request_type;
            r.Teacher_ID = teacher_id;
            r.Time = test_time;
            if(test_type.equals("正常"))
                r.Type = 0;
            else if(test_type.equals("补缓考"))
                r.Type = 1;
            array.add(r);
        }

        res = db.FindAndProcess2();
        if (res == null)
            throw new ServletException();
        statement = db.preparedStatement;

        while(res.next()){

            int id = res.getInt(1);
            String teacher_id = res.getString(2);
            String student_id = res.getString(3);
            String course_id = res.getString(4);
            String section_id = res.getString(5);

            String test_type = res.getString(6);
            String test_time = res.getString(7);
            String test_room = res.getString(8);
            String test_building = res.getString(9);
            String request_time = res.getString(10);
            String reason = res.getString(11);

            int request_type = res.getInt(12);
            int is_valid = res.getInt(13);

            statement.setInt(1,id);
            statement.executeUpdate();

            Request r = new Request();
            r.Course_Index = Integer.decode(section_id);
            r.Course_Number = course_id;
            r.Index = id;
            r.Reason = reason;
            r.Request_State = 0;
            r.Request_Time = request_time;
            r.Student_Number = student_id;
            r.Request_Type = request_type;
            r.Teacher_ID = teacher_id;
            r.Time = test_time;
            if(test_type.equals("正常"))
                r.Type = 0;
            else if(test_type.equals("补缓考"))
                r.Type = 1;
            array.add(r);

        }





        Object[] a= array.toArray();
        if(a.length ==0)
        {
            response.getWriter().println();
            return;
        }

        String json = new Gson().toJson(a);
        response.setCharacterEncoding("UTF-8");
        PrintWriter writer = response.getWriter();
        writer.println(json);
    }catch (SQLException e){
        e.printStackTrace();
    }

 /*     ResultSet result = new RequestInfoDao().findInfo();
        if(result == null)
            return;

        try {
            Request r = new Request();
            while (result.next()) {
                r.Type = result.getInt("Type");
                r.Time = result.getString("Time");
                r.Teacher_ID = result.getString("Teacher_ID");
                r.Student_Number = result.getString("Student_Number");
                r.Request_Type = result.getInt("Request_Type");
                r.Request_Time = result.getString("Request_Time");
                r.Request_State = result.getInt("Request_State");
                r.Reason = result.getString("Reason");
                r.Index = result.getInt("Index");
                r.Course_Number = result.getString("Course_Number");
                r.Course_Index = result.getInt("Course_Index");
            }
            JSONObject json = r.toJson();
            response.setCharacterEncoding("UTF-8");
            PrintWriter writer = response.getWriter();
            writer.println(json.toString());

        }catch (SQLException e){
            e.printStackTrace();
        }
*/
       /* if(!flag) {
            Request r = new Request();

            r.Course_Index = 1;
            r.Course_Number = "999006030";
            r.Index = 0;
            r.Reason = "请求复查试卷";
            r.Request_State = 0;
            String time = LocalDateTime.now().toString();
            System.out.println(time);
            r.Request_Time = "2020-10-22T00:00:00";
            r.Request_Type = 0;
            r.Student_Number = "2018141400000";
            r.Time = "2020-10-01T00:00:00";
            r.Type = 0;
            r.Teacher_ID = "NULLPTR";
            JSONObject json = r.toJson();
            response.setCharacterEncoding("UTF-8");
            PrintWriter writer = response.getWriter();
            writer.println(json.toString());
            flag = true;
        }
        else{
            Request r2 = new Request();
            r2.Course_Index = 1;
            r2.Course_Number ="999006030";
            r2.Index = 0;
            r2.Reason = "请求复查试卷";
            r2.Request_State=0;
            String time2 = LocalDateTime.now().toString();
            System.out.println(time2);
            r2.Request_Time = "2020-10-22T00:10:00";
            r2.Request_Type = 1;
            r2.Student_Number = "2018141400000";
            r2.Time = "2020-10-01T00:00:00";
            r2.Type = 0;
            r2.Teacher_ID = "NULLPTR";
            JSONObject json2 = r2.toJson();
            response.setCharacterEncoding("UTF-8");
            PrintWriter writer2 = response.getWriter();
            writer2.println(json2.toString());
            flag =false;
        }*/
    }
}
