import * as Immutable from 'immutable';

import * as Access from '../accessors';
import Context from '../Context';

const  setCurrentDepartment = (id) => {
    if(Access.currentDepartmentId(Context.cursor) !== id){
        Context.cursor.update(v =>
            v.set(Access.currentDepartmentId.prop, id)
        );
    }
};

export default setCurrentDepartment;