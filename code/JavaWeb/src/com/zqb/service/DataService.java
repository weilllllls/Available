package com.zqb.service;

import com.zqb.dao.DataDao;

public class DataService {
    private DataDao dataDao = new DataDao();

    public void saveData(String deviceId, String deviceName, String createTime, String attachmentFileUrl){
        dataDao.saveData(deviceId,deviceName,createTime,attachmentFileUrl);
    }
    public void updateData(String deviceId, String deviceName, String createTime, String attachmentFileUrl){
        dataDao.updateData(deviceId,deviceName,createTime,attachmentFileUrl);
    }
    public void deleteData(String deviceId){
        dataDao.deleteData(deviceId);
    }
}
