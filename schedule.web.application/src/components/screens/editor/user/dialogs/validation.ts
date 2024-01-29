import {ValidationMessage} from "../../../../../common/enums";

export const loginValidation = {
    // Сообщение о том, что поле обязательно
    // для заполнения
    required: ValidationMessage.REQUIRED,

    // Функция валидации введенных данных
    validate: (value: string) => {
        // Максимальная длинна строки
        const maxLength = 50

        // Проверка на максимальную длинну
        if (value.length > maxLength)
            // Возврат ошибки
            return ValidationMessage.MAX_LENGTH + maxLength

        // Возврат значения о успешной валидации
        return true
    }
}

export const passwordValidation = {
    required: ValidationMessage.REQUIRED,
    validate: (value: string) => {
        const maxLength = 100

        if (value.length > maxLength)
            return ValidationMessage.MAX_LENGTH + maxLength

        return true
    }
}

export const roleValidation = {
    required: ValidationMessage.REQUIRED,
}