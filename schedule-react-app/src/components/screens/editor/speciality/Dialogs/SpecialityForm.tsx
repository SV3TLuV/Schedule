import {ISpeciality} from "../../../../../features/models/ISpeciality";
import {useGetDisciplinesQuery} from "../../../../../store/apis/disciplineApi";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import {specialityFormValidationSchema} from "./validation";
import {Loading} from "../../../../ui/Loading";
import {Button, Form, Modal} from "react-bootstrap";
import {Select} from "../../../../ui/Select";

interface ISpecialityForm {
    title: string
    show: boolean
    speciality: ISpeciality
    onClose: () => void
    onSave: (speciality: ISpeciality) => void
}

export const SpecialityForm = ({title, show, speciality, onClose, onSave}: ISpecialityForm) => {
    const {data: disciplines} = useGetDisciplinesQuery()
    const {control, handleSubmit, reset, formState: {errors}} = useForm<ISpeciality>({
        resolver: yupResolver(specialityFormValidationSchema),
        values: speciality,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<ISpeciality> = data => {
        onSave(data)
        reset(speciality)
        onClose()
    }

    if (!disciplines) {
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
                        name='code'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Form.Control
                                    placeholder='Код'
                                    value={field.value}
                                    onChange={field.onChange}
                                />
                                {errors.code && (
                                    <Form.Text className='text-danger'>
                                        {errors.code?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='name'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Form.Control
                                    placeholder='Название'
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
                    <Controller
                        control={control}
                        name='maxTermId'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Form.Control
                                    placeholder='Количество семестров'
                                    value={field.value}
                                    onChange={field.onChange}
                                />
                                {errors.maxTermId && (
                                    <Form.Text className='text-danger'>
                                        {errors.maxTermId?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='disciplines'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    value={field.value}
                                    options={disciplines.items}
                                    fields='name'
                                    label='Дисциплины'
                                    multiple
                                />
                                {errors.disciplines && (
                                    <Form.Text className='text-danger'>
                                        {errors.disciplines?.message}
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