import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

export default (isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'offices',
        async: isAsync,
        success: callback
    });