import {useAppDispatch} from "./redux.ts";
import {Routes} from "../common/enums/Routes.ts";
import {useNavigate} from "react-router-dom";
import {setCurrent} from "../redux/slices/routeSlice.tsx";

export const useNavigation = () => {
    const dispatch = useAppDispatch()
    const navigate = useNavigate()

    const navigateTo = function (route: Routes) {
        navigate(route)
        dispatch(setCurrent(route))
    }

    return { navigateTo }
}