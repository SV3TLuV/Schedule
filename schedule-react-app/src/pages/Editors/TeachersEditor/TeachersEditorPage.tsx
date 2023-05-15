import {Container, Tab, Tabs} from "react-bootstrap";
import {AvailableTeachersEditor} from "./AvailableTeachersEditor.tsx";
import {DeletedTeachersEditor} from "./DeletedTeachersEditor.tsx";

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