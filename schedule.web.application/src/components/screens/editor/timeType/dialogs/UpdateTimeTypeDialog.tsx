import {IDialog} from "../../../../../features/models";
import {ITimeType} from "../../../../../features/models";
import {useUpdateTimeTypeMutation} from "../../../../../store/apis";
import {TimeTypeForm} from "./TimeTypeForm";

interface IUpdateTimeTypeDialog extends IDialog {
    timeType: ITimeType
}

export const UpdateTimeTypeDialog = ({timeType, open, close}: IUpdateTimeTypeDialog) => {
    const [update] = useUpdateTimeTypeMutation()

    const handleSave = (timeType: ITimeType) => update(timeType)

    return (
        <TimeTypeForm
            title='Обновление вида времени'
            show={open}
            timeType={timeType}
            onClose={close}
            onSave={handleSave}
        />
    )
}