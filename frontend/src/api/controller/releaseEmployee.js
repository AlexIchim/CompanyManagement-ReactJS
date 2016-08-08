import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (id, isAsync, callback) =>
    $.ajax({
        method: 'DELETE',
        url: apiconfig.baseUrl + 'employees/delete/' + id,
        async: isAsync,
        success: callback
    });
