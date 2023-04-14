import * as Yup from 'yup'
import {ValidationMessage} from "../../common/enums/ValidationMessage";

export const loginFormValidationSchema = Yup.object().shape({
    login: Yup.string().required(ValidationMessage.Required),
    password: Yup.string().required(ValidationMessage.Required),
})