import {useDeleteLessonTemplateMutation} from "../../../../../store/apis";
import {useDialog} from "../../../../../hooks";
import {BaseLessonForm} from "./BaseLessonForm.tsx";
import {UpdateLessonTemplateDialog} from "../dialogs";
import {ILessonTemplate} from "../../../../../features/models";
import {IGroup} from "../../../../../features/models";

interface ILessonTemplateForm {
    lessonTemplate: ILessonTemplate
    group: IGroup
    templateTermId: number
}

export const LessonTemplateForm = ({group, lessonTemplate, templateTermId}: ILessonTemplateForm) => {
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
                templateTermId={templateTermId}
            />
        </>
    )
}