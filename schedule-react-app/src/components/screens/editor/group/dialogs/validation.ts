import * as Yup from 'yup'
import {ValidationMessage} from "../../../../../common/enums/ValidationMessage";

export const groupFormValidationSchema = Yup.object().shape({
    number: Yup.number()
        .max(100, ValidationMessage.MAX_LENGTH)
        .required(ValidationMessage.REQUIRED)   ,
    enrollmentYear: Yup.number().required(ValidationMessage.REQUIRED),
    speciality: Yup.object().required(ValidationMessage.REQUIRED),
    term: Yup.object().required(ValidationMessage.REQUIRED)
})