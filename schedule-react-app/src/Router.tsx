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
import {AvailableTeachersEditor} from "./components/screens/editor/teacher/AvailableTeachersEditor.tsx";
import {DeletedTeachersEditor} from "./components/screens/editor/teacher/DeletedTeachersEditor.tsx";
import {AvailableClassroomsEditor} from "./components/screens/editor/classroom/AvailableClassroomsEditor.tsx";
import {DeletedClassroomsEditor} from "./components/screens/editor/classroom/DeletedClassroomsEditor.tsx";
import {AvailableDisciplinesEditor} from "./components/screens/editor/discipline/AvailableDisciplinesEditor.tsx";
import {DeletedDisciplinesEditor} from "./components/screens/editor/discipline/DeletedDisciplinesEditor.tsx";
import {AvailableGroupsEditor} from "./components/screens/editor/group/AvailableGroupsEditor.tsx";
import {DeletedGroupsEditor} from "./components/screens/editor/group/DeletedGroupsEditor.tsx";
import {TimetableEditor} from "./components/screens/editor/lesson/TimetableEditor.tsx";
import {TemplateEditor} from "./components/screens/editor/lesson/TemplateEditor.tsx";
import {AvailableSpecialitiesEditor} from "./components/screens/editor/speciality/AvailableSpecialitiesEditor.tsx";
import {DeletedSpecialitiesEditor} from "./components/screens/editor/speciality/DeletedSpecialitiesEditor.tsx";
import {AvailableTimesEditor} from "./components/screens/editor/time/AvailableTimesEditor.tsx";
import {DeletedTimesEditor} from "./components/screens/editor/time/DeletedTimesEditor.tsx";
import {AvailableTimeTypesEditor} from "./components/screens/editor/timeType/AvailableTimeTypesEditor.tsx";
import {DeletedTimeTypesEditor} from "./components/screens/editor/timeType/DeletedTimeTypesEditor.tsx";

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
                <Route path='classrooms' element={<ClassroomsEditorPage/>}>
                    <Route path='available' element={<AvailableClassroomsEditor/>}/>
                    <Route path='deleted' element={<DeletedClassroomsEditor/>}/>
                </Route>
                <Route path='disciplines' element={<DisciplinesEditorPage/>}>
                    <Route path='available' element={<AvailableDisciplinesEditor/>}/>
                    <Route path='deleted' element={<DeletedDisciplinesEditor/>}/>
                </Route>
                <Route path='groups' element={<GroupsEditorPage/>}>
                    <Route path='available' element={<AvailableGroupsEditor/>}/>
                    <Route path='deleted' element={<DeletedGroupsEditor/>}/>
                </Route>
                <Route path='lessons' element={<LessonEditorPage/>}>
                    <Route path='timetable' element={<TimetableEditor/>}/>
                    <Route path='template' element={<TemplateEditor/>}/>
                </Route>
                <Route path='specialities' element={<SpecialitiesEditorPage/>}>
                    <Route path='available' element={<AvailableSpecialitiesEditor/>}/>
                    <Route path='deleted' element={<DeletedSpecialitiesEditor/>}/>
                </Route>
                <Route path='teachers' element={<TeachersEditorPage/>}>
                    <Route path='available' element={<AvailableTeachersEditor/>}/>
                    <Route path='deleted' element={<DeletedTeachersEditor/>}/>
                </Route>
                <Route path='times' element={<TimesEditorPage/>}>
                    <Route path='available' element={<AvailableTimesEditor/>}/>
                    <Route path='deleted' element={<DeletedTimesEditor/>}/>
                </Route>
                <Route path='time-types' element={<TimeTypesEditorPage/>}>
                    <Route path='available' element={<AvailableTimeTypesEditor/>}/>
                    <Route path='deleted' element={<DeletedTimeTypesEditor/>}/>
                </Route>
            </Route>
        </Route>
    ])
)