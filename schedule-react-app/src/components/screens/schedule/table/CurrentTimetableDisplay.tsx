import {Card, Col, Row} from "react-bootstrap";
import {ICurrentTimetable} from "../../../../features/models";
import "./animations.css";
import {LessonListDisplay} from "./LessonListDisplay.tsx";
import {DayDisplay} from "./DayDisplay.tsx";

interface ICurrentTimetableDisplay {
    timetable: ICurrentTimetable
}

export const CurrentTimetableDisplay = ({timetable}: ICurrentTimetableDisplay) => {
    return (
        <Card className='h-100 w-100 bg-dark text-white font-weight-bold'>
            <Card.Body className='p-0 h-100'>
                <p className='text-uppercase text-truncate fw-bolder text-center mb-0 fs-14'>
                    <span className="text-decoration-underline">
                        {timetable.groupNames}
                    </span>
                </p>
                <Row className='m-0'>
                    {timetable.dates.map(date => (
                        <DayDisplay
                            key={date.key.day.id}
                            day={date.key.day}
                        />
                    ))}
                </Row>
                <Row
                    className='m-0'
                    style={{
                        height: 'calc(100% - 39px)',
                        background: '#363636',
                        flexGrow: 1
                    }}
                >
                    {timetable.dates.map(date => {
                        return date.items.map(timetable => (
                            <Col
                                key={timetable.id}
                                xs={6}
                                style={{
                                    paddingLeft: '0px',
                                    paddingRight: '2px',
                                }}
                            >
                                <LessonListDisplay timetable={timetable}/>
                            </Col>
                        ))
                    })}
                </Row>
            </Card.Body>
        </Card>
    )
}

