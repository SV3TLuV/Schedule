import {IDiscipline} from "../../../../../features/models";
import {IDialog} from "../../../../../features/models";
import {useCreateDisciplineMutation} from "../../../../../store/apis";
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