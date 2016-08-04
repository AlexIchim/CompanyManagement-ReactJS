import config from '../../helper';
import Command from '../../Command';
export default new class Controller{

    onStoreClick(){
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
        Command.hideModal();
    }
}