import {IDialog} from "../../../../../features/models/IDialog";
import {useCreateTimeMutation} from "../../../../../store/apis/timeApi";
import {ITime} from "../../../../../features/models/ITime";
import {TimeForm} from "./TimeForm";
import {emptyTime} from "../../../../../utils/defaultModels";

export const CreateTimeDialog = ({open, close}: IDialog) => {
    const [create] = useCreateTimeMutation()

    const handleSave = (time: ITime) => create(time)

    return (
        <TimeForm
            title='Добавление времени'
            show={open}
            time={emptyTime}
            onClose={close}
            onSave={handleSave}
        />
    )
}