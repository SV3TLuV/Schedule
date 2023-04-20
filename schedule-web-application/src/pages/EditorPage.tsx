import {Container, Tab, Tabs} from "react-bootstrap";
import {GroupEditor} from "../components/GroupEditor/GroupEditor";
import {QueryFilter} from "../common/enums/QueryFilter";
import {TeacherEditor} from "../components/TeacherEditor/TeacherEditor";
import {ClassroomEditor} from "../components/ClassroomEditor/ClassroomEditor";
import {DisciplineEditor} from "../components/DisciplineEditor/DisciplineEditor";
import {TimeEditor} from "../components/TimeEditor/TimeEditor";
import {DayEditor} from "../components/DayEditor/DayEditor";
import {ClassroomTypeEditor} from "../components/ClassroomTypeEditor/ClassroomTypeEditor";
import {TimeTypeEditor} from "../components/TimeTypeEditor/TimeTypeEditor";
import {SpecialityEditor} from "../components/SpecialityEditor/SpecialityEditor";


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
                        <Tab eventKey="groups-available" title="Действующие" style={{ height: "100vh" }}>
                            <GroupEditor
                                filter={QueryFilter.Available}
                                type={"available"}
                            />
                        </Tab>
                        <Tab eventKey="groups-deleted" title="Удаленные" style={{ height: "100vh" }}>
                            <GroupEditor
                                filter={QueryFilter.Available}
                                type={"deleted"}
                            />
                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="teachers" title="Преподаватели">
                    <Tabs>
                        <Tab eventKey="teachers-available" title="Действующие" style={{ height: "100vh" }}>
                            <TeacherEditor
                                filter={QueryFilter.Available}
                                type={"available"}
                            />
                        </Tab>
                        <Tab eventKey="teachers-deleted" title="Удаленные" style={{ height: "100vh" }}>
                            <TeacherEditor
                                filter={QueryFilter.Deleted}
                                type={"deleted"}
                            />
                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="classrooms" title="Кабинеты">
                    <Tabs>
                        <Tab eventKey="classrooms-available" title="Действующие" style={{ height: "100vh" }}>
                            <ClassroomEditor
                                filter={QueryFilter.Available}
                                type={"available"}
                            />
                        </Tab>
                        <Tab eventKey="classrooms-deleted" title="Удаленные" style={{ height: "100vh" }}>
                            <ClassroomEditor
                                filter={QueryFilter.Available}
                                type={"deleted"}
                            />
                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="disciplines" title="Дисциплины">
                    <Tabs>
                        <Tab eventKey="disciplines-available" title="Действующие" style={{ height: "100vh" }}>
                            <DisciplineEditor
                                filter={QueryFilter.Available}
                                type={"available"}
                            />
                        </Tab>
                        <Tab eventKey="disciplines-deleted" title="Удаленные" style={{ height: "100vh" }}>
                            <DisciplineEditor
                                filter={QueryFilter.Deleted}
                                type={"deleted"}
                            />
                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="times" title="Время">
                    <Tabs>
                        <Tab eventKey="times-available" title="Действующие" style={{ height: "100vh" }}>
                            <TimeEditor
                                filter={QueryFilter.Available}
                                type={"available"}
                            />
                        </Tab>
                        <Tab eventKey="times-deleted" title="Удаленные" style={{ height: "100vh" }}>
                            <TimeEditor
                                filter={QueryFilter.Deleted}
                                type={"deleted"}
                            />
                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="days" title="Дни" style={{ height: "100vh" }}>
                    <DayEditor />
                </Tab>
                <Tab eventKey="classroom-types" title="Виды кабинетов" style={{ height: "100vh" }}>
                    <Tabs>
                        <Tab eventKey="classroom-types-available" title="Действующие" style={{ height: "100vh" }}>
                            <ClassroomTypeEditor
                                filter={QueryFilter.Available}
                                type={"available"}
                            />
                        </Tab>
                        <Tab eventKey="classroom-types-deleted" title="Удаленные" style={{ height: "100vh" }}>
                            <ClassroomTypeEditor
                                filter={QueryFilter.Deleted}
                                type={"deleted"}
                            />
                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="time-types" title="Виды времени">
                    <Tabs>
                        <Tab eventKey="time-types-available" title="Действующие" style={{ height: "100vh" }}>
                            <TimeTypeEditor
                                filter={QueryFilter.Available}
                                type={"available"}
                            />
                        </Tab>
                        <Tab eventKey="time-types-deleted" title="Удаленные" style={{ height: "100vh" }}>
                            <TimeTypeEditor
                                filter={QueryFilter.Deleted}
                                type={"deleted"}
                            />
                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="specialties" title="Специальности">
                    <Tabs>
                        <Tab eventKey="specialties-available" title="Действующие" style={{ height: "100vh" }}>
                            <SpecialityEditor
                                filter={QueryFilter.Available}
                                type={"available"}
                            />
                        </Tab>
                        <Tab eventKey="specialties-deleted" title="Удаленные" style={{ height: "100vh" }}>
                            <SpecialityEditor
                                filter={QueryFilter.Available}
                                type={"deleted"}
                            />
                        </Tab>
                    </Tabs>
                </Tab>
                <Tab eventKey="users" title="Пользователи" style={{ height: "100vh" }}>

                </Tab>
            </Tabs>
        </Container>
    )
}