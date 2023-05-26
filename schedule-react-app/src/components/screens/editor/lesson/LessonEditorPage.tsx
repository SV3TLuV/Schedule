import {Container, Tab, Tabs} from "react-bootstrap";
import {TimetableEditor} from "./TimetableEditor.tsx";
import {TemplateEditor} from "./TemplateEditor.tsx";

export const LessonEditorPage = () => {
    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Tabs defaultActiveKey='available'>
                <Tab eventKey='available' title='Расписание'>
                    <TimetableEditor/>
                </Tab>
                <Tab eventKey='deleted' title='Шаблон'>
                    <TemplateEditor/>
                </Tab>
            </Tabs>
        </Container>
    )
}