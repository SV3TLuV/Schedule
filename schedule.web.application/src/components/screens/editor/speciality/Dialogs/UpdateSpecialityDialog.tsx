import {IDialog} from "../../../../../features/models";
import {ISpeciality} from "../../../../../features/models";
import {useUpdateSpecialityMutation} from "../../../../../store/apis";
import {SpecialityForm} from "./SpecialityForm";

interface IUpdateSpecialityDialog extends IDialog {
    speciality: ISpeciality
}

export const UpdateSpecialityDialog = ({ speciality, open, close }: IUpdateSpecialityDialog) => {
    const [update] = useUpdateSpecialityMutation()

    const handleSave = (speciality: ISpeciality) => update(speciality)

    return (
        <SpecialityForm
            title='Обновление специальности'
            show={open}
            speciality={speciality}
            onClose={close}
            onSave={handleSave}
        />
    )
}