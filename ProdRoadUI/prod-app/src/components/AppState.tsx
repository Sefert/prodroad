"use client"

import { AppContext, ISideBarContext, IUserContext, IUserInfo } from "@/app/state/AppContext";
import { useEffect, useState } from "react";

export default function AppState({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {

    //const [userContext, setUserContext] = useState<IUserContext | null>(null);
    //const [navContext, setNavContext] = useState<ISideBarContext | null>(null); 
    const [userInfo, setUserInfo] = useState<IUserInfo | null>(() => {
        const storedValue = () => {return localStorage.getItem('userContext');}
        if (typeof storedValue === 'string') {
          return JSON.parse(storedValue);
        };
        return null;     
    });

    /*token: true,
            refreshToken: 'aa',
            firstName: 'aa',
            lastName: 'string'*/
    const [sideNav, setSideNav] = useState(false);

    useEffect(() => {
        localStorage.setItem('userContext', JSON.stringify(userInfo));
      }, [userInfo]);

    return (
        <AppContext.Provider value={{
            userContext:{userInfo,setUserInfo},
            navContext:{sideNav,setSideNav}
            }}>
            {children}
        </AppContext.Provider>
    );
}
