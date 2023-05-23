import * as Yup from 'yup'
import {ValidationMessage} from "../../../../../common/enums/ValidationMessage";

export const disciplineFormValidationSchema = Yup.object().shape({
    name: Yup.string()
        .max(50, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    code: Yup.string()
        .max(20, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    totalHours: Yup.number().required(ValidationMessage.REQUIRED),
    type: Yup.object().required(ValidationMessage.REQUIRED),
    term: Yup.object().required(ValidationMessage.REQUIRED),
    speciality: Yup.object().required(ValidationMessage.REQUIRED)
})