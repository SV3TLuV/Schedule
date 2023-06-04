import {ISpeciality} from "../../../../../features/models";
import {useCreateSpecialityMutation} from "../../../../../store/apis";
import {SpecialityForm} from "./SpecialityForm";
import {IDialog} from "../../../../../features/models";

export const CreateSpecialityDialog = ({ open, close }: IDialog) => {
    const [create] = useCreateSpecialityMutation()

    const handleSave = (speciality: ISpeciality) => create(speciality)

    return (
        <SpecialityForm
            title='Добавление специальности'
            show={open}
            speciality={{} as ISpeciality}
            onClose={close}
            onSave={handleSave}
        />
    )
}