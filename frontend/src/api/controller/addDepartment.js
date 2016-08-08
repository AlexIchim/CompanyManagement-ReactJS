import $ from 'jquery';
import {default as apiconfig} from '../config';

    export default (departmentInput, isAsync, callback) =>
        $.ajax({
            method: 'POST',
            dataType: 'json',
            data: departmentInput,
            url: apiconfig.baseUrl + 'departments/add',
            async: isAsync,
            success: callback
        });
