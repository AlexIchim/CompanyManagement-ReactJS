import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (allocationInput, isAsync, callback) =>
    $.ajax({
        method: 'POST',
        dataType: 'json',
        data: allocationInput,
        url: apiconfig.baseUrl + 'allocations/add',
        async: isAsync,
        success: callback
    });
