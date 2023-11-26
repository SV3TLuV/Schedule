import {ISpeciality} from "../../../../../features/models";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Button, Form, Modal} from "react-bootstrap";
import {TextField} from "@mui/material";
import {Select} from "../../../../ui";
import {usePaginationQuery} from "../../../../../hooks";
import {useGetTermsQuery} from "../../../../../store/apis";
import {useInfinitySelect} from "../../../../../hooks";
import {ITerm} from "../../../../../features/models";
import {ICourse} from "../../../../../features/models";
import {codeValidation, nameValidation, termValidation} from "./validation";

interface ISpecialityForm {
    title: string
    show: boolean
    speciality: ISpeciality
    onClose: () => void
    onSave: (speciality: ISpeciality) => void
}

interface ISpecialityFormState {
    id: number
    code: string
    name: string
    term: ITerm
}

export const SpecialityForm = ({title, show, speciality, onClose, onSave}: ISpecialityForm) => {
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

    const defaultValue: ISpecialityFormState = {
        id: speciality.id,
        code: speciality.code,
        name: speciality.name,
        term: {
            id: speciality.maxTermId,
            courseTerm: 0,
            course: {} as ICourse
        } as ITerm,
    }

    const {control, handleSubmit, reset, formState: {errors}} = useForm<ISpecialityFormState>({
        values: defaultValue,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<ISpecialityFormState> = data => {
        onSave({
            id: data.id,
            code: data.code,
            name: data.name,
            maxTermId: data.term.id,
        } as ISpeciality)
        handleClose()
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
                        name='code'
                        rules={codeValidation}
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
                        rules={nameValidation}
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
                                    renderValue={(item) => item?.id}
                                    label='Кол-во семестров'
                                    error={!!errors.term?.message}
                                    helperText={errors.term?.message}
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