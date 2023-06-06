import {Button, Container, Nav, Navbar, NavDropdown} from "react-bootstrap";
import {BiTable} from "react-icons/bi";
import {useNavigation, useTypedSelector} from "../../hooks";
import {ILogoutCommand} from "../../features/commands";
import {useLogoutMutation} from "../../store/apis";

export const Header = () => {
    const {user, accessToken, refreshToken} = useTypedSelector(state => state.auth)
    const [logout] = useLogoutMutation()
    const {navigateTo} = useNavigation()

    const handleLogout = async () => await logout({
        accessToken,
        refreshToken
    } as ILogoutCommand)

    const goToLogin = () => navigateTo('/login')
    const goToScheduleSearch = () => navigateTo('/schedule/search')
    const goToScheduleTable = () => navigateTo('/schedule/table/1')
    const goToReports = () => navigateTo('/reports')
    const goToPairsEditor = () => navigateTo('/editor/lessons')
    const goToSpecialitiesEditor = () => navigateTo('/editor/specialities')
    const goToDisciplinesEditor = () => navigateTo('/editor/disciplines')
    const goToGroupsEditor = () => navigateTo('/editor/groups')
    const goToTeachersEditor = () => navigateTo('/editor/teachers')
    const goToClassroomsEditor = () => navigateTo('/editor/classrooms')
    const goToTimesEditor = () => navigateTo('/editor/times')
    const goToTimeTypesEditor = () => navigateTo('/editor/time-types')
    const goToUsersEditor = () => navigateTo('/editor/users')

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
                        {user &&
                            <>
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
                                    {user.role.id === 1 &&
                                        <NavDropdown.Item onClick={goToUsersEditor}>
                                            Пользователи
                                        </NavDropdown.Item>
                                    }
                                </NavDropdown>
                                <Nav.Link onClick={goToReports}>
                                    Отчеты
                                </Nav.Link>
                            </>
                        }
                    </Nav>
                    {!user &&
                        <Button
                            onClick={goToLogin}
                            className='ms-md-2'
                            variant='primary'
                            role='button'
                        >
                            Войти
                        </Button>
                    }
                    {user &&
                        <Button
                            onClick={handleLogout}
                            className='ms-md-2'
                            variant='primary'
                            role='button'
                        >
                            Выйти
                        </Button>
                    }
                </Navbar.Collapse>
            </Container>
        </Navbar>
    )
}