import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (departmentId, positionId, projectId, pageSize, pageNumber, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'employees/available',
        data: {departmentId: departmentId, positionId: positionId, projectId: projectId, pageSize: pageSize, pageNumber: pageNumber},
        async: true,
        success: callback
    });