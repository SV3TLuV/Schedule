import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {Routes} from "../../common/enums/Routes.ts";

interface IRouteState {
    current: Routes | null
}

const routeSlice = createSlice({
    name: 'route',
    initialState: {
        current: null
    } as IRouteState,
    reducers: {
        setCurrent: (state, action: PayloadAction<Routes>) => {
            state.current = action.payload
        }
    }
})


export const { setCurrent } = routeSlice.actions
export default routeSlice.reducer