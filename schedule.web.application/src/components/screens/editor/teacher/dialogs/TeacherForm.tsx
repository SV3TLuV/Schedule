import {ITeacher} from "../../../../../features/models";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Button, Form, Modal} from "react-bootstrap";
import {TextField} from "@mui/material";
import {emailValidation, middleNameValidation, nameValidation, surnameValidation} from "./validation";

interface ITeacherForm {
    title: string
    show: boolean
    teacher: ITeacher
    onClose: () => void
    onSave: (teacher: ITeacher) => void
}

export const TeacherForm = ({title, show, teacher, onClose, onSave}: ITeacherForm) => {
    const {control, handleSubmit, reset, formState: {errors}} = useForm<ITeacher>({
        values: teacher,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<ITeacher> = data => {
        onSave(data)
        handleClose()
    }

    const handleClose = () => {
        reset(teacher)
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
                        name='surname'
                        rules={surnameValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Фамилия'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.surname?.message}
                                    helperText={errors.surname?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='name'
                        rules={nameValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    label='Имя'
                                    size='small'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.name?.message}
                                    helperText={errors.name?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='middleName'
                        rules={middleNameValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Отчество'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.middleName?.message}
                                    helperText={errors.middleName?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='email'
                        rules={emailValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Почта'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.email?.message}
                                    helperText={errors.email?.message}
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