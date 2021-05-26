package com.zqb.servlet;

import com.zqb.service.FileService;
import org.apache.commons.fileupload.FileItem;
import org.apache.commons.fileupload.FileUploadException;
import org.apache.commons.fileupload.disk.DiskFileItemFactory;
import org.apache.commons.fileupload.servlet.ServletFileUpload;
import org.json.JSONException;
import org.json.JSONObject;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

@WebServlet(name = "FileServlet")
public class FileServlet extends HttpServlet {


    protected void doPost(HttpServletRequest request, HttpServletResponse response) {
        FileService fileService = new FileService();
        String attachmentFileId = fileService.saveFiles(request);
        System.out.println("save id "+ attachmentFileId);
        JSONObject json = new JSONObject();
        try {
            json.put("attachment_file_id",attachmentFileId);
        } catch (JSONException e) {
            e.printStackTrace();
        }
        try {
            response.getWriter().print(json);
            response.getWriter().flush();
            response.getWriter().close();
        } catch (IOException e) {
            e.printStackTrace();
        }


    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) {
        doPost(request,response);
    }
}
