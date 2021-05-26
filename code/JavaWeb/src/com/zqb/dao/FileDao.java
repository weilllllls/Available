package com.zqb.dao;

import com.zqb.utils.PublicInfo;
import org.apache.commons.fileupload.FileItem;
import org.apache.commons.fileupload.FileUploadException;
import org.apache.commons.fileupload.disk.DiskFileItemFactory;
import org.apache.commons.fileupload.servlet.ServletFileUpload;
import org.json.JSONException;
import org.json.JSONObject;

import javax.servlet.http.HttpServletRequest;
import java.io.File;
import java.io.UnsupportedEncodingException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

public class FileDao {
    private String uploadPath="C:\\temp";
//    private static final String myDB = "jdbc:mysql://localhost:3306/test?user=good&password=good&useUnicode=true&characterEncoding=UTF-8";
//    private static final String teaDB = "jdbc:mysql://localhost:3366/ydbc2020?user=Administrator&password=XWClassroom20202023&useUnicode=true&characterEncoding=UTF-8";
    File tempPathFile;

    public List<JSONObject> saveFiles(HttpServletRequest request)
    {
        //用于接收一个或多个文件，存入本地并返回url
        List<JSONObject> jsonObjectList = new ArrayList<>();

        try {
            request.setCharacterEncoding("utf-8");	//设置编码
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }

        String currentFilename="";
        //获得磁盘文件条目工厂
        DiskFileItemFactory factory = new DiskFileItemFactory();
        //获取文件需要上传到的路径
        //如果没以下两行设置的话，上传大的 文件 会占用 很多内存，
        //设置暂时存放的 存储室 , 这个存储室，可以和 最终存储文件 的目录不同
        /**
         * 原理 它是先存到 暂时存储室，然后在真正写到 对应目录的硬盘上，
         * 按理来说 当上传一个文件时，其实是上传了两份，第一个是以 .tem 格式的
         * 然后再将其真正写到 对应目录的硬盘上
         */
        factory.setRepository(tempPathFile);
        //设置 缓存的大小，当上传文件的容量超过该缓存时，直接放到 暂时存储室
        factory.setSizeThreshold(1024*1024) ;

        //高水平的API文件上传处理
        ServletFileUpload upload = new ServletFileUpload(factory);

        upload.setSizeMax(4194304);
        try {
            List<FileItem> items = upload.parseRequest(request);
            Iterator<FileItem> iterator = items.iterator();
            while(iterator.hasNext()){
                FileItem fileItem = iterator.next();
                String name = fileItem.getName();
                if(name != null){
                    currentFilename = name;
                    File fullfile = new File(new String(fileItem.getName().getBytes(), "utf-8"));
                    File savedFile = new File(uploadPath,fullfile.getName());
                    System.out.println(fullfile.getName());
                    System.out.println(savedFile.getAbsolutePath());
                    fileItem.write(savedFile);
                    //存入数组中
                    JSONObject json = new JSONObject();
                    String objectId = "OBJECT_" + (new SimpleDateFormat("yyyy-MM-dd HH:mm:ss")).format(new Date());
                    String fileUrl = "/tomystore/" + fullfile.getName();
                    try {
                        json.put("attachment_filename",fullfile.getName());
                        json.put("attachment_object_id",objectId);
                        json.put("attachment_file_url",fileUrl);

                    } catch (JSONException e) {
                        e.printStackTrace();
                    }
                    jsonObjectList.add(json);
                }
            }
        } catch (FileUploadException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }

        return jsonObjectList;

    }

    public String saveFilejson2DB(List<JSONObject> jsonObjectList)
    {
        String attachmentFileId = "OBJECT_" + (new SimpleDateFormat("yyyy_MM_dd_HH_mm_ss")).format(new Date());

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
            for(int i = 0; i < jsonObjectList.size(); i++){
                String attachmentFileUrl = jsonObjectList.get(i).getString("attachment_file_url");
                String sql = "insert into zhuangqibin_attach_file(attachment_file_id,attachment_file_url) values('"
                        + attachmentFileId +  "','" + attachmentFileUrl + "')";
                statement.executeUpdate(sql);
                System.out.println(attachmentFileId);
            }
            statement.close();
            conn.close();
        } catch (SQLException | JSONException sqlexception) {
            sqlexception.printStackTrace();
        }
        return attachmentFileId;
    }
}
