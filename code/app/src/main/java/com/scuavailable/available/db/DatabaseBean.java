package com.scuavailable.available.db;

public class DatabaseBean {
    private int _id;
    private String username;
    private String password;


    public DatabaseBean(int _id, String city, String content) {
        this._id = _id;
        this.username = city;
        this.password = content;
    }

    public DatabaseBean() {
    }

    public int get_id() {
        return _id;
    }

    public void set_id(int _id) {
        this._id = _id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }


}