import {Col, Container, Form, Row} from "react-bootstrap";
import {Select} from "../../../ui/Select.tsx";
import {Controller, useForm} from "react-hook-form";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {useGetGroupsQuery} from "../../../../store/apis/groupApi.ts";
import {useInfinitySelect} from "../../../../hooks/useInfinitySelect.ts";
import {IGroup} from "../../../../features/models/IGroup.ts";

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

/*    const selectedGroup = useWatch({ control, name: 'group' })

    const {data: timetableData} = useGetCurrentTimetableQuery({
        groupId: selectedGroup?.id ?? null,
        dateCount: 3
    })*/

    return (
        <Container style={{ height: 'calc(100vh - 72px)' }}>
            <Row>
                <Col className='mx-auto my-4' xs={12} sm={8} md={6} lg={5} xl={4}>
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
            <Row>

            </Row>
        </Container>
    )
}