import * as Yup from 'yup'
import {ValidationMessage} from "../../../../../common/enums/ValidationMessage";

export const disciplineTypeFormValidationSchema = Yup.object().shape({
    name: Yup.string()
        .max(30, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED)
})