import {createBrowserRouter, createRoutesFromElements, Outlet, Route, useLocation} from "react-router-dom";
import {Routes} from "./common/enums/Routes.ts";
import {LoginPage} from "./pages/LoginPage/LoginPage.tsx";
import {AppNav} from "./components/AppNav/AppNav.tsx";
import {useAppDispatch, useTypedSelector} from "./hooks/redux.ts";
import {useEffect} from "react";
import {setCurrent} from "./redux/slices/routeSlice.tsx";

const Root = () => {
    const {isNavShowed} = useTypedSelector(state => state.route)
    const dispatch = useAppDispatch()
    const {pathname} = useLocation()

    useEffect(() => {
        dispatch(setCurrent(pathname as Routes))
    }, [dispatch, pathname])

    return (
        <div className='Root'>
            {isNavShowed && <AppNav/>}
            <Outlet/>
        </div>
    )
}

export const router = createBrowserRouter(
    createRoutesFromElements([
        <Route path='/' element={<Root/>}>
            <Route path={Routes.LOGIN} element={<LoginPage></LoginPage>}/>
            <Route path={Routes.REPORTS} element={<>Отчеты</>}/>
            <Route path={Routes.SCHEDULE} element={<>Расписание</>}/>
            <Route path={Routes.EDITOR} element={<>Редактор</>}/>
        </Route>
    ])
)