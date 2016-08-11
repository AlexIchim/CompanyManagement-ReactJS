function isNumeric(n) {
  return !isNaN(parseFloat(n)) && isFinite(n);
}
export default new class ValidatorEmployee{
    validateName(name) 
    {
        let errors = []
        if (name=="" || name== null)
            errors.push("Name should not be empty!\n")
        if (isNumeric(name))
            errors.push("Name should start with a letter!")
        return errors
    }
    validateAddress(address)
    {
        let errors = []
        if (address == "" || address == null)
            errors.push("Address should not be empty!")
        return errors
    }
}