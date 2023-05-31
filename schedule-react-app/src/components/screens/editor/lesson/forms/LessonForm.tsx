import {ILesson} from "../../../../../features/models/ILesson.ts";
import {useDeleteLessonMutation} from "../../../../../store/apis/lessonApi.ts";
import {UpdateLessonDialog} from "../dialogs/UpdateLessonDialog.tsx";
import {useDialog} from "../../../../../hooks/useDialog.ts";
import {BaseLessonForm} from "./BaseLessonForm.tsx";
import {IGroup} from "../../../../../features/models/IGroup.ts";

interface ILessonForm {
    lesson: ILesson
    group: IGroup
}

export const LessonForm = ({lesson, group}: ILessonForm) => {
    const [remove] = useDeleteLessonMutation()
    const updateDialog = useDialog()

    const handleDelete = () => remove(lesson.id)

    return (
        <>
            <BaseLessonForm
                item={lesson}
                onChange={updateDialog.show}
                onDelete={handleDelete}
            />
            <UpdateLessonDialog
                lesson={lesson}
                {...updateDialog}
                group={group}
            />
        </>
    )
}