import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import {classroomFormValidationSchema} from "./validation.ts";
import {Button, Form, Modal} from "react-bootstrap";
import {IClassroom} from "../../../../../features/models/IClassroom.ts";
import {useGetClassroomTypesQuery} from "../../../../../store/apis/classroomTypeApi.ts";
import {Loading} from "../../../../ui/Loading.tsx";
import {Select} from "../../../../ui/Select.tsx";
import {TextField} from "@mui/material";

interface IClassroomForm {
    title: string
    show: boolean
    classroom: IClassroom
    onClose: () => void
    onSave: (classroom: IClassroom) => void
}

export const ClassroomForm = ({title, show, classroom, onClose, onSave}: IClassroomForm) => {
    const {data: types} = useGetClassroomTypesQuery()
    const {control, handleSubmit, reset, formState: {errors}} = useForm<IClassroom>({
        resolver: yupResolver(classroomFormValidationSchema),
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

    if (!types) {
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
                        name='cabinet'
                        render={({field}) => (
                            <Form.Group className='m-3' >
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
                    <Controller
                        control={control}
                        name='types'
                        render={({field}) => (
                            <Form.Group className='m-3'>
                                <Select
                                    onChange={field.onChange}
                                    value={field.value}
                                    options={types.items}
                                    fields='name'
                                    label='Виды'
                                    multiple
                                />
                                {errors.types && (
                                    <Form.Text className='text-danger'>
                                        {errors.types?.message}
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