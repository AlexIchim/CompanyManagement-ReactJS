import * as Immutable from 'immutable';

import * as Access from '../accessors';
import * as Controller from '../../api/controller';
import Context from '../Context';

import * as Command from '.';

const  setCurrentOffice = (id, callback) => {
    if(id !== Access.currentOfficeId(Context.cursor)) {
        Context.cursor.update(v =>
            v.set(Access.currentOfficeId.prop, id)
        );

        if(id!==null && id!==undefined){
            Command.fetchOfficeDepartmentList(callback);
        }
    }
};

export default setCurrentOffice;