import {ILesson} from "../../../../../features/models";
import {useDeleteLessonMutation} from "../../../../../store/apis";
import {UpdateLessonDialog} from "../dialogs";
import {useDialog} from "../../../../../hooks";
import {BaseLessonForm} from "./BaseLessonForm.tsx";
import {IGroup} from "../../../../../features/models";

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