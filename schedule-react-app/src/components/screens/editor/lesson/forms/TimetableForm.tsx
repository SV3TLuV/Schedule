import {Button, Card} from "react-bootstrap";
import {ITimetable} from "../../../../../features/models/ITimetable.ts";
import {LessonForm} from "./LessonForm.tsx";
import {useCreateLessonMutation} from "../../../../../store/apis/lessonApi.ts";
import {numberValidation} from "../dialogs/validation.ts";
import {ILesson} from "../../../../../features/models/ILesson.ts";

interface ITimetableForm {
    timetable: ITimetable
}

export const TimetableForm = ({timetable}: ITimetableForm) => {
    const [create] = useCreateLessonMutation()

    const handleCreate = () => {
        const numbers = timetable.lessons.map(lesson => lesson.number)
        const number = Math.max(...numbers) + 1

        if (numberValidation.validate(number) === true) {
            create({
                id: 0,
                number: number,
                subgroup: null,
                isChanged: false,
                timetableId: timetable.id,
                time: null,
                discipline: null,
                teacherClassrooms: []
            } as ILesson)
        }
    }

    return (
        <Card
            style={{
                minWidth: '280px',
                maxWidth: '280px',
            }}
            className='text-center'
        >
            <Card.Header>
                <Card.Title>
                    {timetable.groupNames}
                </Card.Title>
            </Card.Header>
            <Card.Body
                className='py-1'
                style={{
                    maxHeight: '760px',
                    overflowX: 'hidden',
                    overflowY: 'scroll'
                }}
            >
                {timetable.lessons.map(lesson => (
                    <LessonForm
                        key={lesson.id}
                        lesson={lesson}
                        group={timetable.groups[0]}
                    />
                ))}
                <Button className='mb-3' onClick={handleCreate}>
                    Добавить пару
                </Button>
            </Card.Body>
        </Card>
    )
}