import {Container, Tab, Tabs} from "react-bootstrap";
import {AvailableDisciplinesEditor} from "./AvailableDisciplinesEditor.tsx";
import {DeletedDisciplinesEditor} from "./DeletedDisciplinesEditor.tsx";

export const DisciplinesEditorPage = () => {
    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Tabs defaultActiveKey='available'>
                <Tab eventKey='available' title='Действующие'>
                    <AvailableDisciplinesEditor/>
                </Tab>
                <Tab eventKey='deleted' title='Удаленные'>
                    <DeletedDisciplinesEditor/>
                </Tab>
            </Tabs>
        </Container>
    )
}