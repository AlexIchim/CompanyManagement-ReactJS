import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (id, isAsync, callback) =>
    $.ajax({
        method : 'DELETE',
        url : apiconfig.baseUrl + 'projects/delete/' + id,
        async : isAsync,
        success : callback 
    });