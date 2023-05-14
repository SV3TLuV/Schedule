import {LoginForm} from "../components/LoginForm/LoginForm";

export const LoginPage = () => {
    return (
        <div className="container-fluid d-flex d-xxl-flex justify-content-center align-items-center justify-content-xxl-center align-items-xxl-center"
             style={{ height: "calc(100vh - 72px)" }}>
            <LoginForm/>
        </div>
    )
}