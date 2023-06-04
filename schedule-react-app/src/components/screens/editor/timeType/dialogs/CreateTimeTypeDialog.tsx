import {IDialog} from "../../../../../features/models";
import {useCreateTimeTypeMutation} from "../../../../../store/apis";
import {ITimeType} from "../../../../../features/models";
import {TimeTypeForm} from "./TimeTypeForm";

export const CreateTimeTypeDialog = ({open, close}: IDialog) => {
    const [create] = useCreateTimeTypeMutation()

    const handleSave = (timeType: ITimeType) => create(timeType)

    return (
        <TimeTypeForm
            title='Добавление вида времени'
            show={open}
            timeType={{} as ITimeType}
            onClose={close}
            onSave={handleSave}
        />
    )
}