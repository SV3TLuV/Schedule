import {Dispatch, SetStateAction, useCallback, useEffect, useState} from "react";
import {IPagedList} from "../features/models";
import {IPaginationQueryWithFilters} from "../features/queries";

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
    const [options, setOptions] = useState<T[]>(() => [])

    useEffect(() => {
        if (data?.items) {
            setOptions(prev => {
                const resultMap = new Map<number, T>();
                prev.forEach(item => resultMap.set(item.id, item));
                data.items.forEach(item => resultMap.set(item.id, item));
                return Array.from(resultMap.values())
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