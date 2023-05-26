import * as Yup from 'yup'
import {ValidationMessage} from "../../../../../common/enums/ValidationMessage";

export const specialityFormValidationSchema = Yup.object().shape({
    code: Yup.string()
        .max(20, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    name: Yup.string()
        .max(20, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    maxTermId: Yup.number().required(ValidationMessage.REQUIRED),
})