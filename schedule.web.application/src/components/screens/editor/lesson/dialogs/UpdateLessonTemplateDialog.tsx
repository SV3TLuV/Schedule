import {IDialog} from "../../../../../features/models";
import {ILessonTemplate} from "../../../../../features/models";
import {useUpdateLessonTemplateMutation} from "../../../../../store/apis";
import {LessonTemplateForm} from "./LessonTemplateForm.tsx";
import {IGroup} from "../../../../../features/models";

interface IUpdateLessonTemplateDialog extends IDialog {
    lessonTemplate: ILessonTemplate
    group: IGroup
    templateTermId: number
}

export const UpdateLessonTemplateDialog = ({group, lessonTemplate, templateTermId, open, close}: IUpdateLessonTemplateDialog) => {
    const [update] = useUpdateLessonTemplateMutation()

    const handleSave = (lessonTemplate: ILessonTemplate) => update(lessonTemplate)

    return (
        <LessonTemplateForm
            title={'Обновление шаблона пары'}
            show={open}
            lessonTemplate={lessonTemplate}
            group={group}
            templateTermId={templateTermId}
            onClose={close}
            onSave={handleSave}
        />
    )
}