const prop = 'currentDepartments';
const currentDepartments = function currentDepartments(cursor) {
    return cursor.get(prop);
}

currentDepartments.prop = prop;
export default currentDepartments;