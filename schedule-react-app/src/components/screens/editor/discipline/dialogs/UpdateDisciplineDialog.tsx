import {IDialog} from "../../../../../features/models";
import {IDiscipline} from "../../../../../features/models";
import {useUpdateDisciplineMutation} from "../../../../../store/apis";
import {DisciplineForm} from "./DisciplineForm";

interface IUpdateDisciplineDialog extends IDialog {
    discipline: IDiscipline
}

export const UpdateDisciplineDialog = ({discipline, open, close}: IUpdateDisciplineDialog) => {
    const [update] = useUpdateDisciplineMutation()

    const handleSave = (discipline: IDiscipline) => update(discipline)

    return (
        <DisciplineForm
            title='Обновление дисциплины'
            show={open}
            discipline={discipline}
            onClose={close}
            onSave={handleSave}
        />
    )
}