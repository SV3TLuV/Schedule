import {ILesson} from "../../../../features/models";
import {Col, Container, Row} from "react-bootstrap";
import {getShortFio} from "../../../../utils/getShortFio.ts";
import {LessonFormTypography} from "../../editor/lesson/forms";

export const LessonDisplay = ({lesson}: { lesson: ILesson }) => {
    return (
        <Container
            className='text-center my-3 position-relative'
            style={lesson.isChanged
                ? { border: '1px solid #B05E5E'}
                : { border: '1px solid #17a9fd'}}
        >
            <Row>
                <LessonFormTypography
                    text={lesson.discipline?.name ?? ''}
                    isChanged={lesson.isChanged}
                />
            </Row>
            {lesson.subgroup &&
                <Row>
                    <LessonFormTypography
                        text={`(подгруппа: ${lesson.subgroup})`}
                        isChanged={lesson.isChanged}
                    />
                </Row>
            }
            <Row>
                <Col className='p-0' xs={8}>
                    <LessonFormTypography
                        text={getShortFio(lesson.teacherClassrooms?.at(0)?.teacher)}
                        isChanged={lesson.isChanged}
                    />
                </Col>
                <Col className='p-0' xs={4}>
                    <LessonFormTypography
                        text={lesson.teacherClassrooms?.at(0)?.classroom?.cabinet ?? ''}
                        isChanged={lesson.isChanged}
                    />
                </Col>
            </Row>
            <Row>
                <Col className='p-0' xs={8}>
                    <LessonFormTypography
                        text={getShortFio(lesson.teacherClassrooms?.at(1)?.teacher)}
                        isChanged={lesson.isChanged}
                    />
                </Col>
                <Col className='p-0' xs={4}>
                    <LessonFormTypography
                        text={lesson.teacherClassrooms?.at(1)?.classroom?.cabinet ?? ''}
                        isChanged={lesson.isChanged}
                    />
                </Col>
            </Row>
            <Row>
                <LessonFormTypography
                    text={lesson.time ? `${lesson.time.start} - ${lesson.time.end}` : ''}
                    isChanged={lesson.isChanged}
                />
            </Row>
        </Container>
    )
}