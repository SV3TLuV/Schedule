import {GridToolbarContainer} from "@mui/x-data-grid";
import {Button, InputGroup} from "react-bootstrap";
import {ChangeEvent, useState} from "react";
import {HiOutlineSearch, MdOutlineClear} from "react-icons/all";
import {IconButton, InputAdornment, TextField} from "@mui/material";
import {IPaginationQueryWithFilters} from "../../../features/queries/IPaginationQueryWithFilters.ts";

interface IEditorToolbar {
    onCreate?: () => void
    paginationQuery: IPaginationQueryWithFilters
    setPaginationQuery: (query: IPaginationQueryWithFilters) => void;
}

export const EditorToolbar = (
    {
        onCreate,
        paginationQuery,
        setPaginationQuery
    }: IEditorToolbar) => {
    const [text, setText] = useState<string>(() => paginationQuery?.search ?? '')

    const handleInput = (e: ChangeEvent<HTMLInputElement>) => setText(e.target.value)
    const handleSearch = () => setPaginationQuery({...paginationQuery, search: text })
    const handleClear = () =>  setPaginationQuery({...paginationQuery, search: '' })

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