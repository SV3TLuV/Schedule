import {GroupEditor} from "./GroupEditor";
import {lazy} from "react";
import {QueryFilter} from "../../common/enums/QueryFilter";

export default function DeletedGroupEditor() {
    return (
        <GroupEditor filter={QueryFilter.Deleted} />
    )
}

export const LazyDeletedGroupEditor = lazy(() => import("./DeletedGroupEditor"));