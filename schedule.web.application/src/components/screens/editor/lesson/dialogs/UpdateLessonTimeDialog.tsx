import {Button, Form, Modal} from "react-bootstrap";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {IUpdateFilledLessonsTimeCommand} from "../../../../../features/commands";
import {useGetLessonNumbersQuery, useUpdateFilledLessonsTimeMutation} from "../../../../../store/apis";
import {ITimeType} from "../../../../../features/models";
import {usePaginationQuery} from "../../../../../hooks";
import {useGetTimeTypesQuery} from "../../../../../store/apis";
import {useInfinitySelect} from "../../../../../hooks";
import {timeTypeValidation} from "../../timeType/dialogs";
import {Select} from "../../../../ui";
import {IDialog} from "../../../../../features/models";

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

    const {data: pairNumbers} = useGetLessonNumbersQuery(dateId ?? 1)
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
            >
                <Modal.Header closeButton className='text-center'>
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
                                <Form.Text>
                                    (Оставить пустым, для изменения всех)
                                </Form.Text>
                                <Select
                                    onChange={field.onChange}
                                    value={field.value}
                                    options={pairNumbersOptions}
                                    label='Номера пар'
                                    renderValue={(item) => item.id}
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
                                    renderValue={(item) => item.name}
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