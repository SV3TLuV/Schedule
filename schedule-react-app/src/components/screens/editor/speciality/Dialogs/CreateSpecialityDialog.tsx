import {ISpeciality} from "../../../../../features/models/ISpeciality";
import {useCreateSpecialityMutation} from "../../../../../store/apis/specialityApi";
import {SpecialityForm} from "./SpecialityForm";
import {emptySpeciality} from "../../../../../utils/defaultModels";
import {IDialog} from "../../../../../features/models/IDialog";

export const CreateSpecialityDialog = ({ open, close }: IDialog) => {
    const [create] = useCreateSpecialityMutation()

    const handleSave = (speciality: ISpeciality) => create(speciality)

    return (
        <SpecialityForm
            title='Добавление специальности'
            show={open}
            speciality={emptySpeciality}
            onClose={close}
            onSave={handleSave}
        />
    )
}