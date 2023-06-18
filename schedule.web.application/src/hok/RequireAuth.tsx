import {useTypedSelector} from "../hooks";
import {Navigate} from "react-router-dom";

interface IRequireAuthProps {
    roles?: string[]
    children: any
}

export const RequireAuth = ({roles, children}: IRequireAuthProps) => {
    const {user} = useTypedSelector(state => state.auth)

    if (!user) {
        return <Navigate to='/login'/>
    }

    if (roles && !roles.includes(user.role.name)) {
        return <Navigate to='/login'/>
    }

    return children
}