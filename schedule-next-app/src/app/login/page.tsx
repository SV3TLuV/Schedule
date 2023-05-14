import {LoginForm} from "@/components/LoginForm/loginForm";
import {Metadata} from "next";

export const metadata: Metadata = {
    title: 'Вход',
}

export default function Home() {
    return (
        <div className="container-fluid d-flex d-xxl-flex justify-content-center align-items-center justify-content-xxl-center align-items-xxl-center"
             style={{ height: "100vh" }}>
            <LoginForm/>
        </div>
    )
}