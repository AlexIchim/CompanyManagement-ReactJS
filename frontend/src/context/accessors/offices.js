const prop = 'offices';
const offices = function offices(cursor) {
    return cursor.get(prop);
}

offices.prop = prop;
export default offices;