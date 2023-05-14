import 'bootstrap/dist/css/bootstrap.min.css';
import {createBrowserRouter, createRoutesFromElements, Outlet, Route, RouterProvider} from "react-router-dom";
import {LoginPage} from "./pages/LoginPage";
import {EditorPage} from "./pages/Editor/EditorPage";
import {ReportsPage} from "./pages/ReportsPage";
import {AppNav} from "./components/AppNav";
import {Provider} from "react-redux";
import {persistor, store} from "./redux/store";
import {PersistGate} from "redux-persist/integration/react";
import {Routes} from "./common/enums/Routes";

export default function App() {
    return (
        <Provider store={store}>
            <PersistGate loading={null} persistor={persistor}>
                <RouterProvider router={router}/>
            </PersistGate>
        </Provider>
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
    createRoutesFromElements([
        <Route path="/" element={<Root/>}>
            <Route path={Routes.LOGIN} element={<LoginPage/>}/>
            <Route path={Routes.REPORTS} element={<ReportsPage/>}/>
            <Route path={Routes.EDITOR} element={<EditorPage/>}/>
        </Route>
    ])
)