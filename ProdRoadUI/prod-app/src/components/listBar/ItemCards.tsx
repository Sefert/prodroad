"use client"

import Layout from "@/app/layout";
import { Box, Card, CardActionArea, CardContent, Container, Typography } from "@mui/material"
import { grey } from "@mui/material/colors"
import { LayoutRouter } from "next/dist/server/app-render/entry-base";
import { useRouter } from "next/navigation";
import { useLocation } from "react-router";


//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function ItemCards({
    children,
  }: Readonly<{
    children: React.ReactNode;
  }>){
    const router = useRouter();

    const navTo = (index:Number) =>{
        if (index === 0) {
          router.push('/teams/persons/id');
        }
      }
    return (
    <Box component="main" sx={{ width: 180, float: "left",  position:"inline" , borderRight:1, borderRightColor:grey[300], 
                    borderRightStyle:'solid', marginTop:-1, height:"80vh"}}>
        <CardActionArea sx={{ width:150, height:35}} onClick={()=>navTo(0)}>
            <CardContent sx={{ width:150, height:35, padding:0}}>
                <Card sx={{ width:150, height:35, float: "left"}}>
                    <Typography>
                        PERSONSLIST
                    </Typography>
                </Card>        
            </CardContent>
        </CardActionArea>
        <Box sx={{float: "left", marginLeft:25}}>
          {children}
        </Box>
    </Box>
    )
}