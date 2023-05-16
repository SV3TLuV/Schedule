import {GridToolbarContainer} from "@mui/x-data-grid";
import {Button, InputGroup} from "react-bootstrap";
import {ChangeEvent, Dispatch, SetStateAction, useState} from "react";
import {IPaginatedQueryWithFilters} from "../../features/queries/IPaginatedQueryWithFilters.ts";
import {HiOutlineSearch, MdOutlineClear} from "react-icons/all";
import {IconButton, InputAdornment, TextField} from "@mui/material";

interface IEditorToolbarProps{
    onCreate?: () => void
    paginationQuery: IPaginatedQueryWithFilters
    setPaginationQuery: Dispatch<SetStateAction<IPaginatedQueryWithFilters>>;
}

export const EditorToolbar = (
    {
        onCreate,
        paginationQuery,
        setPaginationQuery
    }: IEditorToolbarProps) => {
    const [text, setText] = useState<string>(paginationQuery?.search ?? '')

    const handleInput = (e: ChangeEvent<HTMLInputElement>) => setText(e.target.value)
    const handleSearch = () => setPaginationQuery(state => ({...state, search: text }))
    const handleClear = () =>  setPaginationQuery(state => ({...state, search: '' }))

    return (
        <GridToolbarContainer style={{ padding: 0 }}>
            <InputGroup>
                <TextField
                    label='Поиск'
                    defaultValue={text}
                    onChange={handleInput}
                    variant='filled'
                    fullWidth
                    InputProps={{
                        endAdornment: (
                            <InputAdornment position='end'>
                                <IconButton onClick={handleClear}>
                                    <MdOutlineClear/>
                                </IconButton>
                                <IconButton onClick={handleSearch}>
                                    <HiOutlineSearch/>
                                </IconButton>
                                {onCreate &&
                                    <Button
                                        variant="outline-primary"
                                        onClick={onCreate}
                                    >
                                        Добавить
                                    </Button>
                                }
                            </InputAdornment>
                        )
                    }}
                />
            </InputGroup>
        </GridToolbarContainer>
    );
}