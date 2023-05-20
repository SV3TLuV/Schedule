import * as Yup from 'yup'
import {ValidationMessage} from "../../../../../common/enums/ValidationMessage.ts";

export const classroomFormValidationSchema = Yup.object().shape({
    cabinet: Yup.string()
        .max(10, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    types: Yup.array()
        .min(1, ValidationMessage.REQUIRED)
        .required(ValidationMessage.REQUIRED),
})