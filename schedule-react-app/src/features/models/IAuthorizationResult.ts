import {IUser} from "./IUser.ts";

export interface IAuthorizationResult {
    accessToken: string
    refreshToken: string
    user: IUser
}