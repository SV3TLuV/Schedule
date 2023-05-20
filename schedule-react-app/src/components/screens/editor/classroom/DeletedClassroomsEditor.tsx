import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useGetClassroomsQuery} from "../../../../store/apis/classroomApi.ts";
import {ClassroomEditor} from "./ClassroomEditor.tsx";
import {useState} from "react";
import {IClassroom} from "../../../../features/models/IClassroom.ts";

export const DeletedClassroomsEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Deleted)
    const {data} = useGetClassroomsQuery(paginationQuery)
    const [selected, setSelected] = useState<IClassroom>({} as IClassroom)

    return (
        <ClassroomEditor
            data={data}
            paginationQuery={paginationQuery}
            setPaginationQuery={setPaginationQuery}
        />
    )
}