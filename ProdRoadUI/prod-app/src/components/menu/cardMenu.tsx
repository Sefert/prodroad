"use client"

import { ContextMenu, ContextMenuItem, ContextMenuTrigger } from "rctx-contextmenu";

//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function CardMenu(id:{id:string}){
    console.log(id.id);
    return (  
    <ContextMenu id={id.id}>
        <ContextMenuItem onClick={() => console.log("I'm clicked!"+id.id)}>Edit</ContextMenuItem>
    </ContextMenu>
    );
}