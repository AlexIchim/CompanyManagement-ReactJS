import Context from './Context';

export default new class Accessors{
    items(cursor){
        return cursor.get('items');
    }
    model(cursor){
        return cursor.get('model');
    }
    dropdownItems(cursor){
        return cursor.get('dropdownItems');
    }
}