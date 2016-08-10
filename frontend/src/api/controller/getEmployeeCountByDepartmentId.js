import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (id, searchString, posFilter, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'departments/' + id + '/employees/count?searchString=' + searchString + '&positionIdFilter=' + posFilter,
        async: isAsync,
        success: callback
    });
