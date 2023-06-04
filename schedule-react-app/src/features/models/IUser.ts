import {IRole} from "./IRole.ts";

export interface IUser {
    id: number
    login: string
    password: string
    role: IRole
}