import {ValidationMessage} from "../../../../../common/enums";

export const nameValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 40

        if (value.length > maxLength)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}

export const surnameValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 40

        if (value.length > maxLength)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}

export const middleNameValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 40

        if (value.length > maxLength)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}

export const emailValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 200

        if (value.length > maxLength)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}