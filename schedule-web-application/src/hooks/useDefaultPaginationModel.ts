import {useState} from "react";
import {IPaginationModel} from "../features/models/IPaginationModel";


export const useDefaultPaginationModel = () => {
    const [paginationModel, setPaginationModel] = useState({
        pageSize: 20,
        page: 0
    } as IPaginationModel)

    return [paginationModel, setPaginationModel] as const;
}