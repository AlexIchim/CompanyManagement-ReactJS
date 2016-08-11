import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

export default (id, pageSize, pageNumber, searchString, posFilter,  isAsync, callback) => 
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'projects/' + id + '/employees?searchString=' + searchString + '&positionIdFilter=' + posFilter,
        data: {pageSize: pageSize, pageNumber: pageNumber},
        async: isAsync,
        success: callback
    });
