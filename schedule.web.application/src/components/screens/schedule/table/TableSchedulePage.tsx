import {Col, Container, Row} from "react-bootstrap";
import {useGetCurrentTimetableQuery} from "../../../../store/apis";
import {usePaginationQuery} from "../../../../hooks";
import {IPaginationQuery} from "../../../../features/queries";
import {IGetCurrentTimetableQuery} from "../../../../features/queries";
import {Loading} from "../../../ui";
import {CurrentTimetableDisplay} from "./CurrentTimetableDisplay.tsx";
import {useParams} from "react-router-dom";
import {chunk} from "../../../../utils/chunk.ts";
import {ICurrentTimetable} from "../../../../features/models";

export const TableSchedulePage = () => {
    const {page} = useParams()

    const [timetableQuery] = usePaginationQuery({
        pageSize: 24,
        page: page ? parseInt(page) : 1
    })
    const {data: timetableData} = useGetCurrentTimetableQuery({
        ...timetableQuery as IPaginationQuery,
        dateCount: 2
    } as IGetCurrentTimetableQuery, {
        pollingInterval: 5000
    })

    if (!timetableData) {
        return <Loading/>
    }

    const timetableChunks: ICurrentTimetable[][] = chunk(timetableData.items, 6)

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
            {timetableChunks.map((timetables, index) => (
                <Row className='h-25' key={index}>
                    {timetables.map(timetable => (
                        <Col
                            key={timetable.groupNames}
                            xs={2}
                            style={{
                                padding: '10px'
                            }}
                        >
                            <CurrentTimetableDisplay
                                timetable={timetable}
                            />
                        </Col>
                    ))}
                </Row>
            ))}
        </Container>
    )
}