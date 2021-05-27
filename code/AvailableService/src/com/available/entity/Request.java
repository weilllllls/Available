package com.available.entity;

import org.json.JSONException;
import org.json.JSONObject;

public class Request{
    public int Index;
    public int Request_State;
    public String Course_Number;
    public int Course_Index;
    public String Student_Number;
    public String Time;
    public String Reason;
    public String Request_Time;
    public int Request_Type;
    public int Type;
    public String Teacher_ID;

    public JSONObject toJson() {
        String names[] = {"Index","Request_State","Course_Number","Course_Index","Student_Number","Time","Reason","Request_Time","Request_Type","Type","Teacher_ID"};

        JSONObject json = new JSONObject(this,names);
        return json;
    }

    public void FromJson(JSONObject json) {
        try{
        this.Course_Index = json.getInt("Course_Index");
        this.Course_Number = json.getString("Course_Number");
        this.Index = json.getInt("Index");
        this.Reason = json.getString("Reason");
        this.Request_State = json.getInt("Request_State");
        this.Request_Time = json.getString("Request_Time");
        this.Request_Type = json.getInt("Request_Type");
        this.Student_Number = json.getString("Student_Number");
        this.Teacher_ID = json.getString("Teacher_ID");
        this.Time = json.getString("Time");
        this.Type = json.getInt("Type");

        }catch(JSONException e){
            e.printStackTrace();
        }
    }
}
