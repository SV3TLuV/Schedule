import {ValidationMessage} from "../../../../../common/enums";

export const nameValidation = {
    required: ValidationMessage.REQUIRED
}

export const codeValidation = {
    required: ValidationMessage.REQUIRED
}

export const totalHoursValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: number) => {
        const numberRegex = /^\d+$/;

        if (!numberRegex.test(value.toString()))
            return ValidationMessage.INCORRECT_DATA

        return true
    }
}