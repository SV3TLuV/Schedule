import {ISpeciality} from "../../../../../features/models/ISpeciality";
import {useGetDisciplinesQuery} from "../../../../../store/apis/disciplineApi";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import {specialityFormValidationSchema} from "./validation.ts";
import {Loading} from "../../../../ui/Loading";
import {Button, Form, Modal} from "react-bootstrap";
import {Select} from "../../../../ui/Select";
import {TextField} from "@mui/material";
import {usePaginationQuery} from "../../../../../hooks/usePaginationQuery.ts";
import {useInfinitySelect} from "../../../../../hooks/useInfinitySelect.ts";
import {IDiscipline} from "../../../../../features/models/IDiscipline.ts";

interface ISpecialityForm {
    title: string
    show: boolean
    speciality: ISpeciality
    onClose: () => void
    onSave: (speciality: ISpeciality) => void
}

export const SpecialityForm = ({title, show, speciality, onClose, onSave}: ISpecialityForm) => {
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

    if (!disciplines) {
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