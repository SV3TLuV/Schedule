import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Button, Form, Modal} from "react-bootstrap";
import {IClassroom} from "../../../../../features/models";
import {TextField} from "@mui/material";
import {cabinetValidation} from "./validation";

interface IClassroomForm {
    title: string
    show: boolean
    classroom: IClassroom
    onClose: () => void
    onSave: (classroom: IClassroom) => void
}

export const ClassroomForm = ({title, show, classroom, onClose, onSave}: IClassroomForm) => {
    const {control, handleSubmit, reset, formState: {errors}} = useForm<IClassroom>({
        values: classroom,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<IClassroom> = data => {
        onSave(data)
        handleClose()
    }

    const handleClose = () => {
        reset(classroom)
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
                        name='cabinet'
                        rules={cabinetValidation}
                        render={({field}) => (
                            <Form.Group className='m-3'>
                                <TextField
                                    fullWidth
                                    label='Кабинет'
                                    size='small'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.cabinet?.message}
                                    helperText={errors.cabinet?.message}
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