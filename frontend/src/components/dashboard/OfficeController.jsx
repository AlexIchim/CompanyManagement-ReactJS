import * as React from 'react';
import * as $ from 'jquery';
import config from '../helper';
import Context from '../../context/Context';

export default new class OfficeController {
    
    GetAll(){
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

    Add(){
        console.log("Add called");
        let model= Context.cursor.get("model");
        //console.log("MODEL : ",model);

        $.ajax({
            method:'POST',
            url: config.base+'office/add',
            async:false,
            data:{
                Name:model.Name,
                Address:model.Address,
                Phone:model.Phone,
                Image:model.Image
            },
            success: function(data){  
                console.log("ADD RETURNED");
            }.bind(this)
        });             
        this.GetAll();
        
    }
        
    Update(){
        console.log("Update called");
        let model= Context.cursor.get("model");
        
        

        $.ajax({
            method:'POST',
            url: config.base+'office/update',
            async:false,
            data:{
                Id:model.Id,
                Name:model.Name,
                Address:model.Address,
                Phone:model.Phone,
                Image:model.Image
            },
            success: function(data){                
                console.log("UPDATE RETURNED");
            }.bind(this)
        });
        this.GetAll();
    }

}