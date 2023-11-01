import {IDialog, IDisciplineName} from "../../../../../features/models";
import {useUpdateDisciplineNameMutation} from "../../../../../store/apis";
import {DisciplineNameForm} from "./DisciplineNameForm.tsx";

interface IUpdateDisciplineNameDialog extends IDialog {
    disciplineName: IDisciplineName
}

export const UpdateDisciplineNameDialog = ({disciplineName, open, close}: IUpdateDisciplineNameDialog) => {
    const [update] = useUpdateDisciplineNameMutation()

    const handleSave = (disciplineName: IDisciplineName) => update(disciplineName)

    return (
        <DisciplineNameForm
            title='Обновление названия для дисциплин'
            show={open}
            disciplineName={disciplineName}
            onClose={close}
            onSave={handleSave}
        />
    )
}