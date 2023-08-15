import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {ILesson} from "../../../../../features/models";
import {Button, Form, Modal} from "react-bootstrap";
import {usePaginationQuery} from "../../../../../hooks";
import {useGetTimesQuery} from "../../../../../store/apis";
import {useInfinitySelect} from "../../../../../hooks";
import {useGetDisciplinesQuery} from "../../../../../store/apis";
import {ITime} from "../../../../../features/models";
import {IDiscipline} from "../../../../../features/models";
import {ITeacher} from "../../../../../features/models";
import {useGetTeachersQuery} from "../../../../../store/apis";
import {useGetClassroomsQuery} from "../../../../../store/apis";
import {IClassroom} from "../../../../../features/models";
import {TextField} from "@mui/material";
import {Select} from "../../../../ui";
import {useMemo} from "react";
import {classroomsValidation, numberValidation, subgroupValidation, teachersValidation} from "./validation";
import {IGroup} from "../../../../../features/models";

interface ILessonForm {
    title: string
    show: boolean
    lesson: ILesson
    group: IGroup
    onClose: () => void
    onSave: (lesson: ILesson) => void
}

interface ILessonFormState {
    id: number
    number: number
    subgroup: number | null
    time: ITime | null
    discipline: IDiscipline | null
    teachers: ITeacher[]
    classrooms: IClassroom[]
}

export const LessonForm = ({ title, show, group, lesson, onClose, onSave }: ILessonForm) => {
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
    const disciplineOptions = disciplines.filter(d => d.term.id === group.term.id)

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

    const defaultValue: ILessonFormState = useMemo(() => ({
        id: lesson.id,
        number: lesson.number,
        subgroup: lesson.subgroup,
        time: lesson.time,
        discipline: lesson.discipline,
        teachers: lesson.teacherClassrooms?.map(item => item.teacher) ?? [],
        classrooms: lesson.teacherClassrooms
            ?.map(item => item.classroom)
            ?.filter(classroom => classroom.id !== 0) ?? []
    }), [lesson])

    const {control, handleSubmit, reset, setError, formState: {errors}} = useForm<ILessonFormState>({
        values: defaultValue,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<ILessonFormState> = async data => {
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
                isChanged: false,
                timetableId: lesson.timetableId,
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

    const handleClear = () => {
        // @ts-ignore
        reset(value => ({
            ...value,
            subgroup: '',
            time: null,
            discipline: null,
            teachers: [],
            classrooms: []
        }))
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
                                    fields={['start', 'end']}
                                    fieldSplitter='-'
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
                                    fields='name'
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
                                    fields={['surname', 'name', 'middleName']}
                                    fieldSplitter=' '
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
                                    fields='cabinet'
                                    label='Кабинеты'
                                    multiple
                                    error={!!errors.classrooms?.message}
                                    helperText={errors.classrooms?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Button
                        className='mx-auto'
                        variant="outline-danger"
                        onClick={handleClear}
                    >
                        Очистить
                    </Button>
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