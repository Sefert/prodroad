"use client"

import { Box, Card, CardActionArea, CardContent, Container, IconButton, Stack, Typography } from "@mui/material"
import { grey } from "@mui/material/colors"
import { useRouter } from "next/navigation";
import AddIcon from '@mui/icons-material/Add';
import DeleteIcon from '@mui/icons-material/Delete';
import { useState } from "react";
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import { IItemCardProp } from "@/types/IItemCardProp";
import { Key } from "@mui/icons-material";


//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function ItemCards<T>(
  //elemList:T[],
  {
    elemList,
    marginLeft,
    children,
    itemCardProp
  }: Readonly<{
    children: React.ReactNode;
    elemList:Array<T>;
    marginLeft:number,
    itemCardProp:IItemCardProp
  }>){
    const router = useRouter();
    const [elements, addElem] =useState<Array<T>>(elemList);

    const navTo = (index:Number) =>{
      router.push('/teams/persons/'+index);
    }

    const navToUserPath = (path:string) =>{
        router.push(path);
    }

    const addElems = () =>{
      let data:T = elements[0];
      if (typeof data == 'string'){
        console.log(elements.length);
        addElem([...elements,'TeamNew'+(elements.length+1)]);
      }
    }
    console.log({elemList}.elemList);
    return (

    <Box component="main" sx={{float: "left", display:"inline"}}>
        <Box sx={{ width: 180, float: "left", borderRight:1, borderRightColor:grey[300], 
                    borderRightStyle:'solid', height:"100vh"}}>                    
          {itemCardProp.showAddMain == true && 
          <Stack direction="row" spacing={1} sx={{borderBottom:1, borderBottomColor:grey[300], 
                      borderBottomStyle:'solid'}}>
            <IconButton aria-label="add" onClick={addElems}>
              <AddIcon />
              <Typography>
                {itemCardProp.addMain}
              </Typography>
            </IconButton>
          </Stack>}
          {itemCardProp.showAddOther == true && 
          <Stack direction="row" spacing={1} sx={{borderBottom:1, borderBottomColor:grey[300], 
                      borderBottomStyle:'solid'}}>
            <IconButton aria-label="add" onClick={addElems}>
              <PersonAddIcon />
              <Typography>
                {itemCardProp.addOther}
              </Typography>
            </IconButton>
          </Stack>}
          {elements.map((text:T,index:number) => (
           <CardActionArea key={index} sx={{ width:170, height:35,m:0.5}} onClick={()=>navTo(index)}>
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
        <Box sx={{float: "left" , flexGrow: 1, ml:marginLeft}}>
          {children}
        </Box>
    </Box>
   )
}