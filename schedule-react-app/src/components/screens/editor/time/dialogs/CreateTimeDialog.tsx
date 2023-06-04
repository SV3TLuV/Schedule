import {IDialog} from "../../../../../features/models";
import {useCreateTimeMutation} from "../../../../../store/apis";
import {ITime} from "../../../../../features/models";
import {TimeForm} from "./TimeForm";

export const CreateTimeDialog = ({open, close}: IDialog) => {
    const [create] = useCreateTimeMutation()

    const handleSave = (time: ITime) => create(time)

    return (
        <TimeForm
            title='Добавление времени'
            show={open}
            time={{} as ITime}
            onClose={close}
            onSave={handleSave}
        />
    )
}