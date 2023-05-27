import {ValidationMessage} from "../../../../common/enums/ValidationMessage.ts";

export const disciplineTypeValidation = {
    required: ValidationMessage.REQUIRED
}

export const nameValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 30

        if (value.length > maxLength)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}