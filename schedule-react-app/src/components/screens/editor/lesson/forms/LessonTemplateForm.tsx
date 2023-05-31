import {useDeleteLessonTemplateMutation} from "../../../../../store/apis/lessonTemplateApi.ts";
import {useDialog} from "../../../../../hooks/useDialog.ts";
import {BaseLessonForm} from "./BaseLessonForm.tsx";
import {UpdateLessonTemplateDialog} from "../dialogs/UpdateLessonTemplateDialog.tsx";
import {ILessonTemplate} from "../../../../../features/models/ILessonTemplate.ts";
import {IGroup} from "../../../../../features/models/IGroup.ts";

interface ILessonTemplateForm {
    lessonTemplate: ILessonTemplate
    group: IGroup
}

export const LessonTemplateForm = ({group, lessonTemplate}: ILessonTemplateForm) => {
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
            <UpdateLessonTemplateDialog
                lessonTemplate={lessonTemplate}
                {...updateDialog}
                group={group}
            />
        </>
    )
}