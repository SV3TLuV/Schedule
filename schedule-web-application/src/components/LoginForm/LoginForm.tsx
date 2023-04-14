import React from "react";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {ILoginCommand} from "../../features/commands/ILoginCommand";
import {Button, Card, Col, Form} from "react-bootstrap";
import {BsPersonCircle} from "react-icons/bs";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import {ValidationMessage} from "../../common/enums/ValidationMessage";
import {loginFormValidationSchema} from "./LoginFormValidation";
import {yupResolver} from "@hookform/resolvers/yup";

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
        <Card className="mb-5" style={{ borderRadius: '24px', boxShadow: '5px 10px 20px rgb(41,73,106)' }}>
            <Card.Header>
                <Col className="col-md-8 col-xl-6 text-center mx-auto">
                    <h2>Авторизация</h2>
                </Col>
            </Card.Header>
            <Card.Body className="d-flex flex-column align-items-center">
                <div className="bs-icon-xl bs-icon-circle bs-icon-primary bs-icon my-4">
                    <BsPersonCircle />
                </div>
                <Form className="text-center" method="post" onSubmit={handleSubmit(onSubmit)}>
                    <Controller
                        name="login"
                        control={control}
                        render={({ field }) => (
                            <Form.Group controlId="formLogin">
                                <Form.Control
                                    placeholder="Логин"
                                    className="mb-3"
                                    value={field.value}
                                    onChange={field.onChange}
                                />
                                <Form.Control.Feedback type="invalid">
                                    {errors.login?.message}
                                </Form.Control.Feedback>
                            </Form.Group>
                        )}
                    />
                    <Form.Group controlId="formPassword">
                        <Controller
                            name="password"
                            control={control}
                            render={({ field }) => (
                                <Form.Group controlId="formPassword">
                                    <Form.Control
                                        placeholder="Пароль"
                                        className="mb-3"
                                        value={field.value}
                                        onChange={field.onChange}
                                    />
                                    <Form.Control.Feedback type="invalid">
                                        {"Лохи ебаные"}
                                    </Form.Control.Feedback>
                                </Form.Group>
                            )}
                        />
                    </Form.Group>
                    <Button variant="primary" type="submit" className="d-block w-100 mb-3">
                        Войти
                    </Button>
                </Form>
            </Card.Body>
        </Card>
    )
}