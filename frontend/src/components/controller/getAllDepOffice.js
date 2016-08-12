import React, { Component } from 'react';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';

export default (officeId,pageNr)=>{
    $.ajax({
            method: 'GET',
            async: false,
            url: configs.baseUrl + 'api/office/getAllDepOffice?officeId=' + officeId+'&pageSize=3&pageNr='+pageNr,
            success: function (data) {
                console.log(data)
                Context.cursor.set("departments",Immutable.fromJS(data));
            }.bind(this)
        })
}