function isNumeric(n) {
    if (n!="")
  return !isNaN(parseFloat(n)) && isFinite(n);
}

function isAlpha(s){
    for(var i=0; i<s.length; i++)
      {
        var char1 = s.charAt(i);
            
         if (!((char1 >='a' && char1 <= 'z') || (char1 >= 'A' && char1 <= 'Z') || char1 == ' ' || char1 == '-'))
            {
                return false
            }
      }
    return true
}

function getDigitsNumber(n)
{
    let nr = 0
    for(var i=0; i<n.length; i++)
      {
        var char1 = n.charAt(i);
        if (isNumeric(char1))
        {
            nr++
        }
      }
    return nr
}

function isCorrectPhoneNumber(n)
{
    for(var i=0; i<n.length; i++)
      {
        var char1 = n.charAt(i);
        if (!isNumeric(char1) && char1 !== " " && (char1 !== "+" || i!=0) && char1 !== "-")
            return false
      }
    return true
}

export default new class ValidatorOffice {
    validateName(name)
    {
        let errors = []
        if (name=="" || name== null)
            errors.push("Name should not be empty!")
        if (!isAlpha(name))
            errors.push("Name should only contain letters', '-' or whitespaces!")
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
            errors.push("Phone number should not be empty!")
        if (!isCorrectPhoneNumber(phoneNumber))
            errors.push("Phone number doesn't have the correct format!")
        if (getDigitsNumber(phoneNumber) > 15)
            errors.push("Phone number should not exceed 15 digits!")
        return errors
    }
}