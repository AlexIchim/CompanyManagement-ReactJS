import $ from 'jquery';
import {default as apiconfig} from '../config';

    export default (department, isAsync, callback) => {

        console.log("in department controller update", department);
        $.ajax({
            method: 'PUT',
            url: apiconfig.baseUrl + 'departments/update',
            data: department,
            async: isAsync,
            success: callback
        })
    }
    ;
