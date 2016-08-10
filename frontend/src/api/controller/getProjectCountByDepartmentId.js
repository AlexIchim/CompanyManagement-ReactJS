import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (departmentId, searchString, statusFilter, isAsync, callback) => {
    const filter = statusFilter ? statusFilter : '';
    
    $.ajax({
        method : 'GET',
        url: apiconfig.baseUrl + 'departments/' + departmentId + '/projects/count' + 
                '?searchString=' + searchString + '&statusFilter=' + filter,
        async : isAsync,
        success : callback 
    });
}