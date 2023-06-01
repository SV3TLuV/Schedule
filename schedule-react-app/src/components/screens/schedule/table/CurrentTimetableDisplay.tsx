import {Card, Col, Row} from "react-bootstrap";
import {ICurrentTimetable} from "../../../../features/models/ICurrentTimetable.ts";
import {LessonDisplay} from "./LessonDisplay.tsx";
import {DayDisplay} from "./DayDisplay.tsx";
import {ITimetable} from "../../../../features/models/ITimetable.ts";
import {ILesson} from "../../../../features/models/ILesson.ts";
import {chunk} from "../../../../utils/chunk.ts";
import {useEffect, useMemo, useRef, useState} from "react";
import {CSSTransition} from "react-transition-group";
import "./animations.css";

interface ICurrentTimetableDisplay {
    timetable: ICurrentTimetable
}

export const CurrentTimetableDisplay2 = ({timetable}: ICurrentTimetableDisplay) => {
    return (
        <Card className='h-100 w-100 bg-dark text-white font-weight-bold'>
            <Card.Body className='p-0'>
                <p className='text-uppercase fw-bolder text-center mb-0 fs-14'>
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
                        background: '#363636'
                    }}
                >
                    {timetable.dates.map((date, i) => (
                        <div
                            key={i + date.key.id}
                            className="col"
                            style={{
                                paddingLeft: '0px',
                                paddingRight: '2px',
                            }}
                        >
                            <div className='w-100 h-100'>
                                {date.items.map(item => item.lessons.map(lesson => (
                                    <LessonDisplay
                                        key={lesson.id}
                                        lesson={lesson}
                                    />
                                )))}
                            </div>
                        </div>
                    ))}
                </Row>
            </Card.Body>
        </Card>
    )
}

export const CurrentTimetableDisplay = ({timetable}: ICurrentTimetableDisplay) => {
    return (
        <Card className='h-100 w-100 bg-dark text-white font-weight-bold'>
            <Card.Body className='p-0 h-100'>
                <p className='text-uppercase fw-bolder text-center mb-0 fs-14'>
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

export const LessonListDisplay = ({timetable}: {timetable: ITimetable}) => {
    const [index, setIndex] = useState(0)
    const [show, setShow] = useState(false)
    const nodeRef = useRef(null);

    const lessons: ILesson[][] = useMemo(() => {
        if (timetable.lessons.length === 0)
            return []

        const chunkSize = 4
        return chunk(timetable.lessons, chunkSize)

    }, [timetable])

    useEffect(() => {
        const interval = setInterval(() => {
            const hasMore = lessons.length > 1

            if (hasMore) {
                setShow(false)
            }

            setTimeout(() => {
                setIndex((prev) => {
                    if (prev < lessons.length - 1) {
                        return prev + 1
                    }

                    return 0
                });

                if (hasMore) {
                    setShow(true)
                }
            }, 750)
        }, 7500);

        return () => {
            clearInterval(interval);
        };
    }, [lessons.length])

    return (
        <CSSTransition
            in={show}
            nodeRef={nodeRef}
            classNames='fade'
            timeout={1000}
        >
            <div
                className='h-100 w-100'
                ref={nodeRef}
            >
                {lessons?.[index]?.map((lesson, index) => (
                    <LessonDisplay
                        key={lesson?.id ?? index}
                        lesson={lesson}
                    />
                ))}
            </div>
        </CSSTransition>
    )
}