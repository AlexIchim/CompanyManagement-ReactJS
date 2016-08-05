import * as React from 'react';
import * as $ from 'jquery';
import config from '../helper';
import Context from '../../context/Context';

export default new class OfficeController {

    GetAll(){
        console.log("GetAll called",config.base);
        $.ajax({
            method:'GET',
            url: config.base+'office/getAll',
            async:false,
            success: function(data){
                Context.cursor.set('items',data);
                Context.cursor.set('formToggle',false);
            }.bind(this)
        });
        console.log("Ajax Finished");
    }

    Add(){
        console.log("Add called");
        $.ajax({
            method:'GET',
            url: config.base+'office/getAll',
            async:false,
            success: function(data){
                Context.cursor.set('items',data);
                Context.cursor.set('formToggle',false);
            }.bind(this)
        });
    }

    Update(){
        console.log("Update called");
        $.ajax({
            method:'GET',
            url: config.base+'office/getAll',
            async:false,
            success: function(data){
                Context.cursor.set('items',data);
                Context.cursor.set('formToggle',false);
            }.bind(this)
        });

    }

}