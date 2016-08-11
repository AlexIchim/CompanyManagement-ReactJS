import React, { Component } from 'react';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';

export default (projectId, departmentId, ptype, pageNr)=>{
    $.ajax({
        method: 'GET',
        async: false,
        url: configs.baseUrl + 'api/employee/GetEmployeesThatAreNotFullyAllocated?projectId='+ projectId + +'&pageSize=3' + '&pageNr=' + pageNr + '&departmentId=' + departmentId + '&ptype=' + ptype,
        success: function (data) {
            this.setState({
                availableEmployees: data
            })
        }.bind(this)
    })
}