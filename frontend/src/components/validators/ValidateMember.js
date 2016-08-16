function isNumeric(n) {
  return !isNaN(parseFloat(n)) && isFinite(n);
}

export default new class ValidatorMember {
    validateAllocation(allocation)
    {
        let errors = []
        if (allocation <= 0 || allocation >100)
            errors.push("0<allocation<=100")
        return errors
    }
}