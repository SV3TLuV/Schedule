import {Button, Container, Nav, Navbar, NavDropdown} from "react-bootstrap";
import {BiTable} from "react-icons/all";
import {useNavigation} from "../../hooks/useNavigation.ts";

export const AppNav = () => {
    const {navigateTo} = useNavigation()

    const goToLogin = () => navigateTo('/login')
    const goToScheduleSearch = () => navigateTo('/schedule/search')
    const goToScheduleTable = () => navigateTo('/schedule/table')
    const goToReports = () => navigateTo('/reports')
    const goToPairsEditor = () => navigateTo('/editor/pairs')
    const goToSpecialitiesEditor = () => navigateTo('/editor/specialities')
    const goToDisciplinesEditor = () => navigateTo('/editor/disciplines')
    const goToGroupsEditor = () => navigateTo('/editor/groups')
    const goToTeachersEditor = () => navigateTo('/editor/teachers')
    const goToClassroomsEditor = () => navigateTo('/editor/classrooms')
    const goToTimesEditor = () => navigateTo('/editor/times')
    const goToTimeTypesEditor = () => navigateTo('/editor/time-types')

    return (
        <Navbar bg='light' expand='md' className='py-3'>
            <Container>
                <Navbar.Brand className='navbar-brand d-flex align-items-center'>
                    <span className='bs-icon-sm bs-icon-rounded bs-icon-primary d-flex justify-content-center align-items-center me-2 bs-icon'>
                        <BiTable size='1em' />
                    </span>
                    <span className='fw-bolder'>
                        <strong>Расписание</strong>
                    </span>
                </Navbar.Brand>
                <Navbar.Toggle aria-controls='navcol-2'>
                    <span className='visually-hidden'>Toggle navigation</span>
                    <span className='navbar-toggler-icon'/>
                </Navbar.Toggle>
                <Navbar.Collapse id='navcol-2'>
                    <Nav className='ms-auto'>
                        <NavDropdown title='Расписание'>
                            <NavDropdown.Item onClick={goToScheduleSearch}>
                                Поиск
                            </NavDropdown.Item>
                            <NavDropdown.Item onClick={goToScheduleTable}>
                                Таблица
                            </NavDropdown.Item>
                        </NavDropdown>
                        <NavDropdown title='Редактор'>
                            <NavDropdown.Item onClick={goToPairsEditor}>
                                Пары
                            </NavDropdown.Item>
                            <NavDropdown.Item onClick={goToSpecialitiesEditor}>
                                Специальности
                            </NavDropdown.Item>
                            <NavDropdown.Item onClick={goToDisciplinesEditor}>
                                Дисциплины
                            </NavDropdown.Item>
                            <NavDropdown.Item onClick={goToGroupsEditor}>
                                Группы
                            </NavDropdown.Item>
                            <NavDropdown.Item onClick={goToTeachersEditor}>
                                Преподаватели
                            </NavDropdown.Item>
                            <NavDropdown.Item onClick={goToClassroomsEditor}>
                                Кабинеты
                            </NavDropdown.Item>
                            <NavDropdown.Item onClick={goToTimesEditor}>
                                Время
                            </NavDropdown.Item>
                            <NavDropdown.Item onClick={goToTimeTypesEditor}>
                                Виды времени
                            </NavDropdown.Item>
                        </NavDropdown>
                        <Nav.Link onClick={goToReports}>
                            Отчеты
                        </Nav.Link>
                    </Nav>
                    <Button
                        onClick={goToLogin}
                        className='ms-md-2'
                        variant='primary'
                        role='button'
                    >
                        Войти
                    </Button>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    )
}