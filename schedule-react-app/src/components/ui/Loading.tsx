import {Spinner} from "react-bootstrap";

export const Loading = () => {
    return (
        <Spinner
            className='d-flex align-items-center justify-content-center position-fixed top-50 start-50'
            animation='border'
            role='status'
        />
    )
}