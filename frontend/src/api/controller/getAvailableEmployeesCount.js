import $ from 'jquery';
import {default as apiconfig} from '../config';

export default (departmentId, positionId, projectId, isAsync, callback) =>
    $.ajax({
        method: 'GET',
        url: apiconfig.baseUrl + 'employees/available/count',
        data: {departmentId: departmentId, positionId: positionId, projectId: projectId},
        async: true,
        success: callback
    });