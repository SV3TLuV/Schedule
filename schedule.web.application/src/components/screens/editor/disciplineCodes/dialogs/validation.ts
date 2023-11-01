import {ValidationMessage} from "../../../../../common/enums";

export const codeValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 20

        if (value.length > 20)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}