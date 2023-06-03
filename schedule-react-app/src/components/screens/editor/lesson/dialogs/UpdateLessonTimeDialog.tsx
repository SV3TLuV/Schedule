import {Button, Form, Modal} from "react-bootstrap";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {IUpdateFilledLessonsTimeCommand} from "../../../../../features/commands/IUpdateFilledLessonsTimeCommand.ts";
import {useGetLessonNumbersQuery, useUpdateFilledLessonsTimeMutation} from "../../../../../store/apis/lessonApi.ts";
import {ITimeType} from "../../../../../features/models/ITimeType.ts";
import {usePaginationQuery} from "../../../../../hooks/usePaginationQuery.ts";
import {useGetTimeTypesQuery} from "../../../../../store/apis/timeTypeApi.ts";
import {useInfinitySelect} from "../../../../../hooks/useInfinitySelect.ts";
import {timeTypeValidation} from "../../timeType/dialogs/validation.ts";
import {Select} from "../../../../ui/Select.tsx";
import {IDialog} from "../../../../../features/models/IDialog.ts";

interface IUpdateLessonTimeDialog extends IDialog{
    dateId: number
}

interface IIUpdateLessonTimeDialogState {
    timeType: ITimeType
    pairNumbers: { id: number }[] | null
}

export const UpdateLessonTimeDialog = ({ open, close, dateId }: IUpdateLessonTimeDialog) => {
    const [update] = useUpdateFilledLessonsTimeMutation()

    const [typeQuery, setTypeQuery] = usePaginationQuery()
    const {data: typeData} = useGetTimeTypesQuery(typeQuery)
    const {
        options: types,
        loadMore: loadMoreTypes,
        search: searchTypes
    } = useInfinitySelect<ITimeType>({
        query: typeQuery,
        setQuery: setTypeQuery,
        data: typeData
    })

    const {control, handleSubmit, reset, formState: {errors}}
        = useForm<IIUpdateLessonTimeDialogState>({
            mode: 'onChange',
        })

    const {data: pairNumbers} = useGetLessonNumbersQuery(dateId)
    const pairNumbersOptions = pairNumbers?.map(number => ({ id: number })) ?? []

    const onSubmit: SubmitHandler<IIUpdateLessonTimeDialogState> = data => {
        update({
            dateId,
            timeTypeId: data.timeType.id,
            pairNumbers: data.pairNumbers?.map(number => number.id)
        } as IUpdateFilledLessonsTimeCommand)
        handleClose()
    }

    const handleClose = () => {
        reset({} as IIUpdateLessonTimeDialogState)
        close()
    }

    return (
        <Modal
            onHide={handleClose}
            show={open}
            centered
        >
            <Form
                onSubmit={handleSubmit(onSubmit)}
                className='text-center'
            >
                <Modal.Header>
                    <Modal.Title>
                        Изменить время пар
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Controller
                        control={control}
                        name='pairNumbers'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    value={field.value}
                                    options={pairNumbersOptions}
                                    label='Номера пар'
                                    fields='id'
                                    multiple
                                    error={!!errors.pairNumbers?.message}
                                    helperText={errors.pairNumbers?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='timeType'
                        rules={timeTypeValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreTypes}
                                    onSearch={searchTypes}
                                    value={field.value}
                                    options={types}
                                    fields='name'
                                    label='Вид времени'
                                    error={!!errors.timeType?.message}
                                    helperText={errors.timeType?.message}
                                />
                            </Form.Group>
                        )}
                    />
                </Modal.Body>
                <Modal.Footer>
                    <Button type='submit' className='mx-auto'>
                        Сохранить
                    </Button>
                </Modal.Footer>
            </Form>
        </Modal>
    )
}