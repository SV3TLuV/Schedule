import {Container, Tab, Tabs} from "react-bootstrap";
import {AvailableSpecialitiesEditor} from "../../../components/Editors/SpecialitiesEditor/AvailableSpecialitiesEditor.tsx";
import {DeletedSpecialitiesEditor} from "../../../components/Editors/SpecialitiesEditor/DeletedSpecialitiesEditor.tsx";

export const SpecialitiesEditorPage = () => {
    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Tabs defaultActiveKey='available'>
                <Tab eventKey='available' title='Действующие'>
                    <AvailableSpecialitiesEditor/>
                </Tab>
                <Tab eventKey='deleted' title='Удаленные'>
                    <DeletedSpecialitiesEditor/>
                </Tab>
            </Tabs>
        </Container>
    )
}