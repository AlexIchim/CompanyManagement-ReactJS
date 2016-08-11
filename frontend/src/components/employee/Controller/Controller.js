import config from '../../helper';
import Context from '../../../context/Context'
import Accessors from '../../../context/Accessors';
import $ from 'jquery';
export default new class Controller{

    getAllEmployees(departmentId, pageNumber){
        $.ajax({
            method: 'GET',
            url: config.base + "department/members/" + departmentId + "/5/" + pageNumber,
            async: false,
            success: function(data){
                Context.cursor.set('items', data);
            }.bind(this)
        })
    }

    getTotalNumberOfEmployees(departmentId){
        $.ajax({
            method: 'GET',
            url: config.base + 'department/membersCount/' + departmentId,
            async: false,
            success: function (data) {
                Context.cursor.set('totalNumberOfItems', data);
            }.bind(this)
        })
    }

    Add(departmentId, currentPage){
        $.ajax({
            method: 'POST',
            url: config.base + 'employee/add',
            data: Accessors.model(Context.cursor),
            async: false,
            success: function(data){
                console.log("Successfully added!");
            }.bind(this)
        });
        this.getTotalNumberOfEmployees(departmentId);
        this.getAllEmployees(departmentId, currentPage);
    }

    Edit(departmentId, currentPage){
        $.ajax({
            method: 'PUT',
            url: config.base + 'employee/update',
            data: Accessors.model(Context.cursor),
            async: false,
            success: function(data){
                console.log("Successfully updated!");
            }
        });
        this.getAllEmployees(departmentId, currentPage);
    }

    Delete(element){
        const id = element.Id;

        $.ajax({
            method: 'DELETE',
            url: config.base + 'employee/delete/' + id,
            async: false,
            success: function(data){
                console.log("Successfully deleted!");
            }.bind(this)
        });
    }

}