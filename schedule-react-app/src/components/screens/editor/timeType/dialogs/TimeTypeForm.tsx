import {ITimeType} from "../../../../../features/models/ITimeType";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import {timeTypeFormValidationSchema} from "./validation";
import {Button, Form, Modal} from "react-bootstrap";

interface ITimeTypeForm {
    title: string
    show: boolean
    timeType: ITimeType
    onClose: () => void
    onSave: (time: ITimeType) => void
}

export const TimeTypeForm = ({title, show, timeType, onClose, onSave}: ITimeTypeForm) => {
    const {control, handleSubmit, reset, formState: {errors}} = useForm<ITimeType>({
        resolver: yupResolver(timeTypeFormValidationSchema),
        values: timeType,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<ITimeType> = data => {
        onSave(data)
        reset(timeType)
        onClose()
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
                        name='name'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Form.Control
                                    placeholder='Вид'
                                    value={field.value}
                                    onChange={field.onChange}
                                />
                                {errors.name && (
                                    <Form.Text className='text-danger'>
                                        {errors.name?.message}
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