import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (departmentId, pageSize, pageNumber, searchString, statusFilter, isAsync, callback) => {
    const filter = statusFilter ? statusFilter : '';
    
    $.ajax({
        method : 'GET',
        url: apiconfig.baseUrl + 'departments/' + departmentId + '/projects?pageSize=' + pageSize + '&pageNumber=' + pageNumber + 
                '&searchString=' + searchString + '&statusFilter=' + filter,
        async : isAsync,
        success : callback 
    });
}