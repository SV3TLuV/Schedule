import {ITimeType} from "../../../../../features/models/ITimeType";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Button, Form, Modal} from "react-bootstrap";
import {TextField} from "@mui/material";
import {nameValidation} from "./validation";

interface ITimeTypeForm {
    title: string
    show: boolean
    timeType: ITimeType
    onClose: () => void
    onSave: (time: ITimeType) => void
}

export const TimeTypeForm = ({title, show, timeType, onClose, onSave}: ITimeTypeForm) => {
    const {control, handleSubmit, reset, formState: {errors}} = useForm<ITimeType>({
        values: timeType,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<ITimeType> = data => {
        onSave(data)
        handleClose()
    }

    const handleClose = () => {
        reset(timeType)
        onClose()
    }

    return (
        <Modal
            onHide={handleClose}
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
                        rules={nameValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    label='Вид'
                                    size='small'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.name?.message}
                                    helperText={errors.name?.message}
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