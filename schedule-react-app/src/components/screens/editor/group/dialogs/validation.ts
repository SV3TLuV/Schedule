import {ValidationMessage} from "../../../../../common/enums";
import {IGroup} from "../../../../../features/models";

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

export const mergedGroupsValidation = {
    validate: (value: IGroup[]) => {
        const maxLength = 1;

        if (value.length > maxLength)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}