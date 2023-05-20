import {Container, Tab, Tabs} from "react-bootstrap";
import {AvailableGroupsEditor} from "./AvailableGroupsEditor.tsx";
import {DeletedGroupsEditor} from "./DeletedGroupsEditor.tsx";

export const GroupsEditorPage = () => {
    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Tabs defaultActiveKey='available'>
                <Tab eventKey='available' title='Действующие'>
                    <AvailableGroupsEditor/>
                </Tab>
                <Tab eventKey='deleted' title='Удаленные'>
                    <DeletedGroupsEditor/>
                </Tab>
            </Tabs>
        </Container>
    )
}