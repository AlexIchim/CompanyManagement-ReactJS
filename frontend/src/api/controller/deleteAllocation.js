import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

export default (id, isAsync, callback) =>
    $.ajax({
        method: 'DELETE',
        url: apiconfig.baseUrl + 'allocations/delete/' + id,
        data: id,
        async: isAsync,
        success: callback
    });
