import React, { Component } from 'react';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';

export default (departmentId,pageNr)=>{
    console.log(5)
    $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/employee/getAllDepartmentEmployees?departmentId=' + departmentId+'&pageSize=3&pageNr='+pageNr,
            success: function (data) {
                Context.cursor.set("employees",Immutable.fromJS(data));
            }.bind(this)
        })
}