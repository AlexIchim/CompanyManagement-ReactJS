import * as React from 'react';
import * as $ from 'jquery';

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

    }
        
    Update(){

    }

}