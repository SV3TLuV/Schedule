import {createBrowserRouter, createRoutesFromElements, Outlet, Route, useLocation} from "react-router-dom";
import {useAppDispatch, useTypedSelector} from "./hooks";
import {useEffect} from "react";
import {setCurrentPage} from "./store/slices";
import {LoginPage} from "./components/screens/login";
import {ReportsPage} from "./components/screens/reports";
import {SearchSchedulePage} from "./components/screens/schedule";
import {TableSchedulePage} from "./components/screens/schedule";
import {SpecialitiesEditorPage} from "./components/screens/editor";
import {DisciplinesEditorPage} from "./components/screens/editor";
import {GroupsEditorPage} from "./components/screens/editor";
import {TeachersEditorPage} from "./components/screens/editor";
import {ClassroomsEditorPage} from "./components/screens/editor";
import {TimesEditorPage} from "./components/screens/editor";
import {TimeTypesEditorPage} from "./components/screens/editor";
import {LessonEditorPage} from "./components/screens/editor";
import {AvailableTeachersEditor} from "./components/screens/editor/teacher";
import {DeletedTeachersEditor} from "./components/screens/editor/teacher";
import {AvailableClassroomsEditor} from "./components/screens/editor/classroom";
import {DeletedClassroomsEditor} from "./components/screens/editor/classroom";
import {AvailableDisciplinesEditor} from "./components/screens/editor/discipline";
import {DeletedDisciplinesEditor} from "./components/screens/editor/discipline";
import {AvailableGroupsEditor} from "./components/screens/editor/group";
import {DeletedGroupsEditor} from "./components/screens/editor/group";
import {TimetableEditor} from './components/screens/editor/lesson';
import {TemplateEditor} from "./components/screens/editor/lesson";
import {AvailableSpecialitiesEditor} from "./components/screens/editor/speciality";
import {DeletedSpecialitiesEditor} from "./components/screens/editor/speciality";
import {AvailableTimesEditor} from "./components/screens/editor/time";
import {DeletedTimesEditor} from './components/screens/editor/time';
import {AvailableTimeTypesEditor} from "./components/screens/editor/timeType";
import {DeletedTimeTypesEditor} from "./components/screens/editor/timeType";
import {EditorPage} from "./components/screens/EditorPage.tsx";
import {RequireAuth} from "./hok";
import {UsersEditor} from "./components/screens/editor";
import {Header} from "./components/layout";

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
            <Route path='schedule'>
                <Route path='search' element={<SearchSchedulePage/>}/>
                <Route path='table/:page' element={<TableSchedulePage/>}/>
            </Route>
            <Route path='reports' element={
                <RequireAuth>
                    <ReportsPage/>
                </RequireAuth>
            }/>
            <Route path='editor' element={
                <RequireAuth>
                    <EditorPage/>
                </RequireAuth>
            }>
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
                <Route path='users' element={
                    <RequireAuth roles={['Admin']}>
                        <UsersEditor/>
                    </RequireAuth>
                }/>
            </Route>
        </Route>
    ])
)