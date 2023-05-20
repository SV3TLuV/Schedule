import * as Yup from 'yup'
import {ValidationMessage} from "../../../../../common/enums/ValidationMessage";
import {specialityFormValidationSchema} from "../../speciality/Dialogs/validation";
import {termValidationSchema} from "../../term/validation";

export const groupFormValidationSchema = Yup.object().shape({
    number: Yup.number()
        .max(2, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED),
    enrollmentYear: Yup.number().required(ValidationMessage.REQUIRED),
    speciality: specialityFormValidationSchema.required(ValidationMessage.REQUIRED),
    term: termValidationSchema.required(ValidationMessage.REQUIRED)
})