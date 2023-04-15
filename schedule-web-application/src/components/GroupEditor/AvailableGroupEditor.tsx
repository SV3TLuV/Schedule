import {GroupEditor} from "./GroupEditor";
import {lazy} from "react";


export default function AvailableGroupEditor() {
    return (
        <GroupEditor filter="available" />
    )
}

export const LazyAvailableGroupEditor = lazy(() => import("./AvailableGroupEditor"));