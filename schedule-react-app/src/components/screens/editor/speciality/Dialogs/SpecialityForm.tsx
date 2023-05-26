import {ISpeciality} from "../../../../../features/models/ISpeciality";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import {specialityFormValidationSchema} from "./validation.ts";
import {Button, Form, Modal} from "react-bootstrap";
import {TextField} from "@mui/material";

interface ISpecialityForm {
    title: string
    show: boolean
    speciality: ISpeciality
    onClose: () => void
    onSave: (speciality: ISpeciality) => void
}

export const SpecialityForm = ({title, show, speciality, onClose, onSave}: ISpecialityForm) => {
    const {control, handleSubmit, reset, formState: {errors}} = useForm<ISpeciality>({
        resolver: yupResolver(specialityFormValidationSchema),
        values: speciality,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<ISpeciality> = data => {
        onSave(data)
        handleClose()
    }

    const handleClose = () => {
        reset(speciality)
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
                    <Controller
                        control={control}
                        name='name'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Название'
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
                        name='maxTermId'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Кол-во семестров'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.maxTermId?.message}
                                    helperText={errors.maxTermId?.message}
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