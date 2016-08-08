import React, { Component } from 'react';
import * as $ from 'jquery';
import configs from '../helpers/calls'
import Context from '../../context/Context.js';
import * as Immutable from 'immutable';



export default () => {
    
    $.ajax({
        method: 'GET',
        async: false,
        url: configs.baseUrl + 'api/office/getAll',
        success: function (data) {
            Context.cursor.set("offices", Immutable.fromJS(data));

        }.bind(this)
    })
}