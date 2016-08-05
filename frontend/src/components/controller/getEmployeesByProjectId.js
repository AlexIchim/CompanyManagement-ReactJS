import React, { Component } from 'react';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';

export default (projectId,pageNr)=>{
    $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/project/getEmployeesByProjectId?projectId=' +projectId+'&pageSize=3&pageNr='+pageNr,
            success: function (data) {
                Context.cursor.set("members",Immutable.fromJS(data));
            }.bind(this)
        })
}
