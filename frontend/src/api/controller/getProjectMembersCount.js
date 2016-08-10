import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (projectId, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'projects/' + projectId + '/employees/count',
        async: true,
        success: callback
    });
