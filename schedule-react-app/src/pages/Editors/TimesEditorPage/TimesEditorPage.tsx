import {Container, Tab, Tabs} from "react-bootstrap";
import {AvailableTimesEditor} from "../../../components/Editors/TimesEditor/AvailableTimesEditor.tsx";
import {DeletedTimesEditor} from "../../../components/Editors/TimesEditor/DeletedTimesEditor.tsx";

export const TimesEditorPage = () => {
    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Tabs defaultActiveKey='available'>
                <Tab eventKey='available' title='Действующие'>
                    <AvailableTimesEditor/>
                </Tab>
                <Tab eventKey='deleted' title='Удаленные'>
                    <DeletedTimesEditor/>
                </Tab>
            </Tabs>
        </Container>
    )
}