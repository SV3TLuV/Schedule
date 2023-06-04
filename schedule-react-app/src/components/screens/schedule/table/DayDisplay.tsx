import {Col} from "react-bootstrap";
import {IDay} from "../../../../features/models";

interface IDayDisplay {
    day: IDay
}

export const DayDisplay = ({day}: IDayDisplay) => {
    return (
        <Col
            className="p-0"
            xxl={6}
            style={{
                fontSize: '14px',
            }}
        >
            <p
                className="text-uppercase fw-bolder text-center mb-0"
                style={{
                    fontSize: '12px',
                    color: 'rgb(232,232,232)',
                }}
            >
                {day.name}
            </p>
        </Col>
    )
}