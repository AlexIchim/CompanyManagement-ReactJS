import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (id, pageSize, pageNumber, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'offices/' + id + '/departments?pageSize=' + pageSize + '&pageNumber=' + pageNumber,
        async: isAsync,
        success: callback
    });