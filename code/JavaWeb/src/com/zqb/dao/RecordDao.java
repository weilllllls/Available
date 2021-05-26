package com.zqb.dao;

import com.zqb.utils.PublicInfo;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class RecordDao {
//
//    private static final String myDB = "jdbc:mysql://localhost:3306/test?user=good&password=good&useUnicode=true&characterEncoding=UTF-8";
//    private static final String teaDB = "jdbc:mysql://localhost:3366/ydbc2020?user=Administrator&password=XWClassroom20202023&useUnicode=true&characterEncoding=UTF-8";

    public List getRecord()
    {
        List jsonList = new ArrayList();
        try {
            Class.forName("com.mysql.jdbc.Driver");
        } catch (ClassNotFoundException classnotfoundexception) {
            classnotfoundexception.printStackTrace();
        }
        try {
            //注意：数据表video_file确保在test数据库下面，如果没有就导入进去，或者放在自己建的数据库，下面的test相应要修改
            Connection conn = DriverManager.getConnection(PublicInfo.DBINFO);
            Statement statement = conn.createStatement();
            System.out.println("连接数据库Ok！！！");
            //构造sql语句，根据传递过来的查询条件参数，目前是deviceId和gpsTime
            String sql="select * from zhuangqibin_device_file order by create_time";
            //也可以根据device_id查询
            //String sql="select * from video_file where device_id like '%"+deviceId+"%' order by register_time";
            System.out.println("构造出来的sql语句是："+sql);
            ResultSet rs = statement.executeQuery(sql);
            while (rs.next()) {
                List list = new ArrayList();
                list.add(rs.getString("id"));
                list.add(rs.getString("device_id"));
                list.add(rs.getString("device_name"));
                List attachment_file_url_list = getFileUrl(rs.getString("attachment_file_id"));
                list.add(attachment_file_url_list);
                list.add(rs.getString("create_time"));
                jsonList.add(list);
            }
            statement.close();
            conn.close();
            System.out.println("数据库关闭了！！！");
        } catch (SQLException sqlexception) {
            sqlexception.printStackTrace();
        }
        return jsonList;
    }

    public List getFileUrl(String attachmentFileID)
    {
        List jsonList = new ArrayList();
        try {
            Class.forName("com.mysql.jdbc.Driver");
        } catch (ClassNotFoundException classnotfoundexception) {
            classnotfoundexception.printStackTrace();
        }
        try {
            //注意：数据表video_file确保在test数据库下面，如果没有就导入进去，或者放在自己建的数据库，下面的test相应要修改
            Connection conn = DriverManager.getConnection(PublicInfo.DBINFO);
            Statement statement = conn.createStatement();
            System.out.println("连接数据库Ok！！！");
            //构造sql语句，根据传递过来的查询条件参数，目前是deviceId和gpsTime
            String sql="select attachment_file_url from zhuangqibin_attach_file where attachment_file_id = '"
                    + attachmentFileID  +"'";
            //也可以根据device_id查询
            //String sql="select * from video_file where device_id like '%"+deviceId+"%' order by register_time";
            System.out.println("构造出来的sql语句是："+sql);
            ResultSet rs = statement.executeQuery(sql);
            while (rs.next()) {
                jsonList.add(rs.getString("attachment_file_url"));
            }
            statement.close();
            conn.close();
            System.out.println("数据库关闭了！！！");
        } catch (SQLException sqlexception) {
            sqlexception.printStackTrace();
        }
        return jsonList;
    }
}
