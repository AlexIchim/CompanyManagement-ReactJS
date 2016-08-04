import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

export default (id, isAsync, callback) => 
    $.ajax({
        method: 'DELETE',
        url: apiconfig.baseUrl + 'employees/delete/' + id,
        async: isAsync,
        success: callback
    });
