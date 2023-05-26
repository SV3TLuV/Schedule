import {Container, Tab, Tabs} from "react-bootstrap";
import {useTabs} from "../../../../hooks/useTabs.ts";
import {Outlet} from "react-router-dom";

export const SpecialitiesEditorPage = () => {
    const {key, onSelect} = useTabs({
        baseUrl: '/editor/specialities',
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