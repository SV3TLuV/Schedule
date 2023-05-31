import {LessonForm} from "./LessonForm.tsx";
import {IDialog} from "../../../../../features/models/IDialog.ts";
import {ILesson} from "../../../../../features/models/ILesson.ts";
import {useUpdateLessonMutation} from "../../../../../store/apis/lessonApi.ts";
import {IGroup} from "../../../../../features/models/IGroup.ts";

interface IUpdateLessonDialog extends IDialog {
    lesson: ILesson
    group: IGroup
}

export const UpdateLessonDialog = ({ group, lesson, open, close }: IUpdateLessonDialog) => {
    const [update] = useUpdateLessonMutation()

    const handleSave = (lesson: ILesson) => update(lesson)

    return (
        <LessonForm
            title='Обновление пары'
            show={open}
            lesson={lesson}
            group={group}
            onClose={close}
            onSave={handleSave}
        />
    )
}