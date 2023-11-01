import {IDisciplineName} from "../../../../../features/models";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Button, Form, Modal} from "react-bootstrap";
import {TextField} from "@mui/material";
import {nameValidation} from "./validation.ts";

interface IDisciplineNameForm {
    title: string
    show: boolean
    disciplineName: IDisciplineName
    onClose: () => void
    onSave: (disciplineName: IDisciplineName) => void
}

export const DisciplineNameForm = ({title, show, disciplineName, onClose, onSave}: IDisciplineNameForm) => {
    const {control, handleSubmit, reset, formState: {errors}} = useForm<IDisciplineName>({
        values: disciplineName,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<IDisciplineName> = data => {
        onSave(data)
        handleClose()
    }

    const handleClose = () => {
        reset(disciplineName)
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
                                    label='Название'
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