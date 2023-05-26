import {useAppDispatch, useTypedSelector} from "./redux.ts";
import {useNavigate} from "react-router-dom";
import {setCurrentPage} from "../store/slices/routeSlice.tsx";
import {useCallback} from "react";

export const useNavigation = () => {
    const {currentPage} = useTypedSelector(state => state.application)
    const dispatch = useAppDispatch()
    const navigate = useNavigate()

    const navigateTo = useCallback(function (route: string) {
        if (currentPage === route) {
            return;
        }

        navigate(route)
        dispatch(setCurrentPage(route))
    }, [currentPage, dispatch, navigate])

    return { navigateTo }
}