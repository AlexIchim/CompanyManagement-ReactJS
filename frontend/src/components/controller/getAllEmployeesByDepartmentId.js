import React, { Component } from 'react';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';

export default (departmentId,employeeName,allocation,ptype,jtype,pageNr)=>{
    $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/getAllDepartmentEmployees?departmentId=' + departmentId+ '&employeeName=' + employeeName + '&allocation=' + allocation + '&ptype=' +ptype + '&jtype=' + jtype  + '&pageSize=3&pageNr='+pageNr,
            success: function (data) {
                if (!data){
                    data=[]
                }
                Context.cursor.set("employees",Immutable.fromJS(data));
            }.bind(this)
        })
}