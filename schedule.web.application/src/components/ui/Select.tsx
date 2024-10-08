import {
    Autocomplete,
    AutocompleteRenderInputParams,
    Box,
    SxProps,
    TextField,
    TextFieldPropsSizeOverrides,
    TextFieldVariants,
    Theme
} from "@mui/material";
import {OverridableStringUnion} from "@mui/types";
import {memo, SyntheticEvent, useCallback, useMemo} from "react";
import {isEmpty} from "../../utils/isEmpty.ts";

interface ISelect<T extends { id: any }, K extends keyof T> {
    options: T[],
    groupByKey?: K | null,
    renderValue: (item: T) => string,
    label: string,
    error?: boolean,
    helperText?: string,
    multiple?: boolean,
    value?: T | T[] | null,
    onChange?: (item: T | T[] | null) => void,
    onLoadMore?: () => void,
    onSearch?: (value: string) => void,
    size?: OverridableStringUnion<'small' | 'medium', TextFieldPropsSizeOverrides>,
    sx?: SxProps<Theme>,
    variant?: TextFieldVariants
    clearable?: boolean,
}

export const Select = memo<ISelect<any, any>>(<T extends { id: any }, K extends keyof T>(
    {
        groupByKey,
        options,
        renderValue,
        label,
        error = false,
        helperText = '',
        multiple = false,
        value,
        variant,
        onLoadMore,
        onSearch,
        onChange,
        sx,
        size = 'small',
        clearable = true,
    }: ISelect<T, K>) => {

    const values = useMemo(() => {
        return value ?? []
    }, [value])

    const handleChange = useCallback((_: SyntheticEvent, selected: T | T[] | null) => {
        if (onChange)  {
            onChange(selected)
        }
    }, [onChange])

    const renderInput = useCallback((params: AutocompleteRenderInputParams) => (
        <TextField
            {...params}
            variant={variant}
            fullWidth={true}
            helperText={helperText}
            error={error}
            label={label}
            size={size}
        />
    ), [error, helperText, label, size, variant])

    const getOptionLabel = useCallback((option: T): string => {
        return !isEmpty(option) ? renderValue(option) ?? "" : ""
    }, [renderValue])
    
    const groupBy = useCallback((option: T) => {
        return groupByKey ? option[groupByKey] as string : ''
    }, [groupByKey])
    
    const isOptionEqualToValue = useCallback((option: T, value: T) => {
        if (typeof option !== typeof value)
            return false
        return option.id === value.id
    }, [])

    const handleScroll = useCallback((event: React.UIEvent<HTMLUListElement>) => {
        const { scrollTop, clientHeight, scrollHeight } = event.currentTarget
        const isScrolledToBottom = scrollTop + clientHeight === scrollHeight

        if (value && !isEmpty(value) && onSearch) {
            onSearch('')
        }

        if (isScrolledToBottom && onLoadMore) {
            onLoadMore()
        }
    }, [onLoadMore, onSearch, value])

    const handleSearch = useCallback((_: React.SyntheticEvent, searchValue: string) => {
        if (onSearch) {
            onSearch(value && !isEmpty(value) && !multiple ? '' : searchValue)
        }
    }, [multiple, onSearch, value])

    return (
        <Autocomplete
            disableClearable={!clearable}
            multiple={multiple}
            size={size}
            renderInput={renderInput}
            renderOption={(props, option) => (
                <Box component="li" {...props} key={option.id}>
                    {getOptionLabel(option)}
                </Box>
            )}
            options={options}
            value={values}
            isOptionEqualToValue={isOptionEqualToValue}
            getOptionLabel={getOptionLabel}
            groupBy={groupBy}
            onChange={handleChange}
            sx={sx}
            onInputChange={handleSearch}
            ListboxProps={{ onScroll: handleScroll }}
        />
    )
})