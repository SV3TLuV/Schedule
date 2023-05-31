import {IDialog} from "../../../../../features/models/IDialog.ts";
import {ILessonTemplate} from "../../../../../features/models/ILessonTemplate.ts";
import {useUpdateLessonTemplateMutation} from "../../../../../store/apis/lessonTemplateApi.ts";
import {LessonTemplateForm} from "./LessonTemplateForm.tsx";
import {IGroup} from "../../../../../features/models/IGroup.ts";

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