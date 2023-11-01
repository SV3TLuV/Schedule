import {IDialog, IDisciplineCode} from "../../../../../features/models";
import {useUpdateDisciplineCodeMutation} from "../../../../../store/apis";
import {DisciplineCodeForm} from "./DisciplineCodeForm.tsx";

interface IUpdateDisciplineCodeDialog extends IDialog {
    disciplineCode: IDisciplineCode
}

export const UpdateDisciplineCodeDialog = ({disciplineCode, open, close}: IUpdateDisciplineCodeDialog) => {
    const [update] = useUpdateDisciplineCodeMutation()

    const handleSave = (disciplineCode: IDisciplineCode) => update(disciplineCode)

    return (
        <DisciplineCodeForm
            title='Обновление кода для дисциплин'
            show={open}
            disciplineCode={disciplineCode}
            onClose={close}
            onSave={handleSave}
        />
    )
}