'use client'
import Image from "next/image";
import styles from "./page.module.css";
import { useRouter } from "next/navigation";
import React, { useEffect } from "react";
import { AppContext } from "./state/AppContext";


export default function Home() {
  const router = useRouter();
  const context = React.useContext(AppContext)!;
  //const {sideNav, setSideNav} = context.navContext;
  const {userInfo, setUserInfo} = context.userContext!;

  useEffect(() =>
    {if (userInfo == null){
      console.log(userInfo);
      router.push('./login');
    }});
  return (
    <></>
  );
}
