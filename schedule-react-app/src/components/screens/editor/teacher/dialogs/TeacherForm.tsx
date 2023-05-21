import {ITeacher} from "../../../../../features/models/ITeacher";
import {useGetDisciplinesQuery} from "../../../../../store/apis/disciplineApi";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import {Loading} from "../../../../ui/Loading";
import {Button, Form, Modal} from "react-bootstrap";
import {Select} from "../../../../ui/Select";
import {useGetGroupsQuery} from "../../../../../store/apis/groupApi";
import {teacherFormValidatorSchema} from "./validation";
import {TextField} from "@mui/material";

interface ITeacherForm {
    title: string
    show: boolean
    teacher: ITeacher
    onClose: () => void
    onSave: (teacher: ITeacher) => void
}

export const TeacherForm = ({title, show, teacher, onClose, onSave}: ITeacherForm) => {
    const {data: disciplines} = useGetDisciplinesQuery()
    const {data: groups} = useGetGroupsQuery()
    const {control, handleSubmit, reset, formState: {errors}} = useForm<ITeacher>({
        resolver: yupResolver(teacherFormValidatorSchema),
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

    if (!disciplines || !groups) {
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
                        name='surname'
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
                        name='groups'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    value={field.value}
                                    options={groups.items}
                                    fields='name'
                                    label='Группы'
                                    multiple
                                />
                                {errors.groups && (
                                    <Form.Text className='text-danger'>
                                        {errors.groups?.message}
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
                                    options={groups.items}
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