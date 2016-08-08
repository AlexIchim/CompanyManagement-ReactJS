const prop = 'currentDepartmentId';
const currentDepartmentId = function currentDepartmentId(cursor) {
    return cursor.get(prop);
}

currentDepartmentId.prop = prop;
export default currentDepartmentId;