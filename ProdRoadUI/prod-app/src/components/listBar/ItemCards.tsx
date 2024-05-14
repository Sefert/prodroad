"use client"

import Layout from "@/app/layout";
import { Box, Card, CardActionArea, CardContent, Container, IconButton, Stack, Typography } from "@mui/material"
import { grey } from "@mui/material/colors"
import { LayoutRouter } from "next/dist/server/app-render/entry-base";
import { useRouter } from "next/navigation";
import { useLocation } from "react-router";
import AddIcon from '@mui/icons-material/Add';
import DeleteIcon from '@mui/icons-material/Delete';
import { useState } from "react";


//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function ItemCards<T>(
  //elemList:T[],
  {
    elemList,
    children,
  }: Readonly<{
    children: React.ReactNode;
    elemList:Array<T>;
  }>){
    const router = useRouter();
    const [elements, addElem] =useState<Array<T>>(elemList);

    const navTo = (index:Number) =>{
        if (index === 0) {
          router.push('/teams/persons/id');
        }
    }

    const addElems = () =>{
      let data:T = elements[0];
      if (typeof data == 'string'){
        addElem([...elements,'TeamNew2']);
      }
    }
    console.log({elemList}.elemList);
    return (

    <Box component="main" sx={{float: "left", display:"inline"}}>
        <Box sx={{ width: 180, float: "left", borderRight:1, borderRightColor:grey[300], 
                    borderRightStyle:'solid', height:"100vh"}}>                    
          <Stack direction="row" spacing={1} sx={{borderBottom:1, borderBottomColor:grey[300], 
                      borderBottomStyle:'solid'}}>
            <IconButton aria-label="add" onClick={addElems}>
              <AddIcon />
              <Typography>
                ADDTEAM
              </Typography>
            </IconButton>
          </Stack>
          {elements.map((text:T,index:number) => (
           <CardActionArea key={index} sx={{ width:170, height:35,m:0.5}} onClick={()=>navTo(0)}>
                <CardContent sx={{ width:170, height:35,p:0}}>
                    <Card sx={{ width:170, height:35, float: "left",pl:2}}>
                        <Typography>
                            {text}
                        </Typography>
                    </Card>        
                </CardContent>
            </CardActionArea>
          ))}
        </Box>
        <Box sx={{float: "left" , flexGrow: 1, ml:5}}>
          {children}
        </Box>
    </Box>
   )
}