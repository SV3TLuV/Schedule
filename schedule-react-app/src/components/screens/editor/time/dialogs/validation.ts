import {ValidationMessage} from "../../../../../common/enums";

export const startValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const regex = /^(?:[01][0-9]|2[0-4]):[0-5][0-9]$/;

        if (!regex.test(value))
            return ValidationMessage.INCORRECT_DATA

        return true
    }
}

export const endValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const regex = /^(?:[01][0-9]|2[0-4]):[0-5][0-9]$/;

        if (!regex.test(value))
            return ValidationMessage.INCORRECT_DATA

        return true
    }
}

export const durationValidation = {
    required: ValidationMessage.REQUIRED
}

export const lessonNumberValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: number) => {
        const maxValue = 8

        if (value > maxValue)
            return ValidationMessage.MAX_VALUE + 8

        return true
    }
}