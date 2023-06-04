import {Button, Card, Col, Container, Form, Row} from "react-bootstrap";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {ILoginCommand} from "../../../features/commands";
import {useNavigation} from "../../../hooks";
import {loginValidation, passwordValidation} from "./validation.ts";
import {FaUser} from "react-icons/fa";
import {useTypedSelector} from "../../../hooks";
import {Navigate} from "react-router-dom";
import {useLoginMutation} from "../../../store/apis";

export const LoginForm = () => {
    const {user} = useTypedSelector(state => state.auth)
    const [login] = useLoginMutation()

    const {control, handleSubmit, formState: { errors }} = useForm<ILoginCommand>({
        mode: 'onChange'
    })
    const {navigateTo} = useNavigation();

    const onSubmit: SubmitHandler<ILoginCommand> = async data => {
        await login(data)
    }

    const navigateToSchedule = () => navigateTo('/schedule/search')

    return (
        <Container>
            {user &&
                <Navigate to='/schedule/search'/>
            }
            <Row className='justify-content-center'>
                <Col md={6} xl={4}>
                    <Card
                        className='mb-5'
                        style={{
                            borderRadius: '24px',
                            boxShadow: '5px 10px 20px rgb(41,73,106)'
                        }}
                    >
                        <Card.Body className='d-flex flex-column align-items-center'>
                            <Card.Title
                                style={{
                                    fontSize: '24px',
                                    fontWeight: 'bolder'
                                }}
                            >
                                Вход
                            </Card.Title>
                            <div className='bs-icon-xl bs-icon-circle bs-icon-primary bs-icon my-4'>
                                <FaUser size='40px'/>
                            </div>
                            <Form
                                onSubmit={handleSubmit(onSubmit)}
                                className='text-center'
                            >
                                <Controller
                                    name='login'
                                    control={control}
                                    rules={loginValidation}
                                    render={({ field }) => (
                                        <Form.Group className='mb-3'>
                                            <Form.Control
                                                placeholder='Логин'
                                                value={field.value}
                                                onChange={field.onChange}
                                            />
                                            {errors.login && (
                                                <Form.Text className='text-danger'>
                                                    {errors.login?.message}
                                                </Form.Text>
                                            )}
                                        </Form.Group>
                                    )}
                                />
                                <Controller
                                    name='password'
                                    control={control}
                                    rules={passwordValidation}
                                    render={({ field }) => (
                                        <Form.Group className='mb-3'>
                                            <Form.Control
                                                placeholder='Пароль'
                                                value={field.value}
                                                onChange={field.onChange}
                                            />
                                            {errors.password && (
                                                <Form.Text className='text-danger'>
                                                    {errors.password?.message}
                                                </Form.Text>
                                            )}
                                        </Form.Group>
                                    )}
                                />
                                <Button
                                    className='d-block w-100 mb-3'
                                    type='submit'
                                    variant='primary'
                                >
                                    Войти
                                </Button>
                                <Button
                                    className='d-block w-100 mb-3'
                                    variant='outline-primary'
                                    onClick={navigateToSchedule}
                                >
                                    На главную
                                </Button>
                            </Form>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    )
}