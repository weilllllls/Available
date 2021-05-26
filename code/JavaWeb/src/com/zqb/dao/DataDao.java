package com.zqb.dao;

import com.zqb.utils.PublicInfo;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

public class DataDao {
//    private static final String myDB = "jdbc:mysql://localhost:3306/test?user=good&password=good&useUnicode=true&characterEncoding=UTF-8";
//    private static final String teaDB = "jdbc:mysql://localhost:3366/ydbc2020?user=Administrator&password=XWClassroom20202023&useUnicode=true&characterEncoding=UTF-8";

    public boolean saveData(String deviceId, String deviceName, String createTime, String attachmentFileUrl){
        try {
            Class.forName("com.mysql.jdbc.Driver");
        } catch (ClassNotFoundException classnotfoundexception) {
            classnotfoundexception.printStackTrace();
        }

        //然后链接数据库，开始操作数据表
        try {
            Connection conn = DriverManager
                    .getConnection(PublicInfo.DBINFO);
            Statement statement = conn.createStatement();
            String sql = "insert into zhuangqibin_device_file(device_id,device_name,attachment_file_id,create_time) values('"
                    + deviceId + "','" + deviceName +  "','" + attachmentFileUrl + "','" + createTime +"')";
            statement.executeUpdate(sql);
            statement.close();
            conn.close();
            return true;
        } catch (SQLException sqlexception) {
            sqlexception.printStackTrace();
        }
        return false;
    }

    public boolean updateData(String deviceId, String deviceName, String createTime, String attachmentFileUrl){
        try {
            Class.forName("com.mysql.jdbc.Driver");
        } catch (ClassNotFoundException classnotfoundexception) {
            classnotfoundexception.printStackTrace();
        }

        //然后链接数据库，开始操作数据表
        try {
            Connection conn = DriverManager
                    .getConnection(PublicInfo.DBINFO);
            Statement statement = conn.createStatement();
            String sql = "update zhuangqibin_device_file set device_name = '" + deviceName+"' ,"+ "attachment_file_id= '"+attachmentFileUrl+"' , "+"create_time= '"+ createTime + "' where device_id = '" + deviceId+"';";
            statement.executeUpdate(sql);
            statement.close();
            conn.close();
            return true;
        } catch (SQLException sqlexception) {
            sqlexception.printStackTrace();
        }
        return false;
    }

    public void deleteData(String deviceId) {
        try {
            Class.forName("com.mysql.jdbc.Driver");
        } catch (ClassNotFoundException classnotfoundexception) {
            classnotfoundexception.printStackTrace();
        }

        //然后链接数据库，开始操作数据表
        try {
            Connection conn = DriverManager
                    .getConnection(PublicInfo.DBINFO);
            Statement statement = conn.createStatement();
            String sql = "delete from zhuangqibin_device_file where device_id = " + deviceId+"; ";
            statement.execute(sql);
            statement.close();
            conn.close();
        } catch (SQLException sqlexception) {
            sqlexception.printStackTrace();
        }
    }
}
