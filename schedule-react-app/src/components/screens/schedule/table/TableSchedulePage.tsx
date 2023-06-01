import {Col, Container, Row} from "react-bootstrap";
import {useGetCurrentTimetableQuery} from "../../../../store/apis/timetableApi.ts";
import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {IPaginationQuery} from "../../../../features/queries/IPaginationQuery.ts";
import {IGetCurrentTimetableQuery} from "../../../../features/queries/IGetCurrentTimetableQuery.ts";
import {Loading} from "../../../ui/Loading.tsx";
import {CurrentTimetableDisplay} from "./CurrentTimetableDisplay.tsx";
import {useParams} from "react-router-dom";

export const TableSchedulePage = () => {
    const {page} = useParams()

    const [timetableQuery] = usePaginationQuery({
        pageSize: 24,
        page: page ? parseInt(page) : 1
    })
    const {data: timetables} = useGetCurrentTimetableQuery({
        ...timetableQuery as IPaginationQuery,
        dateCount: 2
    } as IGetCurrentTimetableQuery, {
        pollingInterval: 5000
    })

    if (!timetables) {
        return <Loading/>
    }

    return (
        <Container
            style={{
                height: '100vh',
                overflow: 'hidden',
                padding: '10px',
                background: 'rgb(34,34,34)',
            }}
            fluid
        >
            <Row className='h-100'>
                {timetables.items.map(timetable => (
                    <Col
                        key={timetable.groupNames}
                        xs={2}
                        style={{
                            height: '25%',
                            padding: '10px'
                        }}
                    >
                        <CurrentTimetableDisplay
                            timetable={timetable}
                        />
                    </Col>
                ))}
            </Row>
        </Container>
    )
}