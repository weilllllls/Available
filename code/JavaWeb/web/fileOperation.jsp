<%--
  Created by IntelliJ IDEA.
  User: Administrator
  Date: 2020/10/17
  Time: 19:01
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%
    String path = request.getContextPath();
    String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
    String imgUrl = (String) session.getAttribute("imgUrl");
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
    <base href="<%=basePath%>">

    <title>My JSP 'fileupload.jsp' starting page</title>

    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
    <meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
    <meta http-equiv="description" content="This is my page">
    <!--
    <link rel="stylesheet" type="text/css" href="styles.css">
    -->

</head>

<body>
<!-- enctype 默认是 application/x-www-form-urlencoded -->
<form action="FileServlet" enctype="multipart/form-data" method="post" >

    用户名：<input type="text" name="usename"> <br/>
    上传文件：<input type="file" name="file1"><br/>
    上传文件： <input type="file" name="file2"><br/>
    <input type="submit" value="提交"/>

</form>
<a target="_blank" href="<%=imgUrl%>" >图片链接</a>


</body>
</html>