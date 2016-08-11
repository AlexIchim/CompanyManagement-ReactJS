function isNumeric(n) {
  return !isNaN(parseFloat(n)) && isFinite(n);
}

export default (input)=>{
    if (input.Name=="" || input.Name== null)
        alert("Name should not be empty!")
    if (isNumeric(input.Name))
        alert("Name should not contain numbers!")
    if (!isNumeric(input.Duration))
        alert("Duration should contain only numbers!")
}