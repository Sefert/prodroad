"use client"

import { AppContext, ISideBarContext, IUserContext, IUserInfo } from "@/app/state/AppContext";
import { useState } from "react";

export default function AppState({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {

    //const [userContext, setUserContext] = useState<IUserContext | null>(null);
    //const [navContext, setNavContext] = useState<ISideBarContext | null>(null); 
    const [userInfo, setUserInfo] = useState<IUserInfo | null>(null);
    const [sideNav, setSideNav] = useState(false);

    return (
        <AppContext.Provider value={{
            userContext:{userInfo,setUserInfo},
            navContext:{sideNav,setSideNav}
            }}>
            {children}
        </AppContext.Provider>
    );
}
