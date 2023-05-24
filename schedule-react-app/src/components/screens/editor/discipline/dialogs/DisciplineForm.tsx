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
import {usePaginationQuery} from "../../../../../hooks/usePaginationQuery.ts";
import {useInfinitySelect} from "../../../../../hooks/useInfinitySelect.ts";
import {ISpeciality} from "../../../../../features/models/ISpeciality.ts";
import {ITerm} from "../../../../../features/models/ITerm.ts";
import {IDisciplineType} from "../../../../../features/models/IDisciplineType.ts";

interface IDisciplineForm {
    title: string
    show: boolean
    discipline: IDiscipline
    onClose: () => void
    onSave: (discipline: IDiscipline) => void
}

export const DisciplineForm = ({title, show, discipline, onClose, onSave}: IDisciplineForm) => {
    const [specialityQuery, setSpecialityQuery] = usePaginationQuery()
    const {data: specialityData} = useGetSpecialitiesQuery(specialityQuery)
    const {
        options: specialities,
        loadMore: loadMoreSpecialities,
        search: searchSpecialities
    } = useInfinitySelect<ISpeciality>({
        query: specialityQuery,
        setQuery: setSpecialityQuery,
        data: specialityData
    })

    const [typeQuery, setTypeQuery] = usePaginationQuery()
    const {data: typeData} = useGetDisciplineTypesQuery(typeQuery)
    const {
        options: types,
        loadMore: loadMoreTypes,
        search: searchTypes
    } = useInfinitySelect<IDisciplineType>({
        query: typeQuery,
        setQuery: setTypeQuery,
        data: typeData
    })

    const [termQuery, setTermQuery] = usePaginationQuery()
    const {data: termData} = useGetTermsQuery(termQuery)
    const {
        options: terms,
        loadMore: loadMoreTerms,
        search: searchTerms
    } = useInfinitySelect<ITerm>({
        query: termQuery,
        setQuery: setTermQuery,
        data: termData
    })

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
                                    onLoadMore={loadMoreTypes}
                                    onSearch={searchTypes}
                                    value={field.value}
                                    options={types}
                                    fields='name'
                                    label='Тип'
                                    error={!!errors.type?.message}
                                    helperText={errors.type?.message}
                                />
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
                                    onLoadMore={loadMoreTerms}
                                    onSearch={searchTerms}
                                    value={field.value}
                                    options={terms}
                                    fields='id'
                                    label='Семестр'
                                    error={!!errors.term?.message}
                                    helperText={errors.term?.message}
                                />
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
                                    onLoadMore={loadMoreSpecialities}
                                    onSearch={searchSpecialities}
                                    value={field.value}
                                    options={specialities}
                                    fields='name'
                                    label='Специальность'
                                    error={!!errors.speciality?.message}
                                    helperText={errors.speciality?.message}
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