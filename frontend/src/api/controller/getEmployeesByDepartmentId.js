import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (id, pageSize, pageNumber, searchString, posFilter, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'departments/' + id + '/employees?pageSize=' + pageSize + "&pageNumber=" + pageNumber + '&searchString=' + searchString + '&positionIdFilter=' + posFilter,
        async: isAsync,
        success: callback
    });
