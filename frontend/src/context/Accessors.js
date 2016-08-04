import Context from './Context';

export default new class Accessors{
    items(cursor){
        return cursor.get('items');
    }
    model(cursor){
        return cursor.get('model');
    }
    formToggle(cursor){
        return cursor.get('formToggle'); 
    }
}