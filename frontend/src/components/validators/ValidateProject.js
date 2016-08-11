function isNumeric(n) {
  return !isNaN(parseFloat(n)) && isFinite(n);
}

export default new class ValidatorProject {
    validateName(name)
    {
        let errors = []
        if (name=="" || name== null)
            errors.push("Name should not be empty!")
        if (isNumeric(name))
            errors.push("Name should start with a letter!")
        return errors
    }
    validateDuration(duration)
    {
        let errors = []
        if (duration == 0)
            errors.push("Duration should not be empty!")
        if (!isNumeric(duration))
            errors.push("Duration should contain only numbers!")
        return errors
    }
}