import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (id, pageSize, pageNumber, searchString, posFilter, emplHoursFilter, allocFrom, allocTo, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'departments/' + id + '/employees?pageSize=' + pageSize + "&pageNumber=" + pageNumber + '&searchString=' + searchString + 
             '&positionIdFilter=' + posFilter + '&employmentFilter=' + emplHoursFilter +
             '&allocationFromFilter=' + allocFrom + '&allocationToFilter=' + allocTo,
        async: isAsync,
        success: callback
    });
