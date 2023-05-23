import * as Yup from 'yup'
import {ValidationMessage} from "../../../../../common/enums/ValidationMessage";

export const timeFormValidationSchema = Yup.object().shape({
    start: Yup.string().required(ValidationMessage.REQUIRED),
    end: Yup.string().required(ValidationMessage.REQUIRED),
    duration: Yup.number().required(ValidationMessage.REQUIRED),
    lessonNumber: Yup.number().required(ValidationMessage.REQUIRED),
    type: Yup.object().required(ValidationMessage.REQUIRED)
})