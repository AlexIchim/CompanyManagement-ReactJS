import config from '../../helper';
import Command from '../../Command';
import Context from '../../../context/Context'
import Accessors from '../../../context/Accessors';
import * as $ from 'jquery';
export default new class Controller{

    getAllEmployees(){
        $.ajax({
            method: 'GET',
            url: config.base + 'department/members/1',
            async: false,
            success: function(data){
                Context.cursor.set('items', data);
                Context.cursor.set('formToggle', false);
            }.bind(this)
        })
    }

    Add(){
        $.ajax({
            method: 'POST',
            url: config.base + 'employee/add',
            data: {
                Id: 1,
                Name:  this.refs.inputName.value,
                Address: 'Bacovia 1C',
                Duration: this.refs.inputDuration.value,

            },
            async: false,
            success: function(data){
                console.log('success');
            }.bind(this)
        });
        this.getAllEmployees();
        Command.hideModal();
    }

    Edit(element){
        const index = Accessors.items(Context.cursor).indexOf(element);
        Context.cursor.set('model', element);
        Context.cursor.set('formToggle', true);
    }

    Delete(element){
        console.log('we are in delete method, yey', element.id);
        const id = element.id;
        $.ajax({
            method: 'DELETE',
            url: config.base + 'employee/delete/' + id,
            async: false,
            success: function(data){
                console.log('success');
            }.bind(this)
        });
        this.getAllEmployees();
    }

}