import {ValidationMessage} from "../../../../../common/enums/ValidationMessage.ts";
import {ITeacher} from "../../../../../features/models/ITeacher.ts";
import {IClassroom} from "../../../../../features/models/IClassroom.ts";

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
    validate: (value: ITeacher[]) => {
        const maxLength = 2

        if (value.length > maxLength)
            return ValidationMessage.MAX_VALUE + maxLength

        return true
    }
}

export const classroomsValidation = {
    validate: (value: IClassroom[]) => {
        const maxLength = 2

        if (value.length > maxLength)
            return ValidationMessage.MAX_VALUE + maxLength

        return true
    }
}