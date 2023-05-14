import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {baseApi} from "@/services/baseApi";
import {persistReducer} from "redux-persist";
import storage from "redux-persist/es/storage";
import persistStore from "redux-persist/es/persistStore";
import {FLUSH, PAUSE, PERSIST, PURGE, REGISTER, REHYDRATE} from "redux-persist/es/constants";

const rootReducer = combineReducers({
    [baseApi.reducerPath]: baseApi.reducer,
});

const persistedReducer = persistReducer({
    key: "schedule-next-root",
    storage,
    whitelist: [],
}, rootReducer);

export const makeStore = () => {
    return configureStore({
        reducer: persistedReducer,
        middleware: getDefaultMiddleware =>
            getDefaultMiddleware({
                serializableCheck: {
                    ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER]
                }
            }).concat(baseApi.middleware),
    });
}

export const store = makeStore()
export const persistor = persistStore(store)
export type AppStore = ReturnType<typeof makeStore>
export type AppState = ReturnType<typeof rootReducer>
export type AppDispatch = ReturnType<AppStore['dispatch']>
