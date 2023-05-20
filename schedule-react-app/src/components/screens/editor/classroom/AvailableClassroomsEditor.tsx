import {usePaginationQuery} from "../../../../hooks/usePaginationQuery.ts";
import {QueryFilter} from "../../../../common/enums/QueryFilter.ts";
import {useGetClassroomsQuery} from "../../../../store/apis/classroomApi.ts";
import {ClassroomEditor} from "./ClassroomEditor.tsx";

export const AvailableClassroomsEditor = () => {
    const [paginationQuery, setPaginationQuery] = usePaginationQuery(QueryFilter.Available)
    const {data} = useGetClassroomsQuery(paginationQuery)

    return (
        <ClassroomEditor
            data={data}
            paginationQuery={paginationQuery}
            setPaginationQuery={setPaginationQuery}
        />
    )
}