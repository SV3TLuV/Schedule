import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {IUser} from "../../features/models";

interface IAuthState {
    accessToken: string | null
    refreshToken: string | null
    user: IUser | null
}

const authSlice = createSlice({
    name: 'authorization',
    initialState: {
        accessToken: null,
        refreshToken: null,
        user: null
    } as IAuthState,
    reducers: {
        login: (state, action: PayloadAction<IAuthState>) => {
            state.accessToken = action.payload.accessToken
            state.refreshToken = action.payload.refreshToken
            state.user = action.payload.user
        },
        logout: (state) => {
            state.accessToken = null
            state.refreshToken = null
            state.user = null
        }
    }
})

export const { login, logout } = authSlice.actions
export default authSlice.reducer