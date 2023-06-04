import {Col, Row} from "react-bootstrap";
import clsx from "clsx";
import styles from "./styles.module.css";
import {ILesson} from "../../../../features/models";
import {getShortFio} from "../../../../utils/getShortFio.ts";

interface ILessonDisplay {
    lesson: ILesson
}

export const LessonDisplay = ({lesson}: ILessonDisplay) => {
    return (
        <Row className={clsx(styles.container, 'm-0', {
            [styles.changed]: lesson.isChanged
        })}>
            <Col className='p-0'>
                <Row className='m-0 w-100 h-50'>
                    <Col
                        className='p-0 d-xxl-flex justify-content-xxl-center align-items-xxl-center'
                        xxl={8}
                        style={{
                            letterSpacing: '-0.75px',
                            borderRight: '1px solid rgb(0,0,0)',
                            borderBottom: '1px solid rgb(0,0,0)',
                        }}
                    >
                        <p
                            className='text-truncate text-center mb-0 lh-1'
                            style={{
                                fontSize: '10px',
                                padding: '1px',
                                letterSpacing: '-0.75px',
                                color: '#dbc14d'
                            }}
                        >
                            {lesson?.discipline?.name ?? ''}
                            {lesson?.subgroup && (
                                <>
                                    <br/>
                                    {`(${lesson.subgroup} подгруппа)`}
                                </>
                            )}
                        </p>
                    </Col>
                    <Col
                        className='d-xxl-flex p-0 justify-content-xxl-center align-items-xxl-center'
                        xxl={4}
                        style={{
                            borderBottom: '1px solid rgb(0,0,0)',
                            borderLeft: '1px solid rgb(0,0,0)',
                        }}
                    >
                        <p className='font-monospace text-truncate text-center mb-0 lh-1'
                           style={{
                               fontSize: '11px',
                               letterSpacing: '-0.75px',
                               padding: '1px',
                           }}
                        >
                            {lesson?.time?.start ?? ''}
                            <br/>
                            {lesson?.time?.end ?? ''}
                        </p>
                    </Col>
                </Row>
                <Row className="m-0 w-100 h-50">
                    <Col
                        className='d-xxl-flex p-0 justify-content-xxl-center align-items-xxl-center'
                        xxl={8}
                        style={{
                            letterSpacing: '-0.75px',
                            borderTop: '1px solid rgb(0,0,0)',
                            borderRight: '1px solid rgb(0,0,0)',
                        }}
                    >
                        <p
                            className='text-truncate text-capitalize text-center mb-0 lh-1'
                            style={{
                                fontSize: '10px',
                                letterSpacing: '-0.75px',
                                padding: '1px',
                            }}
                        >
                            {getShortFio(lesson?.teacherClassrooms?.at(0)?.teacher)}
                            <br/>
                            {getShortFio(lesson?.teacherClassrooms?.at(1)?.teacher)}
                        </p>
                    </Col>
                    <Col
                        className='d-xxl-flex p-0 justify-content-xxl-center align-items-xxl-center'
                        xxl={4}
                        style={{
                            borderTop: '1px solid rgb(0,0,0)',
                            borderLeft: '1px solid rgb(0,0,0)',
                        }}
                    >
                        <p
                            className='font-monospace text-truncate text-center mb-0 lh-1'
                            style={{
                                fontSize: '11px',
                                letterSpacing: '-0.75px',
                                padding: '1px',
                            }}
                        >
                            {lesson?.teacherClassrooms?.at(0)?.classroom?.cabinet ?? ''}
                            <br/>
                            {lesson?.teacherClassrooms?.at(1)?.classroom?.cabinet ?? ''}
                        </p>
                    </Col>
                </Row>
            </Col>
        </Row>
    )
}