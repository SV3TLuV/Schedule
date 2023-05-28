import {Container, Form, Row} from "react-bootstrap";
import {Controller, useForm} from "react-hook-form";
import {IWeekType} from "../../../../features/models/IWeekType.ts";
import {ITerm} from "../../../../features/models/ITerm.ts";
import {IDay} from "../../../../features/models/IDay.ts";
import {useGetCurrentWeekTypeQuery, useGetWeekTypesQuery} from "../../../../store/apis/weekTypeApi.ts";
import {useGetCurrentDayQuery, useGetDaysQuery} from "../../../../store/apis/dayApi.ts";
import {Select} from "../../../ui/Select.tsx";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {useInfinitySelect} from "../../../../hooks/useInfinitySelect.ts";
import {useGetTermsQuery} from "../../../../store/apis/termApi.ts";

interface ITemplateEditorState {
    weekType: IWeekType
    term: ITerm
    day: IDay
}

export const TemplateEditor = () => {
    const {data: currentWeekType } = useGetCurrentWeekTypeQuery()
    const {data: currentDay } = useGetCurrentDayQuery()

    const {control, formState: {errors}} = useForm<ITemplateEditorState>({
        values: {
            weekType: currentWeekType ?? {} as IWeekType,
            day: currentDay ?? {} as IDay,
            term: {} as ITerm
        },
        mode: 'onChange',
    })

    const [weekTypeQuery, setWeekTypeQuery] = usePaginationQuery()
    const {data: weekTypeData} = useGetWeekTypesQuery(weekTypeQuery)
    const {
        options: weekTypes,
        loadMore: loadMoreWeekTypes,
        search: searchWeekTypes
    } = useInfinitySelect<IWeekType>({
        query: weekTypeQuery,
        setQuery: setWeekTypeQuery,
        data: weekTypeData
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

    const [dayQuery, setDayQuery] = usePaginationQuery()
    const {data: dayData} = useGetDaysQuery(dayQuery)
    const {
        options: days,
        loadMore: loadMoreDays,
        search: searchDays
    } = useInfinitySelect<IDay>({
        query: dayQuery,
        setQuery: setDayQuery,
        data: dayData
    })

    return (
        <Container
            style={{
                height: 'calc(100vh - 72px - 42px)',
                overflow: 'hidden'
            }}
        >
            <Row className='mb-3'>
                <Controller
                    control={control}
                    name='weekType'
                    render={({field}) => (
                        <Form.Group className='p-0'>
                            <Select
                                onChange={(e) => {
                                    field.onChange(e)
                                }}
                                onLoadMore={loadMoreWeekTypes}
                                onSearch={searchWeekTypes}
                                value={field.value}
                                options={weekTypes}
                                fields='name'
                                label='Вид недели'
                                variant='filled'
                                clearable={false}
                                error={!!errors.weekType?.message}
                                helperText={errors.weekType?.message}
                            />
                        </Form.Group>
                    )}
                />
                <Controller
                    control={control}
                    name='day'
                    render={({field}) => (
                        <Form.Group className='p-0'>
                            <Select
                                onChange={(e) => {
                                    field.onChange(e)
                                }}
                                onLoadMore={loadMoreDays}
                                onSearch={searchDays}
                                value={field.value}
                                options={days}
                                fields='name'
                                label='День'
                                variant='filled'
                                error={!!errors.day?.message}
                                helperText={errors.day?.message}
                            />
                        </Form.Group>
                    )}
                />
                <Controller
                    control={control}
                    name='term'
                    render={({field}) => (
                        <Form.Group className='p-0'>
                            <Select
                                onChange={(e) => {
                                    field.onChange(e)
                                }}
                                onLoadMore={loadMoreTerms}
                                onSearch={searchTerms}
                                value={field.value}
                                options={terms}
                                fields='id'
                                label='Семестр'
                                variant='filled'
                                error={!!errors.term?.message}
                                helperText={errors.term?.message}
                            />
                        </Form.Group>
                    )}
                />
            </Row>
        </Container>
    )
}