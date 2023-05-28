import {ILesson} from "../../../../../features/models/ILesson.ts";
import {useDeleteLessonMutation} from "../../../../../store/apis/lessonApi.ts";
import {UpdateLessonDialog} from "../dialogs/UpdateLessonDialog.tsx";
import {useDialog} from "../../../../../hooks/useDialog.ts";
import {BaseLessonForm} from "./BaseLessonForm.tsx";

interface ILessonForm {
    lesson: ILesson
}

export const LessonForm = ({lesson}: ILessonForm) => {
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
            <UpdateLessonDialog lesson={lesson} {...updateDialog}/>
        </>
    )
}