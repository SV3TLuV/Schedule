import {IDiscipline} from "../../../../../features/models/IDiscipline";
import {useGetDisciplineTypesQuery} from "../../../../../store/apis/discplineTypeApi";
import {useGetSpecialitiesQuery} from "../../../../../store/apis/specialityApi";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import {disciplineFormValidationSchema} from "./validation";
import {Loading} from "../../../../ui/Loading";
import {Button, Form, Modal} from "react-bootstrap";
import {Select} from "../../../../ui/Select";
import {useGetTermsQuery} from "../../../../../store/apis/termApi";
import {TextField} from "@mui/material";

interface IDisciplineForm {
    title: string
    show: boolean
    discipline: IDiscipline
    onClose: () => void
    onSave: (discipline: IDiscipline) => void
}

export const DisciplineForm = ({title, show, discipline, onClose, onSave}: IDisciplineForm) => {
    const {data: types} = useGetDisciplineTypesQuery()
    const {data: terms} = useGetTermsQuery()
    const {data: specialities} = useGetSpecialitiesQuery()
    const {control, handleSubmit, reset, formState: {errors}} = useForm<IDiscipline>({
        resolver: yupResolver(disciplineFormValidationSchema),
        values: discipline,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<IDiscipline> = data => {
        onSave(data)
        handleClose()
    }

    const handleClose = () => {
        reset(discipline)
        onClose()
    }

    if (!types || !specialities || !terms) {
        return <Loading/>
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
                        name='totalHours'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Кол-во часов'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.totalHours?.message}
                                    helperText={errors.totalHours?.message}
                                />
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
                                    label='Тип'
                                />
                                {errors.type && (
                                    <Form.Text className='text-danger'>
                                        {errors.type?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='term'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    value={field.value}
                                    options={terms.items}
                                    fields='id'
                                    label='Семестр'
                                />
                                {errors.term && (
                                    <Form.Text className='text-danger'>
                                        {errors.term?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='speciality'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    value={field.value}
                                    options={specialities.items}
                                    fields='name'
                                    label='Специальность'
                                />
                                {errors.speciality && (
                                    <Form.Text className='text-danger'>
                                        {errors.speciality?.message}
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