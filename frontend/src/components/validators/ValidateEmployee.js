function isNumeric(n) {
  return !isNaN(parseFloat(n)) && isFinite(n);
}

function isAlpha(s){
    for(var i=0; i<s.length; i++)
      {
        var char1 = s.charAt(i);
            
        if (!((char1 >='a' && char1 <= 'z') || (char1 >= 'A' && char1 <= 'Z') || char1 == ' '))
            {
                return false
            }
      }
    return true
}

export default new class ValidatorEmployee{
    validateName(name) 
    {
        let errors = []
        if (name=="" || name== null)
            errors.push("Name should not be empty!\n")
         if (!isAlpha(name))
            errors.push("NName should only contain letters or whitespaces!")
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