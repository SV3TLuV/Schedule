import {
    Autocomplete,
    AutocompleteRenderInputParams,
    SxProps,
    TextField,
    TextFieldPropsSizeOverrides, Theme
} from "@mui/material";
import {OverridableStringUnion} from "@mui/types";
import {SyntheticEvent} from "react";

interface ISelect<T extends { id: any }, K extends keyof T> {
    options: T[];
    groupByKey?: K | null;
    fields: K | K[];
    fieldSplitter?: string;
    label: string;
    error?: boolean;
    helperText?: string;
    multiple?: boolean;
    value?: T | T[] | null;
    onChange?: (item: T | T[] | null) => void;
    margin?: 'dense' | 'normal' | 'none';
    size?: OverridableStringUnion<'small' | 'medium', TextFieldPropsSizeOverrides>;
    sx?: SxProps<Theme>;
    disableClearable?: boolean;
}

export const Select = <T extends { id: any }, K extends keyof T>(
    {
        groupByKey,
        options,
        fields,
        fieldSplitter = ', ',
        label,
        error = false,
        helperText = '',
        multiple = false,
        value,
        onChange,
        sx,
        margin = 'normal',
        size = 'small',
        disableClearable = false
    }: ISelect<T, K>) => {

    const handleChange = (_: SyntheticEvent, selected: T | T[] | null) => {
        if (onChange)  {
            onChange(selected)
        }
    }

    const renderInput = (params: AutocompleteRenderInputParams) => (
        <TextField
            {...params}
            fullWidth={true}
            helperText={helperText}
            error={error}
            margin={margin}
            label={label}
            size={size}
        />
    )

    const getOptionLabel = (option: T) =>
        (fields instanceof Array ? fields : [fields])
            .map(field => option[field])
            .filter(value => value)
            .join(fieldSplitter)

    const values = value ?? []
    const groupBy = (option: T) => groupByKey ? option[groupByKey] as string : '';
    const isOptionEqualToValue = (option: T, value: T) => option.id === value.id

    return (
        <Autocomplete
            disableClearable={disableClearable}
            multiple={multiple}
            size={size}
            renderInput={renderInput}
            options={options}
            value={values}
            isOptionEqualToValue={isOptionEqualToValue}
            getOptionLabel={getOptionLabel}
            groupBy={groupBy}
            onChange={handleChange}
            sx={sx}
        />
    )
}