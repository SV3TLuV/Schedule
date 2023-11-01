import {useCreateDisciplineNameMutation} from "../../../../../store/apis";
import {IDialog, IDisciplineName} from "../../../../../features/models";
import {DisciplineNameForm} from "./DisciplineNameForm.tsx";

export const CreateDisciplineNameDialog = ({ open, close }: IDialog) => {
    const [create] = useCreateDisciplineNameMutation()

    const handleSave = (disciplineName: IDisciplineName) => create(disciplineName)

    return (
        <DisciplineNameForm
            title='Добавление кода дисциплин'
            show={open}
            disciplineName={{} as IDisciplineName}
            onClose={close}
            onSave={handleSave}
        />
    )
}