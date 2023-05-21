import {IDiscipline} from "../../../../../features/models/IDiscipline";
import {IDialog} from "../../../../../features/models/IDialog";
import {useCreateDisciplineMutation} from "../../../../../store/apis/disciplineApi";
import {DisciplineForm} from "./DisciplineForm";

export const CreateDisciplineDialog = ({ open, close }: IDialog) => {
    const [create] = useCreateDisciplineMutation()

    const handleSave = (discipline: IDiscipline) => create(discipline)

    return (
        <DisciplineForm
            title='Добавление дисциплины'
            show={open}
            discipline={{} as IDiscipline}
            onClose={close}
            onSave={handleSave}
        />
    )
}