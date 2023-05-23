import * as Yup from 'yup'
import {ValidationMessage} from "../../../../common/enums/ValidationMessage";

export const courseValidationSchema = Yup.object().shape({
    value: Yup.number().required(ValidationMessage.REQUIRED)
})