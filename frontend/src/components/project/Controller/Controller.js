import config from '../../helper';
import Context from '../../../context/Context'
import Accessors from '../../../context/Accessors';
import GetAllProjects from './GetAllProjects';

export default new class Controller{


    static ajaxCall(){
        console.log('ajax call');
        $.ajax({
            method: 'GET',
            url: config.base + 'department/projects/1/50/1',
            async: false,
            success: function(data){
                Context.cursor.set('items', data);
                Context.cursor.set('model', null);
            }.bind(this)
        })
    }

    GetAllProjects() {
        Controller.ajaxCall();
    }

    Add() {
        console.log('name:', Context.cursor.get('model').Name);
        $.ajax({
            method: 'POST',
            url: config.base + 'project/add',
            data: {
                Name: Context.cursor.get('model').Name,
                DepartmentId: 1,
                Duration: Context.cursor.get('model').Duration,
                Status: "NotStartedYet"
            },
            async: false,
            success: function (data) {
                console.log('success');
            }.bind(this)
        });
        Controller.ajaxCall();
    }

    Update() {
        console.log('model is: ', Context.cursor.get('model'));
        $.ajax({
            method: 'PUT',
            url: config.base + 'project/update',
            data: {
                Name: Context.cursor.get('model').Name,
                DepartmentId: 3,
                Duration: Context.cursor.get('model').Duration,
                Status: Context.cursor.get('model').Status
            },
            async: false,
            success: function (data) {
                console.log('successfully updated')

            }.bind(this)
        });

        Controller.ajaxCall();

    }

    Delete(element){
        console.log('we are in delete method, yey', element.Id);
        const id = element.Id;
        $.ajax({
            method: 'DELETE',
            url: config.base + 'project/delete/' + id,
            async: false,
            success: function(data){
                console.log('success');
            }.bind(this)
        });
        Controller.ajaxCall();
    }
    GetStatusDescriptions(){
        console.log('status descriptions');
        $.ajax({
            method:'GET',
            url: config.base + "/project/statusDescriptions",
            async: false,
            success: function(data){
                Context.cursor.set('dropdownItems', data);
            }.bind(this)
        });

    }

}