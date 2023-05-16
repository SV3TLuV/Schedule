import {Container, Tab, Tabs} from "react-bootstrap";
import {AvailableGroupsEditor} from "../../../components/Editors/GroupsEditor/AvailableGroupsEditor.tsx";
import {DeletedGroupsEditor} from "../../../components/Editors/GroupsEditor/DeletedGroupsEditor.tsx";

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