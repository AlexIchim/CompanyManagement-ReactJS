function isNumeric(n) {
  return !isNaN(parseFloat(n)) && isFinite(n);
}

function isAlpha(s){
    console.log("length", s.length)
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

export default new class ValidatorProject {
    validateName(name)
    {
        let errors = []
        if (name=="" || name== null)
            errors.push("Name should not be empty!")
        if (!isAlpha(name))
            errors.push("Name should only contain letters or whitespaces!")
        return errors
    }
    validateDuration(duration)
    {
        let errors = []
        if (duration == null || duration == "")
            errors.push("Duration should not be empty!")
        if (!isNumeric(duration))
            errors.push("Duration should only contain numbers!")
        return errors
    }
}