import * as Yup from 'yup'
import {ValidationMessage} from "../../../../common/enums/ValidationMessage";
import {courseValidationSchema} from "../course/validation";

export const termValidationSchema = Yup.object().shape({
    id: Yup.number().required(ValidationMessage.REQUIRED),
    courseTerm: Yup.number().required(ValidationMessage.REQUIRED),
    course: courseValidationSchema.required(ValidationMessage.REQUIRED)
})