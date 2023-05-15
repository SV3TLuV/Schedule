import {Container, Tab, Tabs} from "react-bootstrap";
import {AvailableClassroomsEditor} from "./AvailableClassroomsEditor.tsx";
import {DeletedClassroomsEditor} from "./DeletedClassroomsEditor.tsx";

export const ClassroomsEditorPage = () => {
    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Tabs defaultActiveKey='available'>
                <Tab eventKey='available' title='Действующие'>
                    <AvailableClassroomsEditor/>
                </Tab>
                <Tab eventKey='deleted' title='Удаленные'>
                    <DeletedClassroomsEditor/>
                </Tab>
            </Tabs>
        </Container>
    )
}