package com.available.servlet;

import org.apache.commons.fileupload.disk.DiskFileItemFactory;
import org.apache.commons.fileupload.servlet.ServletFileUpload;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.Part;
import java.io.*;

public class UploadInfoServlet extends javax.servlet.http.HttpServlet  {
    private static final long serialVersionUID = 1L;
    private String uploadPath = "/home/qibin/available/upload";
    private File tempPathFile;
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

        System.out.println("接收到数据，开始处理");

        // System.out.println();
        Part part = request.getPart("data");
        InputStream stream = part.getInputStream();
        byte[] outStream = new byte[1024];
        stream.read(outStream);

        String str = new String(outStream, "UTF-8");
        System.out.println(str);
        DiskFileItemFactory factory = new DiskFileItemFactory();
        factory.setSizeThreshold(4096);
        factory.setRepository(tempPathFile);

        ServletFileUpload upload = new ServletFileUpload(factory);
        String file_name = null;
        try {
            /*
             * List<FileItem> list = (List<FileItem>)upload.parseRequest(request);
             *
             * for(FileItem item : list) { String name = item.getFieldName();
             * if(item.isFormField()) { String value = item.getString();
             * request.setAttribute(name, value); } else { String value = item.getName();
             * int start = value.lastIndexOf("\\"); String filename =
             * value.substring(start+1); file_name = filename;
             */

            // request.setAttribute(name, filename);
            Part part2 = request.getPart("file");
            String filename = part2.getSubmittedFileName();

            OutputStream out = new FileOutputStream(new File(uploadPath, filename));
            // InputStream in = item.getInputStream();
            InputStream in = part2.getInputStream();

            int length = 0;
            byte[] buf = new byte[1024];

            // System.out.println("获取上传文件的总共的容量:"+item.getSize());
            System.out.println("获取上传文件的总共的容量:" + part2.getSize());

            while ((length = in.read(buf)) != -1) {
                out.write(buf, 0, length);
            }

            in.close();
            out.close();


            Part part3 = request.getPart("file2");
            String filename2 = part3.getSubmittedFileName();

            OutputStream out2 = new FileOutputStream(new File(uploadPath, filename2));
            // InputStream in = item.getInputStream();
            InputStream in2 = part3.getInputStream();

            int length2 = 0;
            byte[] buf2 = new byte[1024];

            // System.out.println("获取上传文件的总共的容量:"+item.getSize());
            System.out.println("获取上传文件的总共的容量:" + part3.getSize());

            while ((length2 = in2.read(buf2)) != -1) {
                out2.write(buf2, 0, length2);
            }

            in2.close();
            out2.close();




        } catch (Exception e) {
            e.printStackTrace();
        }
        doGet(request, response);
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        System.out.println("DoGet!!!");
        response.getWriter().append("Served at: ").append(request.getContextPath());

    }
}
