"use client"

import Layout from "@/app/layout";
import { Box, Card, CardActionArea, CardContent, Container, IconButton, Stack, Typography } from "@mui/material"
import { grey } from "@mui/material/colors"
import { LayoutRouter } from "next/dist/server/app-render/entry-base";
import { useRouter } from "next/navigation";
import { useLocation } from "react-router";
import AddIcon from '@mui/icons-material/Add';


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
                    borderRightStyle:'solid', height:"80vh"}}>
        <Stack direction="row" spacing={1} sx={{borderBottom:1, borderBottomColor:grey[300], 
                    borderBottomStyle:'solid'}}>
          <IconButton aria-label="add">
            <AddIcon />
            <Typography>
              ADDTEAM
            </Typography>
          </IconButton>
        </Stack>
        <CardActionArea sx={{ width:150, height:35}} onClick={()=>navTo(0)}>
            <CardContent sx={{ width:150, height:35, padding:0}}>
                <Card sx={{ width:150, height:35, float: "left"}}>
                    <Typography>
                        TEAMSLIST
                    </Typography>
                </Card>        
            </CardContent>
        </CardActionArea>
        <Box component="main"sx={{float: "left" , flexGrow: 1, pl: 25, my:-4}}>
          {children}
        </Box>
    </Box>
    )
}