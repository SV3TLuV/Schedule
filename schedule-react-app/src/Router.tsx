import {createBrowserRouter, createRoutesFromElements, Outlet, Route, useLocation} from "react-router-dom";
import {Routes} from "./common/enums/Routes.ts";
import {LoginPage} from "./pages/LoginPage/LoginPage.tsx";
import {AppNav} from "./components/AppNav/AppNav.tsx";
import {useAppDispatch, useTypedSelector} from "./hooks/redux.ts";
import {useEffect} from "react";
import {setCurrent} from "./redux/slices/routeSlice.tsx";

const Root = () => {
    const {current} = useTypedSelector(state => state.route)
    const dispatch = useAppDispatch()
    const {pathname} = useLocation()

    useEffect(() => {
        dispatch(setCurrent(pathname.substring(1, pathname.length) as Routes))
    }, [dispatch, pathname])

    const isShowNav = ![
        Routes.LOGIN
    ].includes(current)

    return (
        <div className='Root'>
            {isShowNav && <AppNav/>}
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