import {LoginForm} from "../components/LoginForm/LoginForm";
import {Col, Container, Row} from "react-bootstrap";

export const LoginPage = () => {
    return (
        <Container
            fluid
            style={{ height: "100vh" }}
            className="d-flex justify-content-center align-items-center"
        >
            <section className="flex-fill py-4 py-xl-5">
                <Row className="justify-content-center">
                    <Col md={6} xl={4}>
                        <LoginForm />
                    </Col>
                </Row>
            </section>
        </Container>
    )
}