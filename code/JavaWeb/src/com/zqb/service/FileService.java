package com.zqb.service;

import com.zqb.dao.FileDao;
import org.json.JSONObject;

import javax.servlet.http.HttpServletRequest;
import java.util.List;

public class FileService {
    private FileDao fileDao = new FileDao();

    public String saveFiles(HttpServletRequest request)
    {
        List<JSONObject> jsonObjectList = fileDao.saveFiles(request);
        String attachmentFileId = fileDao.saveFilejson2DB(jsonObjectList);
        return attachmentFileId;
    }
}
