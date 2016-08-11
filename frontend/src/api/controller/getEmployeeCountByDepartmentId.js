import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (id, searchString, posFilter, emplHoursFilter, allocFrom, allocTo, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'departments/' + id + '/employees/count?searchString=' + searchString + 
             '&positionIdFilter=' + posFilter + '&employmentFilter=' + emplHoursFilter +
             '&allocationFromFilter=' + allocFrom + '&allocationToFilter=' + allocTo,
        async: isAsync,
        success: callback
    });
