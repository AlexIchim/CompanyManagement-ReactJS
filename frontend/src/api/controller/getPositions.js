import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (isAsync, callback) => 
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'positions',
        async: isAsync,
        success: callback
    });
