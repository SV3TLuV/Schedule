import React from "react";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {ILoginCommand} from "../../features/commands/ILoginCommand";
import {loginFormValidationSchema} from "./LoginFormValidation";
import {yupResolver} from "@hookform/resolvers/yup";
import {Button, Card, CardContent, CardHeader, makeStyles, TextField} from "@mui/material";


export const LoginForm = () => {
    const {control, handleSubmit, formState: { errors }} = useForm<ILoginCommand>({
        resolver: yupResolver(loginFormValidationSchema),
        mode: "onChange"
    })

    const onSubmit: SubmitHandler<ILoginCommand> = async data => {
        console.log(data)
    }

    return (
        <Card>
            <CardContent>
                <form className="space-y-4">
                    <div>
                        <label className="block font-medium mb-2" htmlFor="email">Email</label>
                        <input className="border border-gray-400 p-2 w-full" type="email" id="email" name="email"
                               required/>
                    </div>
                    <div>
                        <label className="block font-medium mb-2" htmlFor="password">Пароль</label>
                        <input className="border border-gray-400 p-2 w-full" type="password" id="password" name="password" required/>
                    </div>
                    <div >
                        <Button variant="contained" color="primary" type="submit">Войти</Button>
                    </div>
                </form>
            </CardContent>
        </Card>
    )
}