"use client"
import { Box, Card, CardActionArea, CardContent, Container, Typography } from "@mui/material"
import { grey } from "@mui/material/colors"

//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function TeamsId(){
    return (
        <Box sx={{ width: 180, float: "right", borderRight:1, borderRightColor:grey[300], 
                borderRightStyle:'solid', marginTop:-1, height:"80vh"}}>
            <CardActionArea sx={{ width:150, height:35}}>
            <CardContent sx={{ width:150, height:35, padding:0}}>
                <Card sx={{ width:150, height:35, float: "left"}}>
                    <Typography>
                        TERE2
                    </Typography>
                </Card>        
            </CardContent>
            </CardActionArea>
        </Box>
    )
}