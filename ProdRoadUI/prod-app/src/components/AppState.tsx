"use client"

import { AppContext, ISideBarContext, IUserContext, IUserInfo } from "@/app/state/AppContext";
import { useRouter } from "next/navigation";
import { useEffect, useReducer, useState, useSyncExternalStore } from "react";
import { useLocation } from "react-router";

function getUserContext(){
    const storedValue = localStorage.getItem('userContext');
    return typeof storedValue !== 'undefined' && storedValue !== null ? JSON.parse(storedValue) : null;
}

function useLocalStorageState(key:string, defaultValue:string|null) {
    // Subscribe to changes to the localStorage key
    const subscribe = (callback:any) => {
      const listener = (se:StorageEvent) => {
        if (se.storageArea === localStorage && se.key === key) {
          callback();
        }
      };
      window.addEventListener('storage', listener);
      return () => window.removeEventListener('storage', listener);
    };
  
    // Get the current state from localStorage
    const getSnapshot = () => {
      const item = localStorage.getItem(key);
      return item ? JSON.parse(item) : defaultValue;
    };
  
    // Synchronize state to the localStorage key
    return useSyncExternalStore(subscribe, getSnapshot);
  }

export default function AppState({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    const router = useRouter();
    //https://github.com/vercel/next.js/discussions/19911  typeof window !== "undefined" ? localStorage.getItem('userContext') : null
    const [userInfo, setUserInfo] = useReducer((prev:IUserInfo|null, cur:IUserInfo|null) => {
        localStorage.setItem('userContext', JSON.stringify(cur));
        return cur;
      }, typeof localStorage.getItem('userContext') !== 'undefined' && localStorage.getItem('userContext') !== null ? JSON.parse(localStorage!.getItem('userContext')!) : null
    );
    
    /*useLocalStorageState('userContext', null);*/
    /*useState<IUserInfo | null>( /*() => {
       const storedValue = localStorage.getItem('userContext');
        if (typeof storedValue === 'string' && storedValue !== 'null') {
          return JSON.parse(storedValue);
        } else {
        return null;    
        }
    }*//*);*/
    

    const [sideNav, setSideNav] = useState(false);
    //const [sideNav, setSideNav] = useState(false);

    // Subscribe to changes to the localStorage key
    /*const subscribe = (callback) => {
        const listener = (e) => {
        if (e.storageArea === localStorage && e.key === 'userContext') {
            callback();
        }
        };
        window.addEventListener('storage', listener);
        return () => window.removeEventListener('storage', listener);
    };    useSyncExternalStore(subscribe,getUserContext());*/

    //https://julesblom.com/writing/usesyncexternalstore


    /*useEffect(() => {
        const storedValue = localStorage.getItem('userContext');        
        setUserInfo(typeof storedValue !== 'undefined' && storedValue !== null ? JSON.parse(storedValue) : null);    
    }, []);*/

    useEffect(() => {
        if(typeof userInfo !== 'undefined' && userInfo === null){
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
