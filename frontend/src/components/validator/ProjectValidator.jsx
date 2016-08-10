import ValidationResult from './ValidationResult';

export default new class ProjectValidator{

    ValidateName(name){
        if(!name){
            return new ValidationResult(false, "Name is mandatory")
        }
        if(name.length>30){
            return new ValidationResult(false, "Name must have less than 30 characters.");
        }

        return new ValidationResult(true, "Valid.");
    }

    ValidateDuration(duration){
        if(!duration){
            return new ValidationResult(false, "Duration is mandatory")
        }
        if(duration <=0){
            return new ValidationResult(false, "Duration must be a positive number")
        }
        return new ValidationResult(true, "Valid")
    }

    ValidateAllocation(allocation){
        if(!allocation){
            return new ValidationResult(false, "Allocation is mandatory")
        }
        if(allocation > 100){
            return new ValidationResult(false, "Allocation should be less than 100")
        }
        return new ValidationResult(true, "Valid")
    }

}