import 'bootstrap/dist/css/bootstrap.min.css';
import {createBrowserRouter, createRoutesFromElements, Outlet, Route, RouterProvider} from "react-router-dom";
import {LoginPage} from "./pages/LoginPage";
import {EditorPage} from "./pages/EditorPage";
import {ReportsPage} from "./pages/ReportsPage";
import {AppNav} from "./components/AppNav";
import {RoutePath} from "./common/enums/RoutePath.";
import {GroupEditor} from "./components/GroupEditor/GroupEditor";


export default function App() {
    return (
        <div className="App">
            <RouterProvider router={router}/>
        </div>
    )
}

const Root = () => {
    return (
        <div className="Root">
            <AppNav/>
            <Outlet/>
        </div>
    )
}

export const router = createBrowserRouter(
    createRoutesFromElements(
        <Route path="/" element={<Root/>}>
            <Route path={RoutePath.LOGIN} element={<LoginPage/>}/>
            <Route path={RoutePath.EDITOR} element={<EditorPage/>}/>
            <Route path={RoutePath.REPORTS} element={<ReportsPage/>}/>
        </Route>
    )
)