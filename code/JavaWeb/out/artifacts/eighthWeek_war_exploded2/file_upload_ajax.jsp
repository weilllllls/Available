<%--
  Created by IntelliJ IDEA.
  User: Administrator
  Date: 2020/10/27
  Time: 18:35
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>

<html>
<head>
    <title>Title</title>
    <script src="jquery-1.8.3.min.js"></script>
    <script src="fileinput.js"></script>
    <script src="http://libs.baidu.com/jquery/2.0.0/jquery.min.js"></script>
    <script src="https://cdn.bootcdn.net/ajax/libs/jquery.form/3.09/jquery.form.js"></script>
    <script src="https://cdn.bootcdn.net/ajax/libs/jquery.form/3.09/jquery.form.min.js"></script>
    <link rel="stylesheet" type="text/css" href="bootstrap.min.css"/>
    <link rel="stylesheet" type="text/css" href="fileinput.css"/>
    <link href="https://cdn.bootcdn.net/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap-theme.css" rel="stylesheet">
    <link href="https://cdn.bootcdn.net/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.css" rel="stylesheet">
    <script src="https://cdn.bootcdn.net/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.js"></script>
    <script type="text/javascript" src="jquery.form.js"></script>
</head>
<body>
    <div class="input-chunck">
        <form class="form-horizontal" role="form" method="post"
        enctype="multipart/form-data" id="upload_form" name="upload_form">
        <input type="file" value="选择文件" name="upload-file">
            <a id="start-upload" href="javascript:void(0);" onclick="uploadFileJQ();">开始上传</a>
        </form>
    </div>

    <br>
    <div id="attachment_file_url">文件url</div><br>
    Object:<input type="text" value="" id="attachment_object_id"><br>
    ID:<input type="text" value="12131132" id="device_id"><br>
    Name:<input type="text" value="成都探测器" id="device_name"><br>
    <input type="button" value="保存" onclick="javascript:saveDataToServer()"><br>

</body>
</html>

<script>
    function uploadFileJQ() {
        console.log("JQ");
        var options = {
            type:'post',
            url:'file_receiver?action=upload_article_attachment_record',
            success:function (json) {
                var fileUrl = json.attachment_filename;
                var objectId = json.attachment_object_id;
                var attachmentFileUrl = json.attachment_file_url;

                $("#current_attachment").html("附件；<span style='color:blue;'>"+fileUrl+"</span>");
                $("#attachment_object_id").val(objectId);
                $("#attachment_file_url").html(attachmentFileUrl);


            },
            error:function (error) {
                console.log(error)
            },
            dataType : "json"
        };

        $("#upload_form").ajaxSubmit(options);
    }

    function saveDataToServer() {
        var url = "file_dao?action=save_data_to_db";
        var data = {};
        data.device_id = $("#device_id").val();
        data.device_name = $("#device_name").val();
        data.attachment_file_url = $("#attachment_file_url").html();
        $.post(url,data,function (json) {
            console.log(json);

        });
    }
</script>