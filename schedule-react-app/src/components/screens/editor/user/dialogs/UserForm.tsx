import {IUser} from "../../../../../features/models";
import {Controller, SubmitHandler, useForm} from "react-hook-form";
import {Button, Form, Modal} from "react-bootstrap";
import {TextField} from "@mui/material";
import {loginValidation, passwordValidation, roleValidation} from "./validation.ts";
import {Select} from "../../../../ui";
import {usePaginationQuery} from "../../../../../hooks";
import {useInfinitySelect} from "../../../../../hooks";
import {IRole} from "../../../../../features/models";
import {useGetRolesQuery} from "../../../../../store/apis";

interface IUserForm {
    title: string
    show: boolean
    user: IUser
    onClose: () => void
    onSave: (user: IUser) => void
}

interface IUserState extends IUser {
    confirmPassword: string
}

export const UserForm = ({title, show, user, onClose, onSave}: IUserForm) => {
    const [roleQuery, setRoleQuery] = usePaginationQuery()
    const {data: roleData} = useGetRolesQuery(roleQuery)
    const {
        options: roles,
        loadMore: loadMoreRoles,
        search: searchRoles
    } = useInfinitySelect<IRole>({
        query: roleQuery,
        setQuery: setRoleQuery,
        data: roleData
    })

    const {control, handleSubmit, reset, setError, formState: {errors}} = useForm<IUserState>({
        values: {
            ...user,
            confirmPassword: ''
        } as IUserState,
        mode: 'onChange',
    })

    const onSubmit: SubmitHandler<IUserState> = data => {
        if (data.password !== data.confirmPassword) {
            const error = {
                message: 'Пароли не совпадают.'
            }

            setError('password', error)
            setError('confirmPassword', error)

            return;
        }

        onSave(data)
        handleClose()
    }

    const handleClose = () => {
        reset(user)
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
                        name='login'
                        rules={loginValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    label='Логин'
                                    size='small'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.login?.message}
                                    helperText={errors.login?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='password'
                        rules={passwordValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    label='Пароль'
                                    size='small'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.password?.message}
                                    helperText={errors.password?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='confirmPassword'
                        rules={passwordValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <TextField
                                    fullWidth
                                    label='Подтверждение пароля'
                                    size='small'
                                    value={field.value}
                                    onChange={field.onChange}
                                    error={!!errors.confirmPassword?.message}
                                    helperText={errors.confirmPassword?.message}
                                />
                            </Form.Group>
                        )}
                    />
                    <Controller
                        control={control}
                        name='role'
                        rules={roleValidation}
                        render={({field}) => (
                            <Form.Group className='m-3' >
                                <Select
                                    onChange={field.onChange}
                                    onLoadMore={loadMoreRoles}
                                    onSearch={searchRoles}
                                    value={field.value}
                                    options={roles}
                                    fields='name'
                                    label='Роль'
                                    error={!!errors.role?.message}
                                    helperText={errors.role?.message}
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