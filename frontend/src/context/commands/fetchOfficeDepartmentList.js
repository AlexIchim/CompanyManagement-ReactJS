import * as Immutable from 'immutable';

import * as Access from '../accessors';
import * as Controller from '../../api/controller';
import Context from '../Context';


const  fetchOfficeDepartmentList = (callback) => {
    Controller.getDepartmentsByOffice(
        Access.currentOfficeId(Context.cursor),
        null,
        null,
        true,
        (data) => {
            Context.cursor.update(v =>
                v.set(Access.currentDepartments.prop, Immutable.fromJS(data))
            );

            if(callback !== undefined){
                callback();
            }
        }
    );
};

export default fetchOfficeDepartmentList;