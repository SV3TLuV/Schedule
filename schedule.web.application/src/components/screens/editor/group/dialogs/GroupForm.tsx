import {IGroup} from "../../../../../features/models";
import {useGetGroupsAvailableForJoinQuery} from "../../../../../store/apis";
import {useGetSpecialitiesQuery} from "../../../../../store/apis";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Loading} from "../../../../ui";
import {Button, Form, Modal} from "react-bootstrap";
import {Select} from "../../../../ui";
import {TextField} from "@mui/material";
import {usePaginationQuery} from "../../../../../hooks";
import {useInfinitySelect} from "../../../../../hooks";
import {ISpeciality} from "../../../../../features/models";
import {enrollmentYearValidation, mergedGroupsValidation, numberValidation} from "./validation";
import {specialityValidation} from "../../speciality/Dialogs";
import {IGetAvailableForJoinGroupQuery} from "../../../../../features/queries/IGetAvailableForJoinGroupQuery.ts";

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

    const {control, handleSubmit, getValues, reset, formState: {errors}} = useForm<IGroup>({
        values: group,
        mode: 'onChange',
    })

    const groupState = getValues()

    const {data: availableGroups = []} = useGetGroupsAvailableForJoinQuery({
        groupId: groupState?.id ?? null,
        termId: groupState.term ? groupState.term.id : 0,
        specialityId: groupState.speciality ? groupState.speciality.id : 0
    } as IGetAvailableForJoinGroupQuery, { skip: !groupState.term || !groupState.speciality })

    const onSubmit: SubmitHandler<IGroup> = data => {
        onSave(data)
        handleClose()
    }

    const handleClose = () => {
        reset(group)
        onClose()
    }

    if (!specialities) {
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
                                    renderValue={(item) => `${item.name} | ${item.code}`}
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
                        rules={mergedGroupsValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    value={field.value}
                                    options={availableGroups}
                                    renderValue={(item) => item.name}
                                    label='Объединение с группой'
                                    multiple
                                    error={!!errors.mergedGroups?.message}
                                    helperText={errors.mergedGroups?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='isAfterEleven'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Form.Check
                                    inline
                                    onChange={field.onChange}
                                    checked={field.value}
                                    label='После 11 класса'
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