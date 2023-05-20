import {IDialog} from "../../../../../features/models/IDialog";
import {useCreateTimeTypeMutation} from "../../../../../store/apis/timeTypeApi";
import {ITimeType} from "../../../../../features/models/ITimeType";
import {TimeTypeForm} from "./TimeTypeForm";
import {emptyTimeType} from "../../../../../utils/defaultModels";

export const CreateTimeTypeDialog = ({open, close}: IDialog) => {
    const [create] = useCreateTimeTypeMutation()

    const handleSave = (timeType: ITimeType) => create(timeType)

    return (
        <TimeTypeForm
            title='Добавление вида времени'
            show={open}
            timeType={emptyTimeType}
            onClose={close}
            onSave={handleSave}
        />
    )
}