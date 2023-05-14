import {createBrowserRouter, createRoutesFromElements, Outlet, Route} from "react-router-dom";
import {Routes} from "./common/enums/Routes.ts";
import {LoginPage} from "./pages/LoginPage/LoginPage.tsx";

const Root = () => {
    return (
        <div className="Root">
            <Outlet/>
        </div>
    )
}

export const router = createBrowserRouter(
    createRoutesFromElements([
        <Route path="/" element={<Root/>}>
            <Route path={Routes.LOGIN} element={<LoginPage></LoginPage>}/>
            <Route path={Routes.REPORTS} element={<></>}/>
            <Route path={Routes.EDITOR} element={<></>}/>
        </Route>
    ])
)