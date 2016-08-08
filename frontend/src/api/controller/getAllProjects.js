import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (departmentId, isAsync, callback) =>
    $.ajax({
        method : 'GET',
        url: apiconfig.baseUrl + 'departments/' + departmentId + '/projects',
        async : isAsync,
        success : callback 
    });