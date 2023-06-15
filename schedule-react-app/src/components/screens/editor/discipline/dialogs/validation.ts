import {ValidationMessage} from "../../../../../common/enums";

export const nameValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 50

        if (value.length > maxLength)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}

export const codeValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 20

        if (value.length > 20)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}

export const totalHoursValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const numberRegex = /^\d+$/;

        if (!numberRegex.test(value))
            return ValidationMessage.INCORRECT_DATA

        return true
    }
}