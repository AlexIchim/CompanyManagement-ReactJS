import config from '../../helper';
import Command from '../../Command';
import Context from '../../../context/Context'
import Accessors from '../../../context/Accessors';
export default new class Controller{


    getAllProjects(){
        $.ajax({
            method: 'GET',
            url: config.base + 'department/projects/3/5/1',
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
            url: config.base + 'project/add',
            data: {
                Name:  this.refs.inputName.value,
                DepartmentId: 3,
                Duration: this.refs.inputDuration.value,
                Status: "NotStartedYet"
            },
            async: false,
            success: function(data){
                console.log('success');
            }.bind(this)
        });
        this.getAllProjects();
        Command.hideModal();
    }

    Edit(element){
        const index = Accessors.items(Context.cursor).indexOf(element);
        Context.cursor.set('model', element);
        Context.cursor.set('formToggle', true);
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
        this.getAllProjects();
    }

}