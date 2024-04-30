"use client"

import { AppContext, ISideBarContext, IUserContext, IUserInfo } from "@/app/state/AppContext";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

export default function AppState({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    const router = useRouter();
    //https://github.com/vercel/next.js/discussions/19911
    const [userInfo, setUserInfo] = useState<IUserInfo | null>(() => {
        const storedValue = typeof window !== "undefined" ? localStorage.getItem('userContext') : null; 
        if (typeof storedValue === 'string') {
          return JSON.parse(storedValue);
        };
        return null;     
    });
    

    const [sideNav, setSideNav] = useState(false);

    useEffect(() => {
        if (typeof window !== 'undefined') {
            localStorage.setItem('userContext', JSON.stringify(userInfo));
            if (userInfo === null){
                router.push('/login');
            }
        }
      }, [router, userInfo]);

    return (
        <AppContext.Provider value={{
            userContext:{userInfo,setUserInfo},
            navContext:{sideNav,setSideNav}
            }}>
            {children}
        </AppContext.Provider>
    );
}
