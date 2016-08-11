import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'employees/managers',
        async: isAsync,
        success: callback
    });