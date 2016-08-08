import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (id, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'departments/' + id + '/employees/count',
        async: isAsync,
        success: callback
    });
