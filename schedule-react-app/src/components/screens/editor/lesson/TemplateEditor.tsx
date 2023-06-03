import {Col, Container, Form, Row} from "react-bootstrap";
import {Controller, useForm, useWatch} from "react-hook-form";
import {IWeekType} from "../../../../features/models/IWeekType.ts";
import {ITerm} from "../../../../features/models/ITerm.ts";
import {IDay} from "../../../../features/models/IDay.ts";
import {useGetCurrentWeekTypeQuery, useGetWeekTypesQuery} from "../../../../store/apis/weekTypeApi.ts";
import {useGetCurrentDayQuery, useGetDaysQuery} from "../../../../store/apis/dayApi.ts";
import {Select} from "../../../ui/Select.tsx";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {useInfinitySelect} from "../../../../hooks/useInfinitySelect.ts";
import {useGetTermsQuery} from "../../../../store/apis/termApi.ts";
import {useGetTemplatesQuery} from "../../../../store/apis/templateApi.ts";
import {ITemplate} from "../../../../features/models/ITemplate.ts";
import {TemplateForm} from "./forms/TemplateForm.tsx";
import {IGroup} from "../../../../features/models/IGroup.ts";
import {useGetGroupsQuery} from "../../../../store/apis/groupApi.ts";

interface ITemplateEditorState {
    weekType: IWeekType
    term: ITerm
    day: IDay
    group: IGroup
}

export const TemplateEditor = () => {
    const {data: currentWeekType } = useGetCurrentWeekTypeQuery()
    const {data: currentDay } = useGetCurrentDayQuery()

    const {control, formState: {errors}} = useForm<ITemplateEditorState>({
        values: {
            weekType: currentWeekType ?? {} as IWeekType,
            day: currentDay ?? {} as IDay,
            term: { id: 1 } as ITerm,
            group: {} as IGroup
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

    const selectedWeekType = useWatch({ control, name: 'weekType' })
    const selectedTerm = useWatch({ control, name: 'term' })
    const selectedDay = useWatch({ control, name: 'day' })
    const selectedGroup = useWatch({ control, name: 'group' })

    const [templateQuery, setTemplateQuery] = usePaginationQuery({ pageSize: 24 })
    const {data: templateData} = useGetTemplatesQuery({
        page: templateQuery.page,
        pageSize: templateQuery.pageSize,
        weekTypeId: selectedWeekType?.id ?? null,
        termId: selectedTerm?.id ?? null,
        dayId: selectedDay?.id ?? null,
        groupId: selectedGroup?.id ?? null
    })
    const {
        options: templates,
        loadMore: loadMoreTemplates,
        clear: clearTemplates
    } = useInfinitySelect<ITemplate>({
        query: templateQuery,
        setQuery: setTemplateQuery,
        data: templateData,
    })

    const handleScroll = (event: React.UIEvent<HTMLUListElement>) => {
        const { scrollTop, clientHeight, scrollHeight } = event.currentTarget
        const isScrolledToBottom = scrollTop + clientHeight === scrollHeight

        if (isScrolledToBottom) {
            loadMoreTemplates()
        }
    }

    return (
        <Container
            onScroll={handleScroll}
            style={{
                height: 'calc(100vh - 72px - 42px)',
                overflow: 'scroll'
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
                                    clearTemplates()
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
                                    clearTemplates()
                                    field.onChange(e)
                                }}
                                onLoadMore={loadMoreDays}
                                onSearch={searchDays}
                                value={field.value}
                                options={days}
                                fields='name'
                                label='День'
                                clearable={false}
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
                                    clearTemplates()
                                    field.onChange(e)
                                }}
                                onLoadMore={loadMoreTerms}
                                onSearch={searchTerms}
                                value={field.value}
                                options={terms}
                                fields='id'
                                label='Семестр'
                                clearable={false}
                                variant='filled'
                                error={!!errors.term?.message}
                                helperText={errors.term?.message}
                            />
                        </Form.Group>
                    )}
                />
                <Controller
                    control={control}
                    name='group'
                    render={({field}) => (
                        <Form.Group className='p-0'>
                            <Select
                                onChange={(e) => {
                                    clearTemplates()
                                    field.onChange(e)
                                }}
                                onLoadMore={loadMoreGroups}
                                onSearch={searchGroups}
                                value={field.value}
                                options={groups}
                                fields='name'
                                label='Группа'
                                variant='filled'
                                error={!!errors.group?.message}
                                helperText={errors.group?.message}
                            />
                        </Form.Group>
                    )}
                />
            </Row>
            <Row
                style={{
                    maxHeight: 'calc(100vh - 72px - 42px - 48px - 48px - 48px - 48px)'
                }}
            >
                {templates.map(template => (
                    <Col
                        key={template.id}
                        className='d-flex justify-content-center align-items-center my-3'
                    >
                        <TemplateForm template={template}/>
                    </Col>
                ))}
            </Row>
        </Container>
    )
}