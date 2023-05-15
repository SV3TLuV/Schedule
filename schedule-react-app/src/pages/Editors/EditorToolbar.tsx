import {GridToolbarContainer} from "@mui/x-data-grid";
import {Button} from "react-bootstrap";

interface IEditorToolbarProps{
    onCreate: () => void
}

export const EditorToolbar = (
    {
        onCreate
    }: IEditorToolbarProps) => {

    return (
        <GridToolbarContainer>
            <Button variant='outline-primary' onClick={onCreate}>
                Добавить
            </Button>
        </GridToolbarContainer>
    )
}