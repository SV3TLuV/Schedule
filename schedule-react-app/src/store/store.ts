import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {persistReducer} from "redux-persist";
import storage from "redux-persist/es/storage";
import persistStore from "redux-persist/es/persistStore";
import {FLUSH, PAUSE, PERSIST, PURGE, REGISTER, REHYDRATE} from "redux-persist/es/constants";
import {baseApi} from "./apis";
import applicationReducer from "./slices/applicationSlice.ts";
import authSlice from "./slices/authSlice.ts";

const rootReducer = combineReducers({
    [baseApi.reducerPath]: baseApi.reducer,
    application: applicationReducer,
    auth: authSlice
});

const persistedReducer = persistReducer({
    key: 'root',
    storage,
    whitelist: ['auth'],
}, rootReducer);

export const setupStore = () => {
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

export const store = setupStore()
export const persistor = persistStore(store)
export type AppStore = ReturnType<typeof setupStore>
export type AppState = ReturnType<typeof rootReducer>
export type AppDispatch = AppStore['dispatch']