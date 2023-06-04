import {createSlice, PayloadAction} from "@reduxjs/toolkit";

interface IApplicationState {
    currentPage: string
    isNavShowed: boolean
}

const routesWithoutNav: string[] = [
    '/login',
    '/schedule/table'
]

const applicationSlice = createSlice({
    name: 'application',
    initialState: {
        currentPage: '',
        isNavShowed: true
    } as IApplicationState,
    reducers: {
        setCurrentPage: (state, action: PayloadAction<string>) => {
            state.currentPage = action.payload
            state.isNavShowed = !routesWithoutNav.some(route =>
                action.payload.includes(route))
        }
    }
})


export const { setCurrentPage } = applicationSlice.actions
export default applicationSlice.reducer