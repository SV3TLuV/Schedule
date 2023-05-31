import {IGroup} from "../../../../../features/models/IGroup";
import {useGetTermsQuery} from "../../../../../store/apis/termApi";
import {useGetSpecialitiesQuery} from "../../../../../store/apis/specialityApi";
import {useGetGroupsQuery} from "../../../../../store/apis/groupApi";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Loading} from "../../../../ui/Loading";
import {Button, Form, Modal} from "react-bootstrap";
import {Select} from "../../../../ui/Select";
import {TextField} from "@mui/material";
import {usePaginationQuery} from "../../../../../hooks/usePaginationQuery.ts";
import {useInfinitySelect} from "../../../../../hooks/useInfinitySelect.ts";
import {ISpeciality} from "../../../../../features/models/ISpeciality.ts";
import {ITerm} from "../../../../../features/models/ITerm.ts";
import {enrollmentYearValidation, numberValidation} from "./validation";
import {termValidation} from "../../term/validation";
import {specialityValidation} from "../../speciality/Dialogs/validation";

interface IGroupForm {
    title: string,
    show: boolean,
    group: IGroup,
    onClose: () => void,
    onSave: (group: IGroup) => void
}

export const GroupForm = ({title, show, group, onClose, onSave}: IGroupForm) => {
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
    const groupOptions = groups
        .filter(g => g.id !== group.id)
        .filter(g => group.speciality ? g.speciality.id === group.speciality.id : true)

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

    const {control, handleSubmit, reset, formState: {errors}} = useForm<IGroup>({
        values: group,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<IGroup> = data => {
        onSave(data)
        handleClose()
    }

    const handleClose = () => {
        reset(group)
        onClose()
    }

    if (!terms || !specialities || !groupData) {
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
                        name='number'
                        rules={numberValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Номер'
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
                        name='enrollmentYear'
                        rules={enrollmentYearValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    size='small'
                                    label='Год поступления'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.enrollmentYear?.message}
                                    helperText={errors.enrollmentYear?.message}
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
                    <Controller
                        control={control}
                        name='mergedGroups'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreGroups}
                                    onSearch={searchGroups}
                                    value={field.value}
                                    options={groupOptions}
                                    fields='name'
                                    label='Объединение с группой'
                                    multiple
                                    error={!!errors.mergedGroups?.message}
                                    helperText={errors.mergedGroups?.message}
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