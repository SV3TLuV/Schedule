import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {Routes} from "../../common/enums/Routes.ts";

interface IRouteState {
    current: Routes,
    isNavShowed: boolean
}

const routesWithoutNav: Routes[] = [
    Routes.LOGIN
]

const routeSlice = createSlice({
    name: 'route',
    initialState: {
        current: Routes.SCHEDULE,
        isNavShowed: true
    } as IRouteState,
    reducers: {
        setCurrent: (state, action: PayloadAction<Routes>) => {
            state.current = action.payload
            state.isNavShowed = routesWithoutNav.some(route =>
                !action.payload.includes(route))
        }
    }
})


export const { setCurrent } = routeSlice.actions
export default routeSlice.reducer