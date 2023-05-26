import * as Yup from "yup";
import {ValidationMessage} from "../../../../../common/enums/ValidationMessage.ts";

export const lessonFormValidationSchema = Yup.object().shape({
    number: Yup.number()
        .max(8, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    subgroup: Yup.number()
        .min(1, ValidationMessage.MAX_LENGTH)
        .max(2, ValidationMessage.MAX_LENGTH),
    time: Yup.object().required(ValidationMessage.REQUIRED),
    discipline: Yup.object().required(ValidationMessage.REQUIRED),
    teachers: Yup.array()
        .max(2, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    classrooms: Yup.array()
        .max(2, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
})