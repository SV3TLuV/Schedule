import {IRole} from "./IRole.ts";

export interface IUser {
    id: number
    login: string
    role: IRole
}