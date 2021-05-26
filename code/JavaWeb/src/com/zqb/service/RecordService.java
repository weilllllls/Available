package com.zqb.service;

import com.zqb.dao.RecordDao;

import java.util.List;

public class RecordService {
    private RecordDao recordDao = new RecordDao();
    public List getRecord(){
        return recordDao.getRecord();
    }
}
