import {Container, Tab, Tabs} from "react-bootstrap";
import {AvailableDisciplinesEditor} from "../../../components/Editors/DisciplinesEditor/AvailableDisciplinesEditor.tsx";
import {DeletedDisciplinesEditor} from "../../../components/Editors/DisciplinesEditor/DeletedDisciplinesEditor.tsx";

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