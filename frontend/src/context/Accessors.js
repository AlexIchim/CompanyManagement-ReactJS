

export default new class Accessors{
    items(cursor){
        return cursor.get('items');
    }
    sidebarOffices(cursor){
        return cursor.get('sidebarOffices');
    }
    model(cursor){
        return cursor.get('model');
    }
    dropdownItems(cursor){
        return cursor.get('dropdownItems');
    }
    itemsDescriptions(cursor){
        return cursor.get('itemsDescriptions');
    }
}