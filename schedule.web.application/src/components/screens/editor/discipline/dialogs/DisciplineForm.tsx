import {IDiscipline, IDisciplineCode, IDisciplineName} from "../../../../../features/models";
import {
    useGetDisciplineCodesQuery,
    useGetDisciplineNamesQuery,
    useGetDisciplineTypesQuery
} from "../../../../../store/apis";
import {useGetSpecialitiesQuery} from "../../../../../store/apis";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Loading} from "../../../../ui";
import {Button, Form, Modal} from "react-bootstrap";
import {Select} from "../../../../ui";
import {useGetTermsQuery} from "../../../../../store/apis";
import {TextField} from "@mui/material";
import {usePaginationQuery} from "../../../../../hooks";
import {useInfinitySelect} from "../../../../../hooks";
import {ISpeciality} from "../../../../../features/models";
import {ITerm} from "../../../../../features/models";
import {IDisciplineType} from "../../../../../features/models";
import {codeValidation, nameValidation, totalHoursValidation} from "./validation";
import {disciplineTypeValidation} from "../../disciplineType/validation";
import {termValidation} from "../../term/validation";
import {specialityValidation} from "../../speciality/Dialogs";

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

    const [codeQuery, setCodeQuery] = usePaginationQuery()
    const {data: codeData} = useGetDisciplineCodesQuery(codeQuery)
    const {
        options: codes,
        loadMore: loadMoreCodes,
        search: searchCodes
    } = useInfinitySelect<IDisciplineCode>({
        query: codeQuery,
        setQuery: setCodeQuery,
        data: codeData
    })

    const [nameQuery, setNameQuery] = usePaginationQuery()
    const {data: nameData} = useGetDisciplineNamesQuery(nameQuery)
    const {
        options: names,
        loadMore: loadMoreNames,
        search: searchNames
    } = useInfinitySelect<IDisciplineName>({
        query: nameQuery,
        setQuery: setNameQuery,
        data: nameData
    })

    const {control, handleSubmit, reset, formState: {errors}} = useForm<IDiscipline>({
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
                        rules={nameValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreNames}
                                    onSearch={searchNames}
                                    value={field.value}
                                    options={names}
                                    fields='name'
                                    label='Название'
                                    error={!!errors.name?.message}
                                    helperText={errors.name?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='code'
                        rules={codeValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreCodes}
                                    onSearch={searchCodes}
                                    value={field.value}
                                    options={codes}
                                    fields='name'
                                    label='Код'
                                    error={!!errors.code?.message}
                                    helperText={errors.code?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='totalHours'
                        rules={totalHoursValidation}
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
                        rules={disciplineTypeValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreTypes}
                                    onSearch={searchTypes}
                                    value={field.value}
                                    options={types}
                                    fields='name'
                                    label='Вид'
                                    error={!!errors.type?.message}
                                    helperText={errors.type?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='term'
                        rules={termValidation}
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
                        rules={specialityValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreSpecialities}
                                    onSearch={searchSpecialities}
                                    value={field.value}
                                    options={specialities}
                                    fields={['name', 'code']}
                                    fieldSplitter=' | '
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