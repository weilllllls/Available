package com.available.servlet;

import com.available.entity.Reply;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.stream.JsonReader;
import org.apache.commons.io.IOUtils;

import javax.servlet.ServletException;
import javax.servlet.ServletInputStream;
import javax.servlet.annotation.MultipartConfig;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.Part;
import java.io.*;
import java.util.Enumeration;
import java.util.stream.Stream;

@WebServlet(name = "ServletReply")
@MultipartConfig
public class ServletReply extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        //BufferedReader reader = new BufferedReader(new InputStreamReader(request.getInputStream(),"UTF-8"));
        Part part = request.getPart("data");
        InputStream stream =  part.getInputStream();
        //BufferedReader reader = new BufferedReader(new StreamReader(stream)); //request.getReader();

        byte[] outStream = new byte[1024];
        stream.read(outStream);

        String str = new String(outStream, "UTF-8");
        /*StringBuilder responseStrBuilder = new StringBuilder();
        String inputStr;
        while ((inputStr = reader.readLine()) != null)
            responseStrBuilder.append(inputStr);
        String obj = (String)request.getAttribute("data");
        Enumeration<String> arr = request.getParameterNames();
        //response.get
        inputStr = responseStrBuilder.toString();*/
        System.out.println(str);
        str = str.trim();
        Gson json = new GsonBuilder().setLenient().create();
        //JsonReader r = new JsonReader();
        json.fromJson(str,Reply.class);
        //json.
       /* System.out.println(s);
        System.out.println(request.toString());
        Gson json = new Gson();*/
        response.getWriter().append(str);
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

    }
}
