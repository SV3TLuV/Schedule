import {ITemplate} from "../../../../../features/models/ITemplate.ts";
import {Card} from "react-bootstrap";
import {LessonTemplateForm} from "./LessonTemplateForm.tsx";

interface ITemplateForm {
    template: ITemplate
}

export const TemplateForm = ({template}: ITemplateForm) => {
    return (
        <Card
            style={{ minWidth: '280px', maxWidth: '400px'}}
            className='text-center'
        >
            <Card.Header>
                <Card.Title>
                    {template.groupNames}
                </Card.Title>
            </Card.Header>
            <Card.Body className='py-1'>
                {template.lessons?.map(lessonTemplate => (
                    <LessonTemplateForm
                        key={lessonTemplate.id}
                        lessonTemplate={lessonTemplate}
                    />
                ))}
            </Card.Body>
        </Card>
    )
}