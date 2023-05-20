import {TypedUseSelectorHook, useDispatch, useSelector} from "react-redux";
import {AppDispatch, AppState} from "../store/store.ts";

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useTypedSelector: TypedUseSelectorHook<AppState> = useSelector;