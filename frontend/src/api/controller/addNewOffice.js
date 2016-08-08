import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (office, isAsync, callback) =>
    $.ajax({
        method: 'POST',
        url: apiconfig.baseUrl + 'offices/add',
        data: office,
        async: isAsync,
        success: callback
    });