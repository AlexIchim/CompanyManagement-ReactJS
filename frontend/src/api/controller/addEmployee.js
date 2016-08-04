import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (employeeInput, isAsync, callback) => 
    $.ajax({
        method: 'POST',
        dataType: 'json', 
        data: employeeInput,
        url: apiconfig.baseUrl + 'employees/add',
        async: isAsync,
        success: callback
    });
