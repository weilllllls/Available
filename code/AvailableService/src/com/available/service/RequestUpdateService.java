package com.available.service;

import com.available.dao.RequestUpdateDao;
import com.available.entity.RequestUpdateBean;

public class RequestUpdateService {
    RequestUpdateDao requestUpdateDao = new RequestUpdateDao();
    public boolean addRequestUpdate(RequestUpdateBean requestUpdateBean){
        return requestUpdateDao.addRequestUpdate(requestUpdateBean);
    }
}
