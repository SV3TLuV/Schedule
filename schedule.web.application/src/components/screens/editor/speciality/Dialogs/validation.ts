import {ValidationMessage} from "../../../../../common/enums";

export const specialityValidation = {
    required: ValidationMessage.REQUIRED
}

export const codeValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 20

        if (value.length > maxLength)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}

export const nameValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 20

        if (value.length > maxLength)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}

export const termValidation = {
    required: ValidationMessage.REQUIRED,
}