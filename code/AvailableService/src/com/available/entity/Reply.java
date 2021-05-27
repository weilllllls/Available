package com.available.entity;

public class Reply {

    private String teacherID;
    private String courseNumber;
    private String studentNumber;
    private int courseIndex;
    private String file;
    private String content;
    private String time;

    public String getTime() {
        return time;
    }

    public void setTime(String time) {
        this.time = time;
    }


    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }


    public String getTeacherID() {
        return teacherID;
    }

    public void setTeacherID(String teacherID) {
        this.teacherID = teacherID;
    }

    public String getCourseNumber() {
        return courseNumber;
    }

    public void setCourseNumber(String courseNumber) {
        this.courseNumber = courseNumber;
    }

    public String getStudentNumber() {
        return studentNumber;
    }

    public void setStudentNumber(String studentNumber) {
        this.studentNumber = studentNumber;
    }

    public int getCourseIndex() {
        return courseIndex;
    }

    public void setCourseIndex(int courseIndex) {
        this.courseIndex = courseIndex;
    }

    public String getFile() {
        return file;
    }

    public void setFile(String file) {
        this.file = file;
    }



}
