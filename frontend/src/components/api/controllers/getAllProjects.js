import $ from 'jquery';
import {default as apiconfig} from '../../api/config';

export default (id, isAsync, callback) =>
    $.ajax({
        method : 'GET',
        url: apic
    })