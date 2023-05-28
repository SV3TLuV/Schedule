import {IDialog} from "../../../../../features/models/IDialog.ts";
import {ILessonTemplate} from "../../../../../features/models/ILessonTemplate.ts";
import {useUpdateLessonTemplateMutation} from "../../../../../store/apis/lessonTemplateApi.ts";
import {LessonTemplateForm} from "./LessonTemplateForm.tsx";

interface IUpdateLessonTemplateDialog extends IDialog {
    lessonTemplate: ILessonTemplate
}

export const UpdateLessonTemplateDialog = ({lessonTemplate, open, close}: IUpdateLessonTemplateDialog) => {
    const [update] = useUpdateLessonTemplateMutation()

    const handleSave = (lessonTemplate: ILessonTemplate) => update(lessonTemplate)

    return (
        <LessonTemplateForm
            title={'Обновление шаблона пары'}
            show={open}
            lessonTemplate={lessonTemplate}
            onClose={close}
            onSave={handleSave}
        />
    )
}