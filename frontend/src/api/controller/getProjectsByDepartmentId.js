import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (departmentId, pageSize, pageNumber, isAsync, callback) =>
    $.ajax({
        method : 'GET',
        url: apiconfig.baseUrl + 'departments/' + departmentId + '/projects?pageSize=' + pageSize + '&pageNumber=' + pageNumber,
        async : isAsync,
        success : callback 
    });