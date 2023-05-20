import {IGroup} from "../../../../../features/models/IGroup";
import {useGetTermsQuery} from "../../../../../store/apis/termApi";
import {useGetSpecialitiesQuery} from "../../../../../store/apis/specialityApi";
import {useGetGroupsQuery} from "../../../../../store/apis/groupApi";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import {groupFormValidationSchema} from "./validation";
import {Loading} from "../../../../ui/Loading";
import {Button, Form, Modal} from "react-bootstrap";
import {Select} from "../../../../ui/Select";

interface IGroupForm {
    title: string,
    show: boolean,
    group: IGroup,
    onClose: () => void,
    onSave: (group: IGroup) => void
}

export const GroupForm = ({title, show, group, onClose, onSave}: IGroupForm) => {
    const {data: terms} = useGetTermsQuery()
    const {data: specialities} = useGetSpecialitiesQuery()
    const {groups} = useGetGroupsQuery({page: 1, pageSize: 100}, {
        selectFromResult: ({data}) => ({
            groups: (data?.items ?? []).filter(g => g.id !== group.id)
        })
    })
    const {control, handleSubmit, reset, formState: {errors}} = useForm<IGroup>({
        resolver: yupResolver(groupFormValidationSchema),
        values: group,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<IGroup> = data => {
        onSave(data)
        reset(group)
        onClose()
    }

    if (!terms || !specialities || !groups) {
        return <Loading/>
    }

    return (
        <Modal
            onHide={onClose}
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
                                <Form.Control
                                    placeholder='Название'
                                    value={field.value}
                                    onChange={field.onChange}
                                />
                                {errors.name && (
                                    <Form.Text className='text-danger'>
                                        {errors.name?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='number'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Form.Control
                                    placeholder='Номер'
                                    value={field.value}
                                    onChange={field.onChange}
                                />
                                {errors.number && (
                                    <Form.Text className='text-danger'>
                                        {errors.number?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='enrollmentYear'
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Form.Control
                                    placeholder='Год поступления'
                                    value={field.value}
                                    onChange={field.onChange}
                                />
                                {errors.enrollmentYear && (
                                    <Form.Text className='text-danger'>
                                        {errors.enrollmentYear?.message}
                                    </Form.Text>
                                )}
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
                                    value={field.value}
                                    options={terms.items}
                                    fields='id'
                                    label='Семестр'
                                />
                                {errors.term && (
                                    <Form.Text className='text-danger'>
                                        {errors.term?.message}
                                    </Form.Text>
                                )}
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
                                    value={field.value}
                                    options={specialities.items}
                                    fields='code'
                                    label='Специальность'
                                />
                                {errors.speciality && (
                                    <Form.Text className='text-danger'>
                                        {errors.speciality?.message}
                                    </Form.Text>
                                )}
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
                                    value={field.value}
                                    options={groups}
                                    fields='name'
                                    label='Объединение с группой'
                                    multiple
                                />
                                {errors.mergedGroups && (
                                    <Form.Text className='text-danger'>
                                        {errors.mergedGroups?.message}
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