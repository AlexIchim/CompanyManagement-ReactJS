import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

export default (id, isAsync, callback) => 
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'departments/' + id + '/employees',
        async: isAsync,
        success: callback
    });
