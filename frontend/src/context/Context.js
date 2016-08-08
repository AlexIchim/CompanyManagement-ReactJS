import * as Immutable from 'immutable';
import * as Cursor from 'immutable/contrib/cursor';
import * as Rx from 'rx';

const initialState = {
    offices: [ {id: 0, name: "sample office"} ],
    currentOfficeId: null,
    currentDepartments: [],
    currentDepartmentId: null
};


export default new class Context{
    constructor(){
        this.cursor = Cursor.from( Immutable.fromJS( initialState ), this.onContextChange.bind(this) );
        this.subject = new Rx.BehaviorSubject(this.cursor);
    }

    onContextChange(newImmutable){
        this.cursor = Cursor.from( newImmutable, this.onContextChange.bind(this));
        this.subject.onNext(this.cursor);
    }

    subscribe(handleFunc){
        this.subject.subscribe(handleFunc);
    }
}