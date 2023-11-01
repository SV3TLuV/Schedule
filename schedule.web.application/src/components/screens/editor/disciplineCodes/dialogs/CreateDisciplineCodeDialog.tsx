import {IDialog, IDisciplineCode} from "../../../../../features/models";
import {DisciplineCodeForm} from "./DisciplineCodeForm.tsx";
import {useCreateDisciplineCodeMutation} from "../../../../../store/apis";

export const CreateDisciplineCodeDialog = ({ open, close }: IDialog) => {
    const [create] = useCreateDisciplineCodeMutation()

    const handleSave = (disciplineCode: IDisciplineCode) => create(disciplineCode)

    return (
        <DisciplineCodeForm
            title='Добавление кода для дисциплин'
            show={open}
            disciplineCode={{} as IDisciplineCode}
            onClose={close}
            onSave={handleSave}
        />
    )
}