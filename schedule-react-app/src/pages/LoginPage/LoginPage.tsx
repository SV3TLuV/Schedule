import {Container} from "react-bootstrap";
import {LoginForm} from "../../components/Forms/LoginForm/LoginForm.tsx";

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