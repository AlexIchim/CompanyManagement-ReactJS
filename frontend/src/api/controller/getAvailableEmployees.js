import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (departmentId, positionId, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'employees/available',
        data: {departmentId: departmentId, positionId: positionId},
        async: true,
        success: callback
    });