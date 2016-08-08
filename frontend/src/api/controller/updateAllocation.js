import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

export default (allocation, isAsync, callback) =>
    $.ajax({
        method: 'PUT',
        url: apiconfig.baseUrl + 'allocations/update',
        data: allocation,
        async: isAsync,
        success: callback
    });