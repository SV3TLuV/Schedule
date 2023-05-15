import {createBrowserRouter, createRoutesFromElements, Outlet, Route, useLocation} from "react-router-dom";
import {Routes} from "./common/enums/Routes.ts";
import {LoginPage} from "./pages/LoginPage/LoginPage.tsx";
import {AppNav} from "./components/AppNav/AppNav.tsx";
import {useAppDispatch, useTypedSelector} from "./hooks/redux.ts";
import {useEffect} from "react";
import {setCurrent} from "./redux/slices/routeSlice.tsx";
import {ReportsPage} from "./pages/ReportsPage/ReportsPage.tsx";
import {PairsEditor} from "./pages/Editors/PairsEditor/PairsEditor.tsx";
import {ClassroomsEditorPage} from "./pages/Editors/ClassroomsEditor/ClassroomsEditorPage.tsx";
import {DisciplinesEditorPage} from "./pages/Editors/DisciplinesEditor/DisciplinesEditorPage.tsx";
import {GroupsEditorPage} from "./pages/Editors/GroupsEditor/GroupsEditorPage.tsx";
import {SpecialitiesEditorPage} from "./pages/Editors/SpecialitiesEditor/SpecialitiesEditorPage.tsx";
import {TeachersEditorPage} from "./pages/Editors/TeachersEditor/TeachersEditorPage.tsx";
import {TimesEditorPage} from "./pages/Editors/TimesEditor/TimesEditorPage.tsx";
import {TimeTypesEditorPage} from "./pages/Editors/TimeTypesEditor/TimeTypesEditorPage.tsx";

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
            <Route path={Routes.LOGIN} element={<LoginPage/>}/>
            <Route path={Routes.REPORTS} element={<ReportsPage/>}/>
            <Route path={Routes.SCHEDULE} element={<>Расписание</>}/>
            <Route path={Routes.EDITOR_PAIRS} element={<PairsEditor/>}/>
            <Route path={Routes.EDITOR_SPECIALITIES} element={<SpecialitiesEditorPage/>}/>
            <Route path={Routes.EDITOR_DISCIPLINES} element={<DisciplinesEditorPage/>}/>
            <Route path={Routes.EDITOR_GROUPS} element={<GroupsEditorPage/>}/>
            <Route path={Routes.EDITOR_TEACHERS} element={<TeachersEditorPage/>}/>
            <Route path={Routes.EDITOR_CLASSROOMS} element={<ClassroomsEditorPage/>}/>
            <Route path={Routes.EDITOR_TIMES} element={<TimesEditorPage/>}/>
            <Route path={Routes.EDITOR_TIME_TYPES} element={<TimeTypesEditorPage/>}/>
        </Route>
    ])
)