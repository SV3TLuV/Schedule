import {IDialog} from "../../../../../features/models";
import {ILessonTemplate} from "../../../../../features/models";
import {useUpdateLessonTemplateMutation} from "../../../../../store/apis";
import {LessonTemplateForm} from "./LessonTemplateForm.tsx";
import {IGroup} from "../../../../../features/models";

interface IUpdateLessonTemplateDialog extends IDialog {
    lessonTemplate: ILessonTemplate
    group: IGroup
}

export const UpdateLessonTemplateDialog = ({group, lessonTemplate, open, close}: IUpdateLessonTemplateDialog) => {
    const [update] = useUpdateLessonTemplateMutation()

    const handleSave = (lessonTemplate: ILessonTemplate) => update(lessonTemplate)

    return (
        <LessonTemplateForm
            title={'Обновление шаблона пары'}
            show={open}
            lessonTemplate={lessonTemplate}
            group={group}
            onClose={close}
            onSave={handleSave}
        />
    )
}