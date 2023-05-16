import {Container, Tab, Tabs} from "react-bootstrap";
import {AvailableTeachersEditor} from "../../../components/Editors/TeachersEditor/AvailableTeachersEditor.tsx";
import {DeletedTeachersEditor} from "../../../components/Editors/TeachersEditor/DeletedTeachersEditor.tsx";

export const TeachersEditorPage = () => {
    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Tabs defaultActiveKey='available'>
                <Tab eventKey='available' title='Действующие'>
                    <AvailableTeachersEditor/>
                </Tab>
                <Tab eventKey='deleted' title='Удаленные'>
                    <DeletedTeachersEditor/>
                </Tab>
            </Tabs>
        </Container>
    )
}