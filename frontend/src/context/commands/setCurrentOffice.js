import * as Immutable from 'immutable';

import * as Access from '../accessors';
import * as Controller from '../../api/controller';
import Context from '../Context';


const  setCurrentOffice = (id) => {
    if(id !== Access.currentOfficeId(Context.cursor)) {
        Context.cursor.update(v =>
            v.set(Access.currentOfficeId.prop, id)
        );

        if(id!==null && id!==undefined){
            Controller.getDepartmentsByOffice(
                id,
                null, 
                null,
                true,
                (data) => {
                    Context.cursor.update(v => 
                        v.set(Access.currentDepartments.prop, Immutable.fromJS(data))
                    );
                }
            );
        }
    }
};

export default setCurrentOffice;