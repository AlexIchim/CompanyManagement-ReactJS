import * as Immutable from 'immutable';

import * as Access from '../accessors';
import * as Controller from '../../api/controller';
import Context from '../Context';


const  fetchOffices = (callback) => {
    Controller.getAllOffices(
        true,
        (data) => {
            Context.cursor.update(v => 
                v.set(Access.offices.prop, Immutable.fromJS(data))
            );

            if(callback !== undefined && callback!== null){
                callback();
            }
        }
    );
};

export default fetchOffices;