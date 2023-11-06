import {ILessonTemplate} from "../../../../../features/models";
import {ITime} from "../../../../../features/models";
import {IDiscipline} from "../../../../../features/models";
import {ITeacher} from "../../../../../features/models";
import {IClassroom} from "../../../../../features/models";
import {usePaginationQuery} from "../../../../../hooks";
import {useGetTimesQuery} from "../../../../../store/apis";
import {useInfinitySelect} from "../../../../../hooks";
import {useGetDisciplinesQuery} from "../../../../../store/apis";
import {useGetTeachersQuery} from "../../../../../store/apis";
import {useGetClassroomsQuery} from "../../../../../store/apis";
import {useMemo} from "react";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Button, Form, Modal} from "react-bootstrap";
import {classroomsValidation, numberValidation, subgroupValidation, teachersValidation} from "./validation.ts";
import {TextField} from "@mui/material";
import {Select} from "../../../../ui";
import {IGroup} from "../../../../../features/models";

interface ILessonTemplateForm {
    title: string
    show: boolean
    lessonTemplate: ILessonTemplate
    group: IGroup
    templateTermId: number
    onClose: () => void
    onSave: (lesson: ILessonTemplate) => void
}

interface ILessonTemplateFormState {
    id: number
    number: number
    subgroup: number | null
    time: ITime | null
    discipline: IDiscipline | null
    teachers: ITeacher[]
    classrooms: IClassroom[]
}

export const LessonTemplateForm = ({ title, show, group, templateTermId, lessonTemplate, onClose, onSave }: ILessonTemplateForm) => {
    const [timeQuery, setTimeQuery] = usePaginationQuery()
    const {data: timeData} = useGetTimesQuery(timeQuery)
    const {
        options: times,
        loadMore: loadMoreTimes,
        search: searchTimes
    } = useInfinitySelect<ITime>({
        query: timeQuery,
        setQuery: setTimeQuery,
        data: timeData
    })

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
    const disciplineOptions = disciplines.filter(d =>
        d.speciality.id === group.speciality.id &&
        d.term.id === templateTermId)

    const [teacherQuery, setTeacherQuery] = usePaginationQuery()
    const {data: teacherData} = useGetTeachersQuery(teacherQuery)
    const {
        options: teachers,
        loadMore: loadMoreTeachers,
        search: searchTeachers
    } = useInfinitySelect<ITeacher>({
        query: teacherQuery,
        setQuery: setTeacherQuery,
        data: teacherData
    })

    const [classroomQuery, setClassroomQuery] = usePaginationQuery()
    const {data: classroomData} = useGetClassroomsQuery(classroomQuery)
    const {
        options: classrooms,
        loadMore: loadMoreClassrooms,
        search: searchClassrooms
    } = useInfinitySelect<IClassroom>({
        query: classroomQuery,
        setQuery: setClassroomQuery,
        data: classroomData
    })

    const defaultValue: ILessonTemplateFormState = useMemo(() => ({
        id: lessonTemplate.id,
        number: lessonTemplate.number,
        subgroup: lessonTemplate.subgroup,
        time: lessonTemplate.time,
        discipline: lessonTemplate.discipline,
        teachers: lessonTemplate.teacherClassrooms?.map(item => item.teacher) ?? [],
        classrooms: lessonTemplate.teacherClassrooms
            ?.map(item => item.classroom)
            ?.filter(classroom => classroom) ?? []
    }), [lessonTemplate])

    const {control, handleSubmit, reset, setError, formState: {errors}} = useForm<ILessonTemplateFormState>({
        values: defaultValue,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<ILessonTemplateFormState> = async data => {
        const inputsIsFilled = [
            data.classrooms.length === 0,
            data.teachers.length === 0,
            data.discipline === null,
            data.time === null
        ]

        const isValid = inputsIsFilled.every(input => input === inputsIsFilled[0])

        if (!isValid) {
            const emptyError = {
                message: 'Все поля должны быть заполнены, либо пустые'
            }

            setError("time", emptyError)
            setError("discipline", emptyError)
            setError("classrooms", emptyError)
            setError("teachers", emptyError)
        } else {
            onSave({
                id: data.id,
                number: data.number,
                subgroup: data.subgroup,
                templateId: lessonTemplate.templateId,
                time: data.time,
                discipline: data.discipline,
                teacherClassrooms: data.teachers.map((teacher, index) => ({
                    classroom: data.classrooms.at(index) ?? {} as IClassroom,
                    teacher: teacher
                }))
            })
            handleClose()
        }
    }

    const handleClose = () => {
        reset(defaultValue)
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
                        name='number'
                        rules={numberValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    label='Номер'
                                    size='small'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.number?.message}
                                    helperText={errors.number?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='subgroup'
                        rules={subgroupValidation}
                        render={({field}) => (
                            <Form.Group className='m-3'>
                                <TextField
                                    fullWidth
                                    label='Подгруппа'
                                    size='small'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.subgroup?.message}
                                    helperText={errors.subgroup?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='time'
                        render={({field}) => (
                            <Form.Group className='m-3'>
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreTimes}
                                    onSearch={searchTimes}
                                    value={field.value}
                                    options={times}
                                    renderValue={(item) => `${item.start}-${item.end}`}
                                    label='Время'
                                    error={!!errors.time?.message}
                                    helperText={errors.time?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='discipline'
                        render={({field}) => (
                            <Form.Group className='m-3'>
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreDisciplines}
                                    onSearch={searchDisciplines}
                                    value={field.value}
                                    options={disciplineOptions}
                                    renderValue={(item) => item.name.name}
                                    label='Дисциплина'
                                    error={!!errors.discipline?.message}
                                    helperText={errors.discipline?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='teachers'
                        rules={teachersValidation}
                        render={({field}) => (
                            <Form.Group className='m-3'>
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreTeachers}
                                    onSearch={searchTeachers}
                                    value={field.value}
                                    options={teachers}
                                    renderValue={(item) => `${item.surname} ${item.name} ${item.middleName}`}
                                    label='Преподаватели'
                                    multiple
                                    error={!!errors.teachers?.message}
                                    helperText={errors.teachers?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='classrooms'
                        rules={classroomsValidation}
                        render={({field}) => (
                            <Form.Group className='m-3'>
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreClassrooms}
                                    onSearch={searchClassrooms}
                                    value={field.value}
                                    options={classrooms}
                                    renderValue={(item) => item.cabinet}
                                    label='Кабинеты'
                                    multiple
                                    error={!!errors.classrooms?.message}
                                    helperText={errors.classrooms?.message}
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