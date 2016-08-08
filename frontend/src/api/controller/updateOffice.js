import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (office, isAsync, callback) =>
    $.ajax({
        method: 'PUT',
        url: apiconfig.baseUrl + 'offices/update',
        data: office,
        async: isAsync,
        success: callback
    });