import {ITemplate} from "../../../../../features/models";
import {Button, Card} from "react-bootstrap";
import {LessonTemplateForm} from "./LessonTemplateForm.tsx";
import {useCreateLessonTemplateMutation} from "../../../../../store/apis";
import {ILessonTemplate} from "../../../../../features/models";
import {numberValidation} from "../dialogs";

interface ITemplateForm {
    template: ITemplate
}

export const TemplateForm = ({template}: ITemplateForm) => {
    const [create] = useCreateLessonTemplateMutation()

    const handleCreate = () => {
        const numbers =  template.lessonTemplates.map(lessonTemplate => lessonTemplate.number)
        const number = Math.max(...numbers) + 1

        if (numberValidation.validate(number) === true) {
            create({
                id: 0,
                number: number,
                subgroup: null,
                templateId: template.id,
                time: null,
                discipline: null,
                teacherClassrooms: []
            } as ILessonTemplate)
        }
    }

    return (
        <Card
            className='text-center'
            style={{
                minWidth: '280px',
                maxWidth: '280px',
            }}
        >
            <Card.Header>
                <Card.Title>
                    {template.groupNames}
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
                {template.lessonTemplates?.map(lessonTemplate => (
                    <LessonTemplateForm
                        key={lessonTemplate.id}
                        lessonTemplate={lessonTemplate}
                        group={template.groups[0]}
                        templateTermId={template.term.id}
                    />
                ))}
                <Button className='mb-3' onClick={handleCreate}>
                    Добавить пару
                </Button>
            </Card.Body>
        </Card>
    )
}