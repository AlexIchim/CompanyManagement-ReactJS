const prop = 'currentOfficeId';
const currentOfficeId = function currentOfficeId(cursor) {
    return cursor.get(prop);
}

currentOfficeId.prop = prop;
export default currentOfficeId;