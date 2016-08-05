import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (employeeInput, isAsync, callback) => 
    $.ajax({
        method: 'PUT',
        dataType: 'json', 
        data: employeeInput,
        url: apiconfig.baseUrl + 'employees/update',
        async: isAsync,
        success: callback
    });
