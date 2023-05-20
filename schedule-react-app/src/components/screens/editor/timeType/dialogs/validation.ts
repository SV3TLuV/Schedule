import * as Yup from 'yup'
import {ValidationMessage} from "../../../../../common/enums/ValidationMessage";

export const timeTypeFormValidationSchema = Yup.object().shape({
    name: Yup.string()
        .max(50, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED)
})