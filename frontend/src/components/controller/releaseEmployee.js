import React, { Component } from 'react';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';

export default (employeeId)=>{
    $.ajax({
            method: 'DELETE',
            async: false,
            url: configs.baseUrl + 'api/employee/releaseEmployee?employeeId=' +employeeId,
            success: function (data) {
                
            }.bind(this)
        })
}