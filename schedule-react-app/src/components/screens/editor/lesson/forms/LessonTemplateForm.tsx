import {useDeleteLessonTemplateMutation} from "../../../../../store/apis/lessonTemplateApi.ts";
import {useDialog} from "../../../../../hooks/useDialog.ts";
import {BaseLessonForm} from "./BaseLessonForm.tsx";
import {UpdateLessonTemplateDialog} from "../dialogs/UpdateLessonTemplateDialog.tsx";
import {ILessonTemplate} from "../../../../../features/models/ILessonTemplate.ts";

interface ILessonTemplateForm {
    lessonTemplate: ILessonTemplate
}

export const LessonTemplateForm = ({lessonTemplate}: ILessonTemplateForm) => {
    const [remove] = useDeleteLessonTemplateMutation()
    const updateDialog = useDialog()

    const handleDelete = () => remove(lessonTemplate.id)

    return (
        <>
            <BaseLessonForm
                item={lessonTemplate}
                onChange={updateDialog.show}
                onDelete={handleDelete}
            />
            <UpdateLessonTemplateDialog lessonTemplate={lessonTemplate} {...updateDialog}/>
        </>
    )
}