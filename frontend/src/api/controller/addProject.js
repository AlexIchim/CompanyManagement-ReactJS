import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (project, isAsync, callback) =>
    $.ajax({
        method : 'POST',
        url : apiconfig.baseUrl + 'projects/add',
        data : project,
        async : isAsync,
        success : callback 
    });