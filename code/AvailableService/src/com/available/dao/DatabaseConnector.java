package com.available.dao;

import java.sql.*;

public class DatabaseConnector {

    Connection conn;
    Statement statement;
    public PreparedStatement preparedStatement;


    public DatabaseConnector(){
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
        } catch (ClassNotFoundException classnotfoundexception) {
            classnotfoundexception.printStackTrace();
        }
        try {
            conn = DriverManager
                    .getConnection("jdbc:mysql://localhost:3306/available?user=admin&password=admin&useUnicode=true&characterEncoding=UTF-8&serverTimezone=GMT");
            statement = conn.createStatement();
        }catch (SQLException e){
            e.printStackTrace();
        }
    }



    public ResultSet FindAndProcess(){


        try{
            preparedStatement = conn.prepareStatement(
                "update update_info " +
                        "set is_valid = 1 " +
                        "where id = ? ");

            ResultSet res = statement.executeQuery("select * from update_info where is_valid = 0");
       return res;
        }catch(SQLException e){
            e.printStackTrace();
        }
       return null;
    }

    public ResultSet FindAndProcess2(){
        try{
        preparedStatement = conn.prepareStatement(
                "update request_info " +
                        "set is_valid = 1 " +
                        "where id = ? ");

        ResultSet res = statement.executeQuery("select * from request_info where is_valid = 0");
        return res;
        }catch (SQLException e){
            e.printStackTrace();
        }
        return null;
    }

}
