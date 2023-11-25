import {GridToolbarContainer} from "@mui/x-data-grid";
import {Button, InputGroup} from "react-bootstrap";
import {ChangeEvent, useState} from "react";
import {IconButton, InputAdornment, TextField} from "@mui/material";
import {IPaginationQueryWithFilters} from "../../../features/queries";
import {HiOutlineSearch} from "react-icons/hi";
import {MdOutlineClear} from "react-icons/md";
import {PiExportBold} from "react-icons/pi";
import {BiImport} from "react-icons/bi";

interface IEditorToolbar {
    onImport?: () => void
    onExport?: () => void
    onCreate?: () => void
    paginationQuery: IPaginationQueryWithFilters
    setPaginationQuery: (query: IPaginationQueryWithFilters) => void;
}

export const EditorToolbar = (
    {
        onImport,
        onExport,
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
                    onKeyDown={(event) => {
                        if (event.key === "Enter") {
                            handleSearch()
                        }
                    }}
                    InputProps={{
                        endAdornment: (
                            <InputAdornment position='end'>
                                <IconButton onClick={handleClear}>
                                    <MdOutlineClear/>
                                </IconButton>
                                <IconButton onClick={handleSearch}>
                                    <HiOutlineSearch/>
                                </IconButton>
                                {onExport &&
                                    <IconButton onClick={onExport}>
                                        <PiExportBold />
                                    </IconButton>
                                }
                                {onImport &&
                                    <IconButton onClick={onImport}>
                                        <BiImport />
                                    </IconButton>
                                }
                                {onCreate &&
                                    <Button
                                        variant="outline-primary"
                                        onClick={onCreate}
                                        style={{
                                            marginLeft: '5px'
                                        }}
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