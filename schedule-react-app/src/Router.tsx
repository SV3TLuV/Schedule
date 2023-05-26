import {createBrowserRouter, createRoutesFromElements, Outlet, Route, useLocation} from "react-router-dom";
import {useAppDispatch, useTypedSelector} from "./hooks/redux.ts";
import {useEffect} from "react";
import {setCurrentPage} from "./store/slices/routeSlice.tsx";
import {LoginPage} from "./components/screens/login/LoginPage.tsx";
import {ReportsPage} from "./components/screens/reports/ReportsPage.tsx";
import {SearchSchedulePage} from "./components/screens/schedule/search/SearchSchedulePage.tsx";
import {TableSchedulePage} from "./components/screens/schedule/table/TableSchedulePage.tsx";
import {SpecialitiesEditorPage} from "./components/screens/editor/speciality/SpecialitiesEditorPage.tsx";
import {DisciplinesEditorPage} from "./components/screens/editor/discipline/DisciplinesEditorPage.tsx";
import {GroupsEditorPage} from "./components/screens/editor/group/GroupsEditorPage.tsx";
import {TeachersEditorPage} from "./components/screens/editor/teacher/TeachersEditorPage.tsx";
import {ClassroomsEditorPage} from "./components/screens/editor/classroom/ClassroomsEditorPage.tsx";
import {TimesEditorPage} from "./components/screens/editor/time/TimesEditorPage.tsx";
import {TimeTypesEditorPage} from "./components/screens/editor/timeType/TimeTypesEditorPage.tsx";
import {Header} from "./components/layout/header/Header.tsx";
import {LessonEditorPage} from "./components/screens/editor/lesson/LessonEditorPage.tsx";

const Root = () => {
    const {isNavShowed} = useTypedSelector(state => state.application)
    const dispatch = useAppDispatch()
    const {pathname} = useLocation()

    useEffect(() => {
        dispatch(setCurrentPage(pathname))
    }, [dispatch, pathname])

    return (
        <div className='Root'>
            {isNavShowed && <Header/>}
            <Outlet/>
        </div>
    )
}

export const router = createBrowserRouter(
    createRoutesFromElements([
        <Route path='/' element={<Root/>}>
            <Route path='login' element={<LoginPage/>}/>
            <Route path='reports' element={<ReportsPage/>}/>
            <Route path='schedule'>
                <Route path='search' element={<SearchSchedulePage/>}/>
                <Route path='table' element={<TableSchedulePage/>}/>
            </Route>
            <Route path='editor'>
                <Route path='classrooms' element={<ClassroomsEditorPage/>}/>
                <Route path='disciplines' element={<DisciplinesEditorPage/>}/>
                <Route path='groups' element={<GroupsEditorPage/>}/>
                <Route path='lessons' element={<LessonEditorPage></LessonEditorPage>}/>
                <Route path='specialities' element={<SpecialitiesEditorPage/>}/>
                <Route path='teachers' element={<TeachersEditorPage/>}/>
                <Route path='times' element={<TimesEditorPage/>}/>
                <Route path='time-types' element={<TimeTypesEditorPage/>}/>
            </Route>
        </Route>
    ])
)