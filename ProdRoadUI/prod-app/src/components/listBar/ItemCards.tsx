"use client"

import Layout from "@/app/layout";
import { Box, Card, CardActionArea, CardContent, Container, IconButton, Stack, Typography } from "@mui/material"
import { grey } from "@mui/material/colors"
import { LayoutRouter } from "next/dist/server/app-render/entry-base";
import { useRouter } from "next/navigation";
import { useLocation } from "react-router";
import AddIcon from '@mui/icons-material/Add';
import { useState } from "react";


//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function ItemCards({
    children,
  }: Readonly<{
    children: React.ReactNode;
  }>){
    const router = useRouter();
    const [teamsList, addTeam] = useState(['TEAM1','TEAM2']);

    const navTo = (index:Number) =>{
        if (index === 0) {
          router.push('/teams/persons/id');
        }
      }
    return (

    <Box component="main" sx={{ width: 180, float: "left",  position:"inline" , borderRight:1, borderRightColor:grey[300], 
                    borderRightStyle:'solid', height:"100vh"}}>
        <Stack direction="row" spacing={1} sx={{borderBottom:1, borderBottomColor:grey[300], 
                    borderBottomStyle:'solid'}}>
          <IconButton aria-label="add" onClick={()=>(addTeam([...teamsList,'TeamNew']))}>
            <AddIcon />
            <Typography>
              ADDTEAM
            </Typography>
          </IconButton>
        </Stack>
        {teamsList.map((text:string,index:number) => (
          <CardActionArea sx={{ width:170, height:35,m:0.5}} onClick={()=>navTo(0)}>
              <CardContent sx={{ width:170, height:35,p:0}}>
                  <Card sx={{ width:170, height:35, float: "left",pl:2}}>
                      <Typography>
                          {text}
                      </Typography>
                  </Card>        
              </CardContent>
          </CardActionArea>
        ))}
        <Box component="main"sx={{float: "left" , flexGrow: 1, pl: 25, my:-4}}>
          {children}
        </Box>
    </Box>
    )
}