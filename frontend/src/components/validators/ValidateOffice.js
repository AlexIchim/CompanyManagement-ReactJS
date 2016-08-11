function isNumeric(n) {
  return !isNaN(parseFloat(n)) && isFinite(n);
}

export default new class ValidatorOffice {
    validateName(name)
    {
        let errors = []
        if (name=="" || name== null)
            errors.push("Name should not be empty!")
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
    validatePhoneNumber(phoneNumber)
    {
        let errors = []
        if (phoneNumber== "" || phoneNumber == null)
            errors.push("Phone number should not be wmpty!")
        if (!isNumeric(phoneNumber))
            errors.push("Phone number should contain only numbers!")
        if (phoneNumber.length > 15)
            errors.push("Phone number should not exceed 15 digits!")
        return errors
    }
}