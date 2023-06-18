import {IDialog} from "../../../../../features/models";
import {ITime} from "../../../../../features/models";
import {useUpdateTimeMutation} from "../../../../../store/apis";
import {TimeForm} from "./TimeForm";

interface IUpdateTimeDialog extends IDialog {
    time: ITime
}

export const UpdateTimeDialog = ({time, open, close}: IUpdateTimeDialog) => {
    const [update] = useUpdateTimeMutation()

    const handleSave = (time: ITime) => update(time)

    return (
        <TimeForm
            title='Обновление времени'
            show={open}
            time={time}
            onClose={close}
            onSave={handleSave}
        />
    )
}