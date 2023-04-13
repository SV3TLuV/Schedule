import {SubmitHandler, useForm, useFormState} from "react-hook-form";
import {ILoginCommand} from "../../features/commands/ILoginCommand";

export const LoginForm = () => {
    const { control, handleSubmit } = useForm<ILoginCommand>({})
    const {errors} = useFormState({control})

    const onSubmit: SubmitHandler<ILoginCommand> = async data => {
    }

    return (
        <div></div>
    )
}