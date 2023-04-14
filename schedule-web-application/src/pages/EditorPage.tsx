import {Container, Tab, Tabs} from "react-bootstrap";

export const EditorPage = () => {
    return (
        <Container fluid style={{ padding: 0, margin: 0 }}>
            <Tabs>
                <Tab eventKey="pairs" title="Пары">
                    <Tabs>
                        <Tab eventKey="pairs-available" title="Действительные">

                        </Tab>
                        <Tab eventKey="pairs-template" title="Шаблон">

                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="groups" title="Группы">
                    <Tabs>
                        <Tab eventKey="groups-available" title="Действующие">

                        </Tab>
                        <Tab eventKey="groups-deleted" title="Удаленные">

                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="teachers" title="Преподаватели">
                    <Tabs>
                        <Tab eventKey="teachers-available" title="Действующие">

                        </Tab>
                        <Tab eventKey="teachers-deleted" title="Удаленные">

                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="classrooms" title="Кабинеты">
                    <Tabs>
                        <Tab eventKey="classrooms-available" title="Действующие">

                        </Tab>
                        <Tab eventKey="classrooms-deleted" title="Удаленные">

                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="disciplines" title="Дисциплины">
                    <Tabs>
                        <Tab eventKey="disciplines-available" title="Действующие">

                        </Tab>
                        <Tab eventKey="disciplines-deleted" title="Удаленные">

                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="times" title="Время">
                    <Tabs>
                        <Tab eventKey="times-available" title="Действующие">

                        </Tab>
                        <Tab eventKey="times-deleted" title="Удаленные">

                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="days" title="Дни">

                </Tab>
                <Tab eventKey="classroom-types" title="Виды кабинетов">

                </Tab>
                <Tab eventKey="time-types" title="Виды времени">

                </Tab>
                <Tab eventKey="specialties" title="Специальности">
                    <Tabs>
                        <Tab eventKey="specialties-available" title="Действующие">

                        </Tab>
                        <Tab eventKey="specialties-deleted" title="Удаленные">

                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="users" title="Пользователи">
                    <Tabs>
                        <Tab eventKey="users-available" title="Действующие">

                        </Tab>
                        <Tab eventKey="users-deleted" title="Удаленные">

                        </Tab>
                    </Tabs>
                </Tab>
            </Tabs>
        </Container>
    )
}