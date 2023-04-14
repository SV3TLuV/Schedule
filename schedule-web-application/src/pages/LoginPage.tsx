import {LoginForm} from "../components/LoginForm/LoginForm";
import React from "react";

export const LoginPage = () => {
    return (
        <div className="flex justify-center items-center h-screen">
            <div className="shadow-2xl">
                <LoginForm/>
            </div>
        </div>
    )
}