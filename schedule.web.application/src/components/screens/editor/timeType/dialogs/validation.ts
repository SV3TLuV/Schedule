import {ValidationMessage} from "../../../../../common/enums";

export const timeTypeValidation = {
    required: ValidationMessage.REQUIRED
}

export const nameValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 50

        if (value.length > maxLength)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}