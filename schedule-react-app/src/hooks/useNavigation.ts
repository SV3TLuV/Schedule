import {useAppDispatch, useTypedSelector} from "./redux.ts";
import {useNavigate} from "react-router-dom";
import {setCurrentPage} from "../store/slices/routeSlice.tsx";

export const useNavigation = () => {
    const {currentPage} = useTypedSelector(state => state.application)
    const dispatch = useAppDispatch()
    const navigate = useNavigate()

    const navigateTo = function (route: string) {
        if (currentPage === route) {
            return;
        }

        navigate(route)
        dispatch(setCurrentPage(route))
    }

    return { navigateTo }
}