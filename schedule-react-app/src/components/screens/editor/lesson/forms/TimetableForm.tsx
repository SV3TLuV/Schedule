import {ITimetable} from "../../../../../features/models/ITimetable.ts";
import {Card} from "react-bootstrap";
import {IGroup} from "../../../../../features/models/IGroup.ts";

interface ITimetableForm {
    timetable: ITimetable
}

function joinNames(groups: IGroup[]) {
    return groups.map(g => g.name).join(' ')
}

export const TimetableForm = ({timetable}: ITimetableForm) => {
    return (
        <Card
            style={{ minWidth: '240px'}}
            className='text-center'
        >
            <Card.Header>
                <Card.Title>
                    {joinNames(timetable.groups)}
                </Card.Title>
            </Card.Header>
            <Card.Body>
                <Card.Text>
                    {timetable.date.id}
                </Card.Text>
            </Card.Body>
        </Card>
    )
}