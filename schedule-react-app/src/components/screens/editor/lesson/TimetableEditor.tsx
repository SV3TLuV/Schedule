import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {useGetCurrentDateQuery, useGetDatesQuery} from "../../../../store/apis/dateApi.ts";
import {useInfinitySelect} from "../../../../hooks/useInfinitySelect.ts";
import {IDate} from "../../../../features/models/IDate.ts";
import {Col, Container, Form, Row} from "react-bootstrap";
import {Select} from "../../../ui/Select.tsx";
import {Controller, useForm, useWatch} from "react-hook-form";
import {IGroup} from "../../../../features/models/IGroup.ts";
import {useGetGroupsQuery} from "../../../../store/apis/groupApi.ts";
import {ITimetable} from "../../../../features/models/ITimetable.ts";
import {useGetTimetablesQuery} from "../../../../store/apis/timetableApi.ts";
import {TimetableForm} from "./forms/TimetableForm.tsx";

interface ITimetableEditorState {
    group: IGroup
    date: IDate
}

export const TimetableEditor = () => {
    const {data: currentDate} = useGetCurrentDateQuery()

    const {control, formState: {errors}} = useForm<ITimetableEditorState>({
        values: {
            date: currentDate ?? {} as IDate,
            group: {} as IGroup
        },
        mode: 'onChange',
    })

    const [dateQuery, setDateQuery] = usePaginationQuery()
    const {data: dateData} = useGetDatesQuery(dateQuery)
    const {
        options: dates,
        loadMore: loadMoreDates,
        search: searchDates
    } = useInfinitySelect<IDate>({
        query: dateQuery,
        setQuery: setDateQuery,
        data: dateData
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

    const selectedDate = useWatch({ control, name: 'date' })
    const selectedGroup = useWatch({ control, name: 'group' })

    const [timetableQuery, setTimetableQuery] = usePaginationQuery({ pageSize: 40 })
    const {data: timetableData} = useGetTimetablesQuery({
        page: timetableQuery.page,
        pageSize: timetableQuery.pageSize,
        dateId: selectedDate.id,
        groupId: selectedGroup?.id,
    })
    const {
        options: timetables,
        loadMore: loadMoreTimetables,
        clear: clearTimetables
    } = useInfinitySelect<ITimetable>({
        query: timetableQuery,
        setQuery: setTimetableQuery,
        data: timetableData,
    })

    const resetTimetableQuery = () => setTimetableQuery(prev => ({...prev, page: 1}))

    const handleScroll = (event: React.UIEvent<HTMLUListElement>) => {
        const { scrollTop, clientHeight, scrollHeight } = event.currentTarget
        const isScrolledToBottom = scrollTop + clientHeight === scrollHeight

        if (isScrolledToBottom) {
            loadMoreTimetables()
        }
    }

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
                    name='date'
                    render={({field}) => (
                        <Form.Group className='p-0'>
                            <Select
                                onChange={(e) => {
                                    clearTimetables()
                                    field.onChange(e)
                                }}
                                onLoadMore={loadMoreDates}
                                onSearch={searchDates}
                                value={field.value}
                                options={dates}
                                fields='value'
                                label='Дата'
                                variant='filled'
                                clearable={false}
                                error={!!errors.date?.message}
                                helperText={errors.date?.message}
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
                                    clearTimetables()
                                    resetTimetableQuery()
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
                onScroll={handleScroll}
                style={{
                    maxHeight: 'calc(100vh - 72px - 42px - 48px - 48px)',
                    overflow: 'scroll'
                }}
            >
                {timetables.map(timetable => (
                    <Col
                        key={timetable.id}
                        className='d-flex justify-content-center align-items-center my-3'
                    >
                        <TimetableForm timetable={timetable}/>
                    </Col>
                ))}
            </Row>
        </Container>
    )
}