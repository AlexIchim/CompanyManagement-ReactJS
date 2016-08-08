import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (project, isAsync, callback) =>
    $.ajax({
        method : 'PUT',
        url : apiconfig.baseUrl + 'projects/update',
        data : project,
        async : isAsync,
        success : callback 
    });