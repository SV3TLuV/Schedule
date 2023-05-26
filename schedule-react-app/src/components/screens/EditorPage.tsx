import {Navigate, Outlet} from "react-router-dom";

export const EditorPage = () => {
    return (
        <>
            <Navigate to='/editor/lessons'/>
            <Outlet/>
        </>
    )
}