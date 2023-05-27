import {ValidationMessage} from "../../../../../common/enums/ValidationMessage.ts";

export const lessonValidation = {
    required: ValidationMessage.REQUIRED
}

export const numberValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: number) => {
        const maxValue = 8

        if (value > maxValue)
            return ValidationMessage.MAX_VALUE + maxValue

        return true
    }
}

export const subgroupValidation = {
    validate: (value: number | null) => {
        const maxValue = 2
        const minValue = 1

        if (value === null)
            return true

        if (value > maxValue)
            return ValidationMessage.MAX_VALUE + maxValue

        if (value < minValue)
            return ValidationMessage.MIN_VALUE + minValue

        return true
    }
}

export const teachersValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: object[]) => {
        const maxLength = 2

        if (value.length > maxLength)
            return ValidationMessage.MAX_VALUE + maxLength

        return true
    }
}

export const classroomsValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: object[]) => {
        const maxLength = 2

        if (value.length > maxLength)
            return ValidationMessage.MAX_VALUE + maxLength

        return true
    }
}