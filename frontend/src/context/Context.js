import mock from '../data/user';
import * as Immutable from 'immutable';
import * as Cursor from 'immutable/contrib/cursor';
import * as Rx from 'rx';


const initialState = {
    user : mock,
    items: null,
    model:null,
    itemsDropdown: null
};

class Context{

    constructor(){
        this.cursor         = Cursor.from( Immutable.fromJS( initialState ), this.onContextChange.bind(this) );
        this.subject        = new Rx.BehaviorSubject(this.cursor);

    }

    onContextChange(newImmutable){
        this.cursor = Cursor.from( newImmutable, this.onContextChange.bind(this));
        this.subject.onNext(this.cursor);
    }


    subscribe(handleFunc){
        return this.subject.subscribe(handleFunc);
    }

}

export default new Context;