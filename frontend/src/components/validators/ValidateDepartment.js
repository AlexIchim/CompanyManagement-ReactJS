function isNumeric(n) {
  return !isNaN(parseFloat(n)) && isFinite(n);
}
export default new class ValidatorDepartment{
    validateName(name) 
    {
        let errors = []
    if (name=="" || name== null)
        errors.push("Name should not be empty!\n")
    if (isNumeric(name))
         errors.push("Name should not contain numbers!")
    console.log(errors)
    console.log(name)
    return errors
    }
}