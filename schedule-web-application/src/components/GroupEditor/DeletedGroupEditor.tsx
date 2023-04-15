import {GroupEditor} from "./GroupEditor";
import {lazy} from "react";

export default function DeletedGroupEditor() {
    return (
        <GroupEditor filter="deleted" />
    )
}

export const LazyDeletedGroupEditor = lazy(() => import("./DeletedGroupEditor"));