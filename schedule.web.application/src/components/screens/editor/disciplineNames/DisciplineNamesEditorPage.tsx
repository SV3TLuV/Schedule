import {useTabs} from "../../../../hooks";
import {Container, Tab, Tabs} from "react-bootstrap";
import {Outlet} from "react-router-dom";

export const DisciplineNamesEditorPage = () => {
    const {key, onSelect} = useTabs({
        baseUrl: '/editor/discipline-names',
        defaultKey: 'available'
    })

    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Tabs
                activeKey={key}
                onSelect={onSelect}
            >
                <Tab eventKey='available' title='Действующие'/>
                <Tab eventKey='deleted' title='Удаленные'/>
            </Tabs>
            <Outlet/>
        </Container>
    )
}