import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {ILoginCommand} from "../../features/commands/ILoginCommand";
import {loginFormValidationSchema} from "./LoginFormValidation";
import {yupResolver} from "@hookform/resolvers/yup";
import {Card, CardContent, CardHeader} from "@mui/material";

export const LoginForm = () => {
    const {
        control,
        handleSubmit,
        formState: { errors }
    } = useForm<ILoginCommand>({
        resolver: yupResolver(loginFormValidationSchema),
        mode: "onChange"
    })

    const onSubmit: SubmitHandler<ILoginCommand> = async data => {
        console.log(data)
    }

    return (
        <Card>
            <CardHeader>
                <h2>Авторизация</h2>
            </CardHeader>
            <CardContent>

            </CardContent>
        </Card>
    )
}