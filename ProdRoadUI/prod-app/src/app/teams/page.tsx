"use client"

import { Box, Card, CardActionArea, CardContent, Container, Typography } from "@mui/material"
import { grey } from "@mui/material/colors"
import { useRouter } from "next/navigation";


//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function Team({
    children,
  }: Readonly<{
    children: React.ReactNode;
  }>){
    const router = useRouter();
    const navTo = (index:Number) =>{
        if (index === 0) {
          router.push('#id');
        }
      }
    return (
    <Box sx={{ width: 180, float: "left", borderRight:1, borderRightColor:grey[300], 
                    borderRightStyle:'solid', marginTop:-1, height:"80vh"}}>
        <CardActionArea sx={{ width:150, height:35}} onClick={()=>navTo(0)}>
            <CardContent sx={{ width:150, height:35, padding:0}}>
                <Card sx={{ width:150, height:35, float: "left"}}>
                    <Typography>
                        TERE
                    </Typography>
                </Card>        
            </CardContent>
        </CardActionArea>
        <Container sx={{float: "left"}}>
            {children}
        </Container>
    </Box>
    )
}