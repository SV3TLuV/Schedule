import {Dispatch, SetStateAction, useCallback, useEffect, useState} from "react";
import {IPagedList} from "../features/models/IPagedList.ts";
import {IPaginationQueryWithFilters} from "../features/queries/IPaginationQueryWithFilters.ts";

interface useInfinitySelect<T> {
    data?: IPagedList<T>
    query: IPaginationQueryWithFilters
    setQuery: Dispatch<SetStateAction<IPaginationQueryWithFilters>>
}


export const useInfinitySelect = <T extends { id: number }>(
    {
        query,
        setQuery,
        data,
    }: useInfinitySelect<T>) => {
    const [options, setOptions] = useState<T[]>([])

    useEffect(() => {
        if (data?.items) {
            setOptions(prev => {
                const array = [...prev, ...data.items];

                const uniqueArray = [...new Set(array.map(item => item.id))];

                return uniqueArray
                    .map(id => array.find(item => item.id === id))
                    .filter(item => item) as T[];
            });
        }
    }, [data?.items])

    const loadMore = useCallback(() => {
        const hasMore = query.page < (data?.totalPages ?? 1)

        if (hasMore) {
            setQuery(prev => ({...prev, page: prev.page + 1}))
        }
    }, [data?.totalPages, query.page, setQuery])

    const search = useCallback((value: string) => {
        setQuery(prev => ({...prev, search: value}))
    }, [setQuery])

    const clear = useCallback(() => {
        setOptions([])
    }, [setOptions])

    return {options, loadMore, search, clear}
}