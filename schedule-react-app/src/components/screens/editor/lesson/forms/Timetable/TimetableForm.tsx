import {Card} from "react-bootstrap";
import {ITimetable} from "../../../../../../features/models/ITimetable.ts";
import {LessonForm} from "../LessonForm.tsx";

interface ITimetableForm {
    timetable: ITimetable
}

export const TimetableForm = ({timetable}: ITimetableForm) => {
    return (
        <Card
            style={{ minWidth: '280px', maxWidth: '400px'}}
            className='text-center'
        >
            <Card.Header>
                <Card.Title>
                    {timetable.groupNames}
                </Card.Title>
            </Card.Header>
            <Card.Body className='py-1'>
                {timetable.lessons.map(lesson => (
                    <LessonForm
                        key={lesson.id}
                        lesson={lesson}
                    />
                ))}
            </Card.Body>
        </Card>
    )
}