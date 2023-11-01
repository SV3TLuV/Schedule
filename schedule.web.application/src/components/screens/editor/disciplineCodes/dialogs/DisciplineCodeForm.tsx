import {IDisciplineCode} from "../../../../../features/models";
import {Button, Form, Modal} from "react-bootstrap";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {TextField} from "@mui/material";
import {codeValidation} from "./validation.ts";

interface IDisciplineCodeForm {
    title: string
    show: boolean
    disciplineCode: IDisciplineCode
    onClose: () => void
    onSave: (disciplineCode: IDisciplineCode) => void
}

export const DisciplineCodeForm = ({title, show, disciplineCode, onClose, onSave} : IDisciplineCodeForm) => {
    const {control, handleSubmit, reset, formState: {errors}} = useForm<IDisciplineCode>({
        values: disciplineCode,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<IDisciplineCode> = data => {
        onSave(data)
        handleClose()
    }

    const handleClose = () => {
        reset(disciplineCode)
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
                        name='code'
                        rules={codeValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    label='Код'
                                    size='small'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.code?.message}
                                    helperText={errors.code?.message}
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