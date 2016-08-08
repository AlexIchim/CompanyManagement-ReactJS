import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

export default (id, pageSize, pageNumber, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'departments/' + id + '/employees?pageSize=' + pageSize + "&pageNumber=" + pageNumber,
        async: isAsync,
        success: callback
    });
