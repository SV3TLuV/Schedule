import {ITime} from "../../../../../features/models/ITime";
import {useGetTimeTypesQuery} from "../../../../../store/apis/timeTypeApi";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import {timeFormValidationSchema} from "./validation";
import {Loading} from "../../../../ui/Loading";
import {Button, Form, Modal} from "react-bootstrap";
import {Select} from "../../../../ui/Select";

interface ITimeForm {
    title: string
    show: boolean
    time: ITime
    onClose: () => void
    onSave: (time: ITime) => void
}

export const TimeForm = ({title, show, time, onClose, onSave}: ITimeForm) => {
    const {data: types} = useGetTimeTypesQuery()
    const {control, handleSubmit, reset, formState: {errors}} = useForm<ITime>({
        resolver: yupResolver(timeFormValidationSchema),
        values: time,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<ITime> = data => {
        onSave(data)
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
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Form.Control
                                    placeholder='Начало'
                                    value={field.value}
                                    onChange={field.onChange}
                                />
                                {errors.start && (
                                    <Form.Text className='text-danger'>
                                        {errors.start?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='end'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Form.Control
                                    placeholder='Конец'
                                    value={field.value}
                                    onChange={field.onChange}
                                />
                                {errors.end && (
                                    <Form.Text className='text-danger'>
                                        {errors.end?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='duration'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Form.Control
                                    placeholder='Длительность'
                                    value={field.value}
                                    onChange={field.onChange}
                                />
                                {errors.duration && (
                                    <Form.Text className='text-danger'>
                                        {errors.duration?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='lessonNumber'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Form.Control
                                    placeholder='Номер пары'
                                    value={field.value}
                                    onChange={field.onChange}
                                />
                                {errors.lessonNumber && (
                                    <Form.Text className='text-danger'>
                                        {errors.lessonNumber?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='type'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    value={field.value}
                                    options={types.items}
                                    fields='name'
                                    label='Виды'
                                />
                                {errors.type && (
                                    <Form.Text className='text-danger'>
                                        {errors.type?.message}
                                    </Form.Text>
                                )}
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