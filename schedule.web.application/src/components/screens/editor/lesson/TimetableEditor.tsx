import {usePaginationQuery, useTypedSelector} from "../../../../hooks";
import {useGetCurrentDateQuery, useGetDatesQuery} from "../../../../store/apis";
import {useInfinitySelect} from "../../../../hooks";
import {IDate} from "../../../../features/models";
import {Button, Col, Container, Form, Row} from "react-bootstrap";
import {Select} from "../../../ui";
import {Controller, useForm, useWatch} from "react-hook-form";
import {IGroup} from "../../../../features/models";
import {useGetGroupsQuery} from "../../../../store/apis";
import {ITimetable} from "../../../../features/models";
import {useGetTimetablesQuery} from "../../../../store/apis";
import {TimetableForm} from "./forms";
import {IPagedList} from "../../../../features/models";
import {useDialog} from "../../../../hooks";
import {UpdateLessonTimeDialog} from "./dialogs";
import {downloadTimetableReport} from "../../../../store/apis/reportApi.ts";

interface ITimetableEditorState {
    group: IGroup
    date: IDate
}

export const TimetableEditor = () => {
    const { accessToken } = useTypedSelector(state => state.auth)

    const {data: currentDate} = useGetCurrentDateQuery()

    const {control, formState: {errors}} = useForm<ITimetableEditorState>({
        values: {
            date: currentDate ?? {} as IDate,
            group: {} as IGroup
        },
        mode: 'onChange',
    })

    const [dateQuery, setDateQuery] = usePaginationQuery()
    const {data: dateData} = useGetDatesQuery({ ...dateQuery, educationalOnly: true })
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

    const [timetableQuery, setTimetableQuery] = usePaginationQuery({ pageSize: 24 })
    const {data: timetableData } = useGetTimetablesQuery({
        page: timetableQuery.page,
        pageSize: timetableQuery.pageSize,
        dateId: selectedDate.id,
        groupId: selectedGroup?.id,
    }, {
        skip: !selectedDate?.id,
        refetchOnMountOrArgChange: true
    })
    const {
        options: timetables,
        loadMore: loadMoreTimetables,
        clear: clearTimetables
    } = useInfinitySelect<ITimetable>({
        query: timetableQuery,
        setQuery: setTimetableQuery,
        data: timetableData ?? {} as IPagedList<ITimetable>,
    })

    const updateTimeDialog = useDialog()

    const resetTimetableQuery = () => setTimetableQuery(prev => ({...prev, page: 1}))

    const handleScroll = (event: React.UIEvent<HTMLUListElement>) => {
        const { scrollTop, clientHeight, scrollHeight } = event.currentTarget
        const isScrolledToBottom = scrollTop + clientHeight === scrollHeight

        if (isScrolledToBottom) {
            loadMoreTimetables()
        }
    }

    const handleSave = async () => {
        if (selectedDate && accessToken) {
            await downloadTimetableReport(selectedDate.id, selectedDate.id, accessToken)
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
                                renderValue={(item) => item.value}
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
                                renderValue={(item) => item.name}
                                label='Группа'
                                variant='filled'
                                error={!!errors.group?.message}
                                helperText={errors.group?.message}
                            />
                        </Form.Group>
                    )}
                />
                <Form.Group className='text-center my-2'>
                    <Button
                        className='d-inline-block mx-2 my-2'
                        style={{ width: '160px' }}
                        onClick={updateTimeDialog.show}
                    >
                        Изменить время
                    </Button>
                    <Button
                        className='d-inline-block mx-2 my-2'
                        style={{ width: '160px' }}
                        onClick={handleSave}
                    >
                        Сохранить в Excel
                    </Button>
                </Form.Group>
            </Row>
            <Row
                style={{
                    maxHeight: 'calc(100vh - 72px - 42px - 48px - 48px)',
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
            <UpdateLessonTimeDialog
                {...updateTimeDialog}
                dateId={selectedDate.id}
            />
        </Container>
    )
}