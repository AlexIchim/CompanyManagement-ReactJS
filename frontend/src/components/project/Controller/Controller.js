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

    static GetAllProjectMembers(projectId){
        $.ajax({
            method: 'GET',
            url: config.base + 'project/members/' +projectId + '/5/1',
            async: false,
            success: function(data){
                console.log('data:', data);
                Context.cursor.set('items', data);
                Context.cursor.set('model', null);
            }.bind(this)
        });
    }

    GetAllProjects() {
        Controller.ajaxCall();
    }
    GetProjectMembers(projectId){
        Controller.GetAllProjectMembers(projectId);
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
        console.log('model is: ', Context.cursor.get('model').Status);
        $.ajax({
            method: 'PUT',
            url: config.base + 'project/update',
            data: {
                Id: Context.cursor.get('model').Id,
                Name: Context.cursor.get('model').Name,
                DepartmentId: 3,
                Duration: Context.cursor.get('model').Duration,
                Status: Context.cursor.get('model').Status
            },
            async: false,
            success: function (data) {
                console.log('successfully updated')
                console.log('data updated:', data);

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

    EditAllocation(projectId){
        console.log('controller edit allocation');
        console.log('allocation ',Context.cursor.get('model').Allocation )
        $.ajax({
            method: 'PUT',
            url: config.base + "project/editAllocation",
            data: {
                projectId: projectId,
                employeeId: Context.cursor.get('model').Id,
                Allocation: Context.cursor.get('model').Allocation
            },
            async: false,
            success: function(data){
                console.log('successfully changed allocation');
            }.bind(this)
        });
        Controller.GetAllProjectMembers(projectId);
    }

    DeleteAssignment(employeeId, projectId){
        console.log('employee id, projectId', employeeId, projectId);
        $.ajax({
            method: 'DELETE',
            url: config.base + "project/deleteEmployee/" + employeeId + "/" + projectId,
            async: false,
            success: function(data){
                console.log('successfully deleted assignment')
            }
        })
        Controller.GetAllProjectMembers(projectId);
    }

}