import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

export default (id, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'offices/' + id + '/departments',
        async: isAsync,
        success: callback
    });
