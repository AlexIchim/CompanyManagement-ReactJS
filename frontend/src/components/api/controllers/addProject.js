import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

export default (id, isAsync, callback) =>
    $.ajax({
        method : 'POST',
        url : apiconfig.baseUrl + 'departments/' + id +
    })