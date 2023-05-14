import {Button, Container, Nav, Navbar} from "react-bootstrap";
import {BiTable} from "react-icons/all";

export const AppNav = () => {
    return (
        <Navbar bg="light" expand="md" className="py-3">
            <Container>
                <Navbar.Brand href="#" className="navbar-brand d-flex align-items-center">
                    <span className="bs-icon-sm bs-icon-rounded bs-icon-primary d-flex justify-content-center align-items-center me-2 bs-icon">
                        <BiTable size="1em" />
                    </span>
                    <span className="fw-bolder">
                        <strong>Расписание</strong>
                    </span>
                </Navbar.Brand>
                <Navbar.Toggle aria-controls="navcol-2">
                    <span className="visually-hidden">Toggle navigation</span>
                    <span className="navbar-toggler-icon"/>
                </Navbar.Toggle>
                <Navbar.Collapse id="navcol-2">
                    <Nav className="ms-auto">
                        <Nav.Link active>
                            Расписание
                        </Nav.Link>
                        <Nav.Link active>
                            Редактор
                        </Nav.Link>
                        <Nav.Link active>
                            Отчеты
                        </Nav.Link>
                    </Nav>
                    <Button className="ms-md-2" variant="primary" role="button">
                        Войти
                    </Button>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    )
}