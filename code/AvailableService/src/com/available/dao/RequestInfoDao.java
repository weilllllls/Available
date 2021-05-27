package com.available.dao;

import com.available.entity.RequestInfo;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

public class RequestInfoDao {

    public boolean addRequestInfo(RequestInfo ri){
        try {
            Class.forName("com.mysql.jdbc.Driver");
        } catch (ClassNotFoundException classnotfoundexception) {
            classnotfoundexception.printStackTrace();
        }

        //然后链接数据库，开始操作数据表
        try {
            Connection conn = DriverManager
                    .getConnection("jdbc:mysql://localhost:3306/available_service?user=root&password=root&useUnicode=true&characterEncoding=UTF-8");
            Statement statement = conn.createStatement();
            String sql = "insert into request_info(teacher_id,student_id,course_id,section_id,test_type,is_valid) values('"
                    + ri.getTeacherID() + "','" + ri.getSectionID() +  "','" +  ri.getCourseID() +  "','" + ri.getSectionID() +  "','" + ri.getTestType() +  "','" + "0" + "')";
            statement.executeUpdate(sql);
            statement.close();
            conn.close();
            return true;
        } catch (SQLException sqlexception) {
            sqlexception.printStackTrace();
            return false;
        }
    }
}
