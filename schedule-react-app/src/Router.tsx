import {createBrowserRouter, createRoutesFromElements, Outlet, Route, useLocation} from "react-router-dom";
import {Routes} from "./common/enums/Routes.ts";
import {LoginPage} from "./pages/LoginPage/LoginPage.tsx";
import {AppNav} from "./components/AppNav/AppNav.tsx";
import {useAppDispatch, useTypedSelector} from "./hooks/redux.ts";
import {useEffect} from "react";
import {setCurrent} from "./redux/slices/routeSlice.tsx";
import {ReportsPage} from "./pages/ReportsPage/ReportsPage.tsx";
import {PairsEditor} from "./pages/Editors/PairsEditor/PairsEditor.tsx";
import {TimeTypesEditor} from "./pages/Editors/TimeTypesEditor/TimeTypesEditor.tsx";
import {ClassroomTypesEditor} from "./pages/Editors/ClassroomTypesEditor/ClassroomTypesEditor.tsx";
import {TimesEditor} from "./pages/Editors/TimesEditor/TimesEditor.tsx";
import {ClassroomsEditor} from "./pages/Editors/ClassroomsEditor/ClassroomsEditor.tsx";
import {TeachersEditor} from "./pages/Editors/TeachersEditor/TeachersEditor.tsx";
import {GroupsEditor} from "./pages/Editors/GroupsEditor/GroupsEditor.tsx";
import {DisciplinesEditor} from "./pages/Editors/DisciplinesEditor/DisciplinesEditor.tsx";
import {SpecialitiesEditor} from "./pages/Editors/SpecialitiesEditor/SpecialitiesEditor.tsx";

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
            <Route path={Routes.EDITOR_SPECIALITIES} element={<SpecialitiesEditor/>}/>
            <Route path={Routes.EDITOR_DISCIPLINES} element={<DisciplinesEditor/>}/>
            <Route path={Routes.EDITOR_GROUPS} element={<GroupsEditor/>}/>
            <Route path={Routes.EDITOR_TEACHERS} element={<TeachersEditor/>}/>
            <Route path={Routes.EDITOR_CLASSROOMS} element={<ClassroomsEditor/>}/>
            <Route path={Routes.EDITOR_TIMES} element={<TimesEditor/>}/>
            <Route path={Routes.EDITOR_CLASSROOM_TYPES} element={<ClassroomTypesEditor/>}/>
            <Route path={Routes.EDITOR_TIME_TYPES} element={<TimeTypesEditor/>}/>
        </Route>
    ])
)