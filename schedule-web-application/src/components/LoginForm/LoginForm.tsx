import React from "react";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {ILoginCommand} from "../../features/commands/ILoginCommand";
import {loginFormValidationSchema} from "./LoginFormValidation";
import {yupResolver} from "@hookform/resolvers/yup";
import {Button, Card, Col, Container, Form, Row} from "react-bootstrap";
import {FaUser} from "react-icons/all";


export const LoginForm = () => {
    const {control, handleSubmit, formState: { errors }} = useForm<ILoginCommand>({
        resolver: yupResolver(loginFormValidationSchema),
        mode: "onChange"
    })

    const onSubmit: SubmitHandler<ILoginCommand> = async data => {

    }

    return (
        <Container>
            <Row className="mb-5">
                <Col md={8} xl={6} className="text-center mx-auto">
                    <h2>Авторизация</h2>
                </Col>
            </Row>
            <Row className="justify-content-center">
                <Col md={6} xl={4}>
                    <Card className="mb-5" style={{ borderRadius: '24px', boxShadow: '5px 10px 20px rgb(41,73,106)' }}>
                        <Card.Body className="d-flex flex-column align-items-center">
                            <div className="bs-icon-xl bs-icon-circle bs-icon-primary bs-icon my-4">
                                <FaUser size="40px"/>
                            </div>
                            <Form onSubmit={handleSubmit(onSubmit)} className="text-center" method="post">
                                <Controller
                                    name="login"
                                    control={control}
                                    render={({ field }) => (
                                        <Form.Group controlId="formAuthLogin" className="mb-3">
                                            <Form.Control
                                                placeholder="Логин"
                                                value={field.value}
                                                onChange={field.onChange}
                                            />
                                            {errors.login && (
                                                <Form.Text className="text-danger">
                                                    {errors.login?.message}
                                                </Form.Text>
                                            )}
                                        </Form.Group>
                                    )}
                                />
                                <Controller
                                    name="password"
                                    control={control}
                                    render={({ field }) => (
                                        <Form.Group controlId="formAuthPassword" className="mb-3">
                                            <Form.Control
                                                placeholder="Пароль"
                                                value={field.value}
                                                onChange={field.onChange}
                                            />
                                            {errors.password && (
                                                <Form.Text className="text-danger">
                                                    {errors.password?.message}
                                                </Form.Text>
                                            )}
                                        </Form.Group>
                                    )}
                                />
                                <Button variant="primary" type="submit" className="d-block w-100 mb-3">
                                    Войти
                                </Button>
                            </Form>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    )
}