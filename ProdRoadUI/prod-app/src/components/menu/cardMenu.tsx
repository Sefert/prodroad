"use client"

import { Box, Card, CardActionArea, CardContent, Container, IconButton, Stack, Typography } from "@mui/material"
import { grey } from "@mui/material/colors"
import { useRouter } from "next/navigation";
import AddIcon from '@mui/icons-material/Add';
import { useState } from "react";
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import { IItemCardProp } from "@/types/IItemCardProp";
import {DndContext} from '@dnd-kit/core';
import {Draggable} from '@/components/dnd/Draggable';
import { ContextMenu, ContextMenuItem, ContextMenuTrigger } from "rctx-contextmenu";

//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function CardMenu(id:string){
    return (  
    <ContextMenu id={id}>
        <ContextMenuItem>Edit</ContextMenuItem>
    </ContextMenu>
    );
}