import { createContext } from "react";

export interface IUserInfo {
    "token": boolean,//string
    "refreshToken": string,
    "firstName": string,
    "lastName": string
}

export interface IUserContext {
    userInfo: IUserInfo | null,
    setUserInfo: (userInfo: IUserInfo | null) => void
}

export interface ISideBarContext {
    sideNav: boolean,
    setSideNav: (sideNav: boolean) => void
}

export interface IAppContext {
    userContext : IUserContext | null,
    navContext : ISideBarContext,
}


export const AppContext = createContext<IAppContext | null>(null);
