import {ValidationMessage} from "../../../../../common/enums";

export const numberValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const requiredLength = 2
        const numberRegex = /^\d+$/;

        if (!numberRegex.test(value))
            return ValidationMessage.INCORRECT_DATA

        if (value.length != requiredLength)
            return ValidationMessage.REQUIRED_LENGTH + requiredLength

        return true
    }
}

export const enrollmentYearValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: number) => {
        const requiredLength = 4
        const stringValue = value.toString()
        const numberRegex = /^\d+$/;

        if (!numberRegex.test(stringValue))
            return ValidationMessage.INCORRECT_DATA

        if (stringValue.length != requiredLength)
            return ValidationMessage.REQUIRED_LENGTH + requiredLength

        return true
    }
}