import config from '../../helper';
import Context from '../../../context/Context'
import Accessors from '../../../context/Accessors';
import $ from 'jquery';
export default new class Controller{

    getAllEmployees( jobType, position, allocation){
        console.log('name: ', name);
        $.ajax({
            method: 'GET',
            url: config.base + 'department/members/1/5/1' + "?jobType=" + jobType + "&position=" + position +"&allocation=" + allocation,
            async: false,
            success: function(data){
                console.log("DATA: ", data);
                Context.cursor.set('items', data);
            }.bind(this)
        })
    }

    Add(){
        //console.log("Add employee!");
        console.log("ModeL: ", Context.cursor.get('model'));

        $.ajax({
            method: 'POST',
            url: config.base + 'employee/add',
            data: Accessors.model(Context.cursor),
            async: false,
            success: function(data){
                console.log('success');
            }.bind(this)
        });
        //this.getAllEmployees();

    }

    Edit(){
        $.ajax({
            method: 'PUT',
            url: config.base + 'employee/update',
            data: Accessors.model(Context.cursor),
            async: false,
            success: function(data){
                console.log('success');
            }.bind(this)
        });
    }

    Delete(element){
        console.log('we are in delete method, yey');
        const id = element.Id;

        $.ajax({
            method: 'DELETE',
            url: config.base + 'employee/delete/' + id,
            async: false,
            success: function(data){
                console.log('success');
            }.bind(this)
        });
        //this.getAllEmployees();
    }

}