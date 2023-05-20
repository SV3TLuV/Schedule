import * as Yup from 'yup'
import {ValidationMessage} from "../../../../../common/enums/ValidationMessage";

export const teacherFormValidatorSchema = Yup.object().shape({
    name: Yup.string()
        .max(40, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    surname: Yup.string()
        .max(40, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    middleName: Yup.string()
        .max(40, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    email: Yup.string()
        .max(200, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    groups: Yup.array(),
    disciplines: Yup.array()
})