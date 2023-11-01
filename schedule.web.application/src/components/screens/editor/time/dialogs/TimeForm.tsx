import {ITime} from "../../../../../features/models";
import {useGetTimeTypesQuery} from "../../../../../store/apis";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Loading} from "../../../../ui";
import {Button, Form, Modal} from "react-bootstrap";
import {Select} from "../../../../ui";
import {TextField} from "@mui/material";
import {usePaginationQuery} from "../../../../../hooks";
import {useInfinitySelect} from "../../../../../hooks";
import {ITimeType} from "../../../../../features/models";
import {durationValidation, endValidation, lessonNumberValidation, startValidation} from "./validation";
import {timeTypeValidation} from "../../timeType/dialogs";

interface ITimeForm {
    title: string
    show: boolean
    time: ITime
    onClose: () => void
    onSave: (time: ITime) => void
}

export const TimeForm = ({title, show, time, onClose, onSave}: ITimeForm) => {
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

    const {control, handleSubmit, reset, formState: {errors}} = useForm<ITime>({
        values: time,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<ITime> = data => {
        onSave(data)
        handleClose()
    }

    const handleClose = () => {
        reset(time)
        onClose()
    }

    if (!types) {
        return <Loading/>
    }

    return (
        <Modal
            onHide={onClose}
            show={show}
            centered
        >
            <Form
                onSubmit={handleSubmit(onSubmit)}
                className='text-center'
            >
                <Modal.Header closeButton>
                    <Modal.Title>
                        {title}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Controller
                        control={control}
                        name='start'
                        rules={startValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Начало'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.start?.message}
                                    helperText={errors.start?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='end'
                        rules={endValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Конец'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.end?.message}
                                    helperText={errors.end?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='duration'
                        rules={durationValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Длительность (академ. час)'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.duration?.message}
                                    helperText={errors.duration?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='lessonNumber'
                        rules={lessonNumberValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Номер пары'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.lessonNumber?.message}
                                    helperText={errors.lessonNumber?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='type'
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
                                    label='Вид'
                                    error={!!errors.type?.message}
                                    helperText={errors.type?.message}
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