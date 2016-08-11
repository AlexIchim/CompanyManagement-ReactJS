import * as React from 'react';
import * as $ from 'jquery';
import config from '../helper';
import Context from '../../context/Context';
import Accessors from '../../context/Accessors';

export default new class DepartmentController{

    static getDepartmentsAjaxCall(officeId, pageNumber){
        $.ajax({
            method: 'GET',
            url: config.base + 'office/departments/' + officeId + '/5/' + pageNumber,
            async: false,
            success: function (data) {
                Context.cursor.set('items', data);
            }.bind(this)
        })
    }

    static getTotalNumberOfDepartmentsAjaxCall(officeId){
        $.ajax({
            method: 'GET',
            url: config.base + 'office/departmentsCount/' + officeId,
            async: false,
            success: function (data) {
                Context.cursor.set('totalNumberOfItems', data);
            }.bind(this)
        })
    }

    getTotalNumberOfDepartments(officeId){
        DepartmentController.getTotalNumberOfDepartmentsAjaxCall(officeId);
    }

    getDepartments(officeId, pageNumber){
         //console.log("Getting departments");
         DepartmentController.getDepartmentsAjaxCall(officeId, pageNumber);
    }

    Add(officeId, currentPage){
       $.ajax({
           method: 'POST',
           url: config.base + 'department/add',
           async: false,
           data: Accessors.model(Context.cursor),
           success: function(data){
               //Context.cursor.set('totalNumberOfItems', Accessors.totalNumberOfItems(Context.cursor) + 1);
               //this.getTotalNumberOfDepartments(officeId);
               DepartmentController.getTotalNumberOfDepartmentsAjaxCall(officeId);
               DepartmentController.getDepartmentsAjaxCall(officeId, currentPage);
           }.bind(this)
       });
    }

    Update(officeId, currentPage){
        $.ajax({
            method: 'PUT',
            url: config.base + 'department/update',
            async: false,
            data: Accessors.model(Context.cursor),
            success: function(){
                DepartmentController.getDepartmentsAjaxCall(officeId, currentPage);
            }
        });
    }

}