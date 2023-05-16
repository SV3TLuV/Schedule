import {Container, Tab, Tabs} from "react-bootstrap";
import {AvailableTimeTypesEditor} from "../../../components/Editors/TimeTypesEditor/AvailableTimeTypesEditor.tsx";
import {DeletedTimeTypesEditor} from "../../../components/Editors/TimeTypesEditor/DeletedTimeTypesEditor.tsx";

export const TimeTypesEditorPage = () => {
    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Tabs defaultActiveKey='available'>
                <Tab eventKey='available' title='Действующие'>
                    <AvailableTimeTypesEditor/>
                </Tab>
                <Tab eventKey='deleted' title='Удаленные'>
                    <DeletedTimeTypesEditor/>
                </Tab>
            </Tabs>
        </Container>
    )
}