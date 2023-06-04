import {ITeacher} from "../../../../../features/models";
import {useGetDisciplinesQuery} from "../../../../../store/apis";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Loading} from "../../../../ui";
import {Button, Form, Modal} from "react-bootstrap";
import {Select} from "../../../../ui";
import {useGetGroupsQuery} from "../../../../../store/apis";
import {TextField} from "@mui/material";
import {usePaginationQuery} from "../../../../../hooks";
import {useInfinitySelect} from "../../../../../hooks";
import {IDiscipline} from "../../../../../features/models";
import {IGroup} from "../../../../../features/models";
import {emailValidation, middleNameValidation, nameValidation, surnameValidation} from "./validation";

interface ITeacherForm {
    title: string
    show: boolean
    teacher: ITeacher
    onClose: () => void
    onSave: (teacher: ITeacher) => void
}

export const TeacherForm = ({title, show, teacher, onClose, onSave}: ITeacherForm) => {
    const [disciplineQuery, setDisciplineQuery] = usePaginationQuery()
    const {data: disciplineData} = useGetDisciplinesQuery(disciplineQuery)
    const {
        options: disciplines,
        loadMore: loadMoreDisciplines,
        search: searchDisciplines
    } = useInfinitySelect<IDiscipline>({
        query: disciplineQuery,
        setQuery: setDisciplineQuery,
        data: disciplineData
    })

    const [groupQuery, setGroupQuery] = usePaginationQuery()
    const {data: groupData} = useGetGroupsQuery(groupQuery)
    const {
        options: groups,
        loadMore: loadMoreGroups,
        search: searchGroups
    } = useInfinitySelect<IGroup>({
        query: groupQuery,
        setQuery: setGroupQuery,
        data: groupData
    })

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
                    <Controller
                        control={control}
                        name='groups'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreGroups}
                                    onSearch={searchGroups}
                                    value={field.value}
                                    options={groups}
                                    fields='name'
                                    label='Группы'
                                    multiple
                                    error={!!errors.groups?.message}
                                    helperText={errors.groups?.message}
                                />
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
                                    onLoadMore={loadMoreDisciplines}
                                    onSearch={searchDisciplines}
                                    value={field.value}
                                    options={disciplines}
                                    fields='name'
                                    label='Дисциплины'
                                    multiple
                                    error={!!errors.disciplines?.message}
                                    helperText={errors.disciplines?.message}
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