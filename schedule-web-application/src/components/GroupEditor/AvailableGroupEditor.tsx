import {GroupEditor} from "./GroupEditor";
import {lazy} from "react";
import {QueryFilter} from "../../common/enums/QueryFilter";


export default function AvailableGroupEditor() {
    return (
        <GroupEditor filter={QueryFilter.Available} />
    )
}

export const LazyAvailableGroupEditor = lazy(() => import("./AvailableGroupEditor"));