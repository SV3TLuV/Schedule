import * as Yup from 'yup'
import {ValidationMessage} from "../../../common/enums/ValidationMessage.ts";

export const loginFormValidationSchema = Yup.object().shape({
    login: Yup.string().required(ValidationMessage.REQUIRED),
    password: Yup.string().required(ValidationMessage.REQUIRED),
})