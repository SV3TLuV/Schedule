import {ValidationMessage} from "../../../../../common/enums";

export const numberValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 2
        const numberRegex = /^\d+$/;

        if (!numberRegex.test(value))
            return ValidationMessage.INCORRECT_DATA

        if (value.length != maxLength)
            return ValidationMessage.REQUIRED_LENGTH + maxLength

        return true
    }
}

export const enrollmentYearValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const requiredLength = 4
        const numberRegex = /^\d+$/;

        if (!numberRegex.test(value))
            return ValidationMessage.INCORRECT_DATA

        if (value.length != requiredLength)
            return ValidationMessage.REQUIRED_LENGTH + requiredLength

        return true
    }
}