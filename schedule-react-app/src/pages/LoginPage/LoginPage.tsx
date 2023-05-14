import {LoginForm} from "../../components/LoginForm/LoginForm.tsx";
import {Container} from "react-bootstrap";

export const LoginPage = () => {
    return (
        <Container
            className='d-flex justify-content-center align-items-center'
            style={{ height: '100vh' }}
        >
            <LoginForm/>
        </Container>
    )
}