import config from '../../helper';
import Context from '../../../context/Context';

export default function GetAllProjects(){
    console.log('items before:', Context.cursor.get('items'));

    $.ajax({
        method: 'GET',
        url: config.base + 'department/projects/1/50/1',
        async: false,
        success: function(data){
            Context.cursor.set('items', data);
        }.bind(this)
    })

    console.log('items afteR:', Context.cursor.get('items'));
}