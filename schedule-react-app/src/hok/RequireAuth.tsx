import {useTypedSelector} from "../hooks";
import {Navigate} from "react-router-dom";

interface IRequireAuthProps {
    children: any
}

export const RequireAuth = ({children}: IRequireAuthProps) => {
    const {user} = useTypedSelector(state => state.auth)

    if (!user) {
        return <Navigate to='/login'/>
    }

    return children
}