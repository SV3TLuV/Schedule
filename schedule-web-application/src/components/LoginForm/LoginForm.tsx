import 'bootstrap/dist/css/bootstrap.min.css';
import { Card, Form, Button } from 'react-bootstrap';

export const LoginForm = () => {
    return (
        <Card style={{ borderStyle: "none", borderRadius: "24px", boxShadow: "5px 5px 20px 0px rgb(89,116,143)" }}>
            <Card.Body style={{ padding: "36px", borderStyle: "none" }}>
                <p className="text-capitalize fs-1 fw-bolder text-center card-text" style={{ marginBottom: "30px" }}>
                    Авторизация
                </p>
                <Form>
                    <Form.Group controlId="Login">
                        <Form.Control type="text" placeholder="Логин" style={{ marginBottom: "20px" }} />
                    </Form.Group>
                    <Form.Group controlId="Password">
                        <Form.Control type="password" placeholder="Пароль" style={{ marginBottom: "20px" }} />
                    </Form.Group>
                    <Button variant="primary" type="submit" style={{ width: "100%" }}>
                        Войти
                    </Button>
                </Form>
            </Card.Body>
        </Card>
    );
};