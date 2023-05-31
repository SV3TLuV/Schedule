import {ValidationMessage} from "../../../../../common/enums/ValidationMessage";

export const groupValidation = {
    required: ValidationMessage.REQUIRED
}

export const numberValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 2

        if (value.length != maxLength)
            return ValidationMessage.REQUIRED_LENGTH + maxLength

        return true
    }
}

export const enrollmentYearValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: number) => {
        const requiredLength = 4
        const valueString = value.toString()

        if (isNaN(value))
            return ValidationMessage.INCORRECT_DATA

        if (valueString.length != requiredLength)
            return ValidationMessage.REQUIRED_LENGTH + requiredLength

        return true
    }
}