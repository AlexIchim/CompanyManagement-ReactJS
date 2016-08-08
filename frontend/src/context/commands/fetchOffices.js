import * as Immutable from 'immutable';

import * as Access from '../accessors';
import * as Controller from '../../api/controller';
import Context from '../Context';


const  fetchOffices = () => {
    Controller.getAllOffices(
        true,
        (data) => {
            Context.cursor.update(v => 
                v.set(Access.offices.prop, Immutable.fromJS(data))
            );
        }
    );
};

export default fetchOffices;