import {Container, Tab, Tabs} from "react-bootstrap";
import {useTabs} from "../../../../hooks";
import {Outlet} from "react-router-dom";

export const LessonEditorPage = () => {
    const {key, onSelect} = useTabs({
        baseUrl: '/editor/lessons',
        defaultKey: 'timetable'
    })

    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Tabs
                activeKey={key}
                onSelect={onSelect}
            >
                <Tab eventKey='timetable' title='Расписание'/>
                <Tab eventKey='template' title='Шаблон'/>
            </Tabs>
            <Outlet/>
        </Container>
    )
}