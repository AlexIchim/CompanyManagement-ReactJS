import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (projectId, searchString, posFilter, isAsync, callback) => 
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'projects/' + projectId + '/employees/count?searchString=' + searchString + '&positionIdFilter=' + posFilter,
        async: true,
        success: callback
    });