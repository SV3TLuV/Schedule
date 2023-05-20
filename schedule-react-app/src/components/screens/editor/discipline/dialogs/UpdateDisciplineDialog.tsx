import {IDialog} from "../../../../../features/models/IDialog";
import {IDiscipline} from "../../../../../features/models/IDiscipline";
import {useUpdateDisciplineMutation} from "../../../../../store/apis/disciplineApi";
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