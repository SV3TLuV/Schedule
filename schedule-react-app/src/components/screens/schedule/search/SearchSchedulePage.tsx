import {Card, Col, Container, Form, Row} from "react-bootstrap";
import {Select} from "../../../ui";
import {Controller, useForm, useWatch} from "react-hook-form";
import {usePaginationQuery} from "../../../../hooks";
import {useGetGroupsQuery} from "../../../../store/apis";
import {useInfinitySelect} from "../../../../hooks";
import {IGroup} from "../../../../features/models";
import {useGetCurrentTimetableQuery} from "../../../../store/apis";
import {IGetCurrentTimetableQuery} from "../../../../features/queries";
import {LessonDisplay} from "./LessonDisplay.tsx";

interface ISearchSchedulePageState {
    group: IGroup | null
}

export const SearchSchedulePage = () => {
    const {control, formState: {errors}} = useForm<ISearchSchedulePageState>({
        values: {
            group: null
        },
        mode: 'onChange',
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

    const selectedGroup = useWatch({ control, name: 'group' })

    const {data: timetables} = useGetCurrentTimetableQuery({
        groupId: selectedGroup?.id ?? null,
        dateCount: 6
    } as IGetCurrentTimetableQuery, {
        skip: selectedGroup === null,
        pollingInterval: 7500
    })

    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Row>
                <Col
                    className='mx-auto my-4'
                    style={{
                        maxWidth: '420px'
                    }}
                >
                    <Controller
                        control={control}
                        name='group'
                        render={({field}) => (
                            <Form.Group className='p-0'>
                                <Select
                                    onChange={(e) => {
                                        field.onChange(e)
                                    }}
                                    onLoadMore={loadMoreGroups}
                                    onSearch={searchGroups}
                                    value={field.value}
                                    options={groups}
                                    fields='name'
                                    label='Группа'
                                    error={!!errors.group?.message}
                                    helperText={errors.group?.message}
                                />
                            </Form.Group>
                        )}
                    />
                </Col>
            </Row>
            <Row className='d-flex flex-column'>
                {timetables && timetables.items.map(current => (
                    current.dates.map(date => (
                        date.items.map(timetable => (
                            <Card
                                className='mx-auto text-center p-0 mb-4'
                                key={timetable.id}
                                style={{
                                    minWidth: '280px',
                                    maxWidth: '396px',
                                }}
                            >
                                <Card.Header className='text-center'>
                                    <Card.Title>
                                        {`${date.key.day.name} (${date.key.value})`}
                                    </Card.Title>
                                </Card.Header>
                                <Card.Body
                                    style={{
                                        maxHeight: '760px',
                                        overflowX: 'hidden',
                                        overflowY: 'scroll'
                                    }}
                                >
                                    {timetable.lessons.map(lesson => (
                                        <LessonDisplay
                                            key={lesson.id}
                                            lesson={lesson}
                                        />
                                    ))}
                                </Card.Body>
                            </Card>
                        ))
                    ))
                ))}
            </Row>
        </Container>
    )
}

