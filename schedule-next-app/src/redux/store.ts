import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {baseApi} from "@/services/baseApi";
import {createWrapper} from "next-redux-wrapper";

const rootReducer = combineReducers({
    [baseApi.reducerPath]: baseApi.reducer,
});

export const makeStore = () => {
    return configureStore({
        reducer: rootReducer,
        middleware: getDefaultMiddleware =>
            getDefaultMiddleware()
                .concat(baseApi.middleware),
    });
}

export type AppStore = ReturnType<typeof makeStore>
export type AppState = ReturnType<typeof rootReducer>
export type AppDispatch = ReturnType<AppStore['dispatch']>
export const wrapper = createWrapper<AppStore>(makeStore)