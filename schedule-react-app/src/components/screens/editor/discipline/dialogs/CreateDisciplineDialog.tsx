import {IDiscipline} from "../../../../../features/models/IDiscipline";
import {emptyDisciplineType} from "../../disciplineType/dialogs/CreateDisciplineTypeDialog";
import {emptySpeciality} from "../../speciality/Dialogs/CreateSpecialityDialog";
import {IDialog} from "../../../../../features/models/IDialog";
import {useCreateDisciplineMutation} from "../../../../../store/apis/disciplineApi";
import {DisciplineForm} from "./DisciplineForm";

export const emptyDiscipline: IDiscipline = {
    id: 0,
    name: '',
    code: '',
    totalHours: 0,
    type: emptyDisciplineType,
    term: {
        id: 0,
        courseTerm: 0,
        course: {
            value: 0
        }
    },
    speciality: emptySpeciality,
    isDeleted: false
}

export const CreateDisciplineDialog = ({ open, close }: IDialog) => {
    const [create] = useCreateDisciplineMutation()

    const handleSave = (discipline: IDiscipline) => create(discipline)

    return (
        <DisciplineForm
            title='Добавление дисциплины'
            show={open}
            discipline={emptyDiscipline}
            onClose={close}
            onSave={handleSave}
        />
    )
}