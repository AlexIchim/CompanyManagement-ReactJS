import * as React from 'react';
import * as $ from 'jquery';
import config from '../helper';
import Context from '../../context/Context';
import Accessors from '../../context/Accessors';

export default new class DepartmentController{

    static ajaxCall(officeId){
        $.ajax({
            method: 'GET',
            url: config.base + 'office/departments/' + officeId,
            async: false,
            success: function (data) {
                Context.cursor.set('items', data);
            }.bind(this)
        })
    }

    getDepartments(officeId){
         console.log("Getting departments");
         DepartmentController.ajaxCall(officeId);
    }

    Add(officeId){
       $.ajax({
            method: 'POST',
            url: config.base + 'department/add',
            async: false,
            data: Accessors.model(Context.cursor),
            success                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  : function(){
                DepartmentController.ajaxCall(officeId);
            }
        });
    }

    Update(officeId){
        $.ajax({
            method: 'PUT',
            url: config.base + 'department/update',
            async: false,
            data: Accessors.model(Context.cursor),
            success: function(){
                DepartmentController.ajaxCall(officeId);
            }
        });
    }

    /*editDepartment(element){
        const index = Accessors.items(Context.cursor).indexOf(element);
        Context.cursor.set('model', element);
        Context.cursor.set('formToggle', true);
    }*/

}