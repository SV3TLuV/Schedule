import {createBrowserRouter, createRoutesFromElements, Outlet, Route, useLocation} from "react-router-dom";
import {useAppDispatch, useTypedSelector} from "./hooks/redux.ts";
import {useEffect} from "react";
import {setCurrentPage} from "./store/slices/routeSlice.tsx";
import {LoginPage} from "./components/screens/login/LoginPage.tsx";
import {ReportsPage} from "./components/screens/reports/ReportsPage.tsx";
import {SearchSchedulePage} from "./components/screens/schedule/search/SearchSchedulePage.tsx";
import {TableSchedulePage} from "./components/screens/schedule/table/TableSchedulePage.tsx";
import {PairsEditor} from "./components/screens/editor/pair/PairsEditor.tsx";
import {SpecialitiesEditorPage} from "./components/screens/editor/speciality/SpecialitiesEditorPage.tsx";
import {DisciplinesEditorPage} from "./components/screens/editor/discipline/DisciplinesEditorPage.tsx";
import {GroupsEditorPage} from "./components/screens/editor/group/GroupsEditorPage.tsx";
import {TeachersEditorPage} from "./components/screens/editor/teacher/TeachersEditorPage.tsx";
import {ClassroomsEditorPage} from "./components/screens/editor/classroom/ClassroomsEditorPage.tsx";
import {TimesEditorPage} from "./components/screens/editor/time/TimesEditorPage.tsx";
import {TimeTypesEditorPage} from "./components/screens/editor/timeType/TimeTypesEditorPage.tsx";
import {Header} from "./components/layout/header/Header.tsx";

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
                <Route path='pairs' element={<PairsEditor/>}/>
                <Route path='specialities'>
                    <Route index element={<SpecialitiesEditorPage/>}/>
                    <Route path='create' element={<>Добавить специальность</>}/>
                    <Route path='update/:id' element={<>Обновить специальность</>}/>
                </Route>
                <Route path='disciplines'>
                    <Route index element={<DisciplinesEditorPage/>}/>
                    <Route path='create' element={<>Добавить дисциплину</>}/>
                    <Route path='update/:id' element={<>Обновить дисциплину</>}/>
                </Route>
                <Route path='groups'>
                    <Route index element={<GroupsEditorPage/>}/>
                    <Route path='create' element={<>Добавить группу</>}/>
                    <Route path='update/:id' element={<>Обновить группу</>}/>
                </Route>
                <Route path='teachers'>
                    <Route index element={<TeachersEditorPage/>}/>
                    <Route path='create' element={<>Добавить преподавателя</>}/>
                    <Route path='update/:id' element={<>Обновить преподавателя</>}/>
                </Route>
                <Route path='classrooms'element={<ClassroomsEditorPage/>}/>
                <Route path='times'>
                    <Route index element={<TimesEditorPage/>}/>
                    <Route path='create' element={<>Добавить время</>}/>
                    <Route path='update/:id' element={<>Обновить время</>}/>
                </Route>
                <Route path='time-types'>
                    <Route index element={<TimeTypesEditorPage/>}/>
                    <Route path='create' element={<>Добавить вид времени</>}/>
                    <Route path='update/:id' element={<>Обновить вид времени</>}/>
                </Route>
            </Route>
        </Route>
    ])
)