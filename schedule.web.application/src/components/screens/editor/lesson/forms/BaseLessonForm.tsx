import {useState} from "react";
import {Button, Col, Container, Row} from "react-bootstrap";
import {IconButton} from "@mui/material";
import {LessonFormTypography} from "./LessonFormTypography.tsx";
import {ILesson} from "../../../../../features/models";
import {ILessonTemplate} from "../../../../../features/models";
import {AiOutlineDelete} from "react-icons/ai";

interface IBaseLessonForm {
    item: ILesson | ILessonTemplate
    onChange: () => void
    onDelete: (id: number) => void
}

export const BaseLessonForm = ({item, onChange, onDelete}: IBaseLessonForm) => {
    const [isBlurred, setIsBlurred] = useState(() => false)

    const showButtons = () => setIsBlurred(true)
    const hideButtons = () => setIsBlurred(false)

    const handleDelete = () => onDelete(item.id)

    const itemIsChanged = 'isChanged' in item && item.isChanged;

    return (
        <Container
            className='text-center my-4 position-relative'
            onMouseEnter={showButtons}
            onMouseLeave={hideButtons}
        >
            {isBlurred &&
                <Button
                    className='position-absolute top-50 start-50 translate-middle'
                    onClick={onChange}
                    style={{zIndex: 1}}
                >
                    Изменить
                </Button>
            }
            {isBlurred && item.number > 4 &&
                <IconButton
                    className='position-absolute top-0 start-100 text-danger'
                    onClick={handleDelete}
                    style={{
                        zIndex: 1,
                        transform: "translate(-100%, 0)"
                    }}
                >
                    <AiOutlineDelete/>
                </IconButton>
            }
            <div style={isBlurred ? {filter: 'blur(4px)'} : {}}>
                <Row className='my-1'>
                    <LessonFormTypography
                        text={item?.discipline?.name.name ?? ''}
                        isChanged={itemIsChanged}
                    />
                </Row>
                <Row className='my-1'>
                    {item?.subgroup &&
                        <LessonFormTypography
                            text={`Подгруппа: ${item.subgroup}`}
                            isChanged={itemIsChanged}
                        />
                    }
                </Row>
                <Row className='my-1'>
                    <Col className='p-0' xs={7}>
                        <LessonFormTypography
                            text={item?.teacherClassrooms?.at(0)?.teacher?.shortFio ?? ''}
                            isChanged={itemIsChanged}
                        />
                    </Col>
                    <Col className='p-0' xs={5}>
                        <LessonFormTypography
                            text={item?.teacherClassrooms?.at(0)?.classroom?.cabinet ?? ''}
                            isChanged={itemIsChanged}
                        />
                    </Col>
                </Row>
                <Row className='my-1'>
                    <Col className='p-0' xs={7}>
                        <LessonFormTypography
                            text={item?.teacherClassrooms?.at(1)?.teacher?.shortFio ?? ''}
                            isChanged={itemIsChanged}
                        />
                    </Col>
                    <Col className='p-0' xs={5}>
                        <LessonFormTypography
                            text={item?.teacherClassrooms?.at(1)?.classroom?.cabinet ?? ''}
                            isChanged={itemIsChanged}
                        />
                    </Col>
                </Row>
                <Row className='my-1'>
                    <LessonFormTypography
                        text={item.time ? `${item?.time?.start} - ${item?.time?.end}` : ''}
                        isChanged={itemIsChanged}
                    />
                </Row>
            </div>
        </Container>
    )
}