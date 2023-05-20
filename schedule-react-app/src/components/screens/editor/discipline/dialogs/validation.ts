import * as Yup from 'yup'
import {ValidationMessage} from "../../../../../common/enums/ValidationMessage";
import {disciplineTypeFormValidationSchema} from "../../disciplineType/dialogs/validation";
import {termValidationSchema} from "../../term/validation";
import {specialityFormValidationSchema} from "../../speciality/Dialogs/validation";

export const disciplineFormValidationSchema = Yup.object().shape({
    name: Yup.string()
        .max(50, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    code: Yup.string()
        .max(20, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    totalHours: Yup.number().required(ValidationMessage.REQUIRED),
    type: disciplineTypeFormValidationSchema.required(ValidationMessage.REQUIRED),
    term: termValidationSchema.required(ValidationMessage.REQUIRED),
    speciality: specialityFormValidationSchema.required(ValidationMessage.REQUIRED)
})