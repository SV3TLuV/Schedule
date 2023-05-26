import {useCallback, useEffect, useState} from "react";
import {useNavigation} from "./useNavigation.ts";
import {useLocation} from "react-router-dom";

interface IUseTabs {
    defaultKey: string
    baseUrl: string
}

export const useTabs = ({baseUrl, defaultKey}: IUseTabs) => {
    const [key, setKey] = useState(() => defaultKey)
    const {pathname} = useLocation()
    const {navigateTo} = useNavigation()

    const onSelect = useCallback((selected: string | null) => {
        if (selected) {
            setKey(selected)
            navigateTo(`${baseUrl}/${selected}`)
        }
    }, [baseUrl, navigateTo])

    useEffect(() => {
        const url = `${baseUrl}/${key}`

        if (url !== pathname) {
            navigateTo(`${baseUrl}/${defaultKey}`)
        }
        
    }, [pathname, baseUrl, defaultKey, navigateTo, key])

    return {key, onSelect}
}