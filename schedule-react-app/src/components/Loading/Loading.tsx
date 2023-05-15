import {useTypedSelector} from "../../hooks/redux.ts";
import {Container, Spinner} from "react-bootstrap";

export const Loading = () => {
    const {isNavShowed} = useTypedSelector(state => state.route)

    return (
        <Container
            className='d-flex justify-content-center align-items-center'
            style={isNavShowed ? {height: 'calc(100vh - 72px)'} : { height: '100vh' }}
        >
            <Spinner animation='border' role='status' className='mr-2'/>
        </Container>
    )
}