package com.available.service;

import com.available.dao.RequestInfoDao;
import com.available.entity.RequestInfo;

public class RequestInfoService {
    RequestInfoDao requestInfoDao = new RequestInfoDao();
    public boolean addRequestInfo(RequestInfo requestInfo){
        return requestInfoDao.addRequestInfo(requestInfo);
    }
}
