"use client"
import { Box, Button, Card, CardActionArea, CardContent, Container, FormControl, Grid, IconButton, InputAdornment, InputLabel, OutlinedInput, Paper, TextField, Typography } from "@mui/material"
import { grey } from "@mui/material/colors"
import { styled } from '@mui/material/styles';

const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'light' ? '#fff' : '#fff',
    ...theme.typography.body2, 
    color: theme.palette.primary.dark,
    padding: theme.spacing(1),
    textAlign: 'center',
  }));
//https://stackoverflow.com/questions/49007357/how-to-make-the-whole-card-component-clickable-in-material-ui-using-react-js
export default function TeamsId(){
    return (
        <Item sx={{ background:'#929cb3', maxWidth:600, minWidth:300, flexShrink:"inherit", mt:5}}>
            <FormControl sx={{padding:1, width: '100%'}} variant="outlined">
            <TextField
                id="outlined-number"
                label="Nimi"
                type="string"
                defaultValue="Hello World"
            />
            </FormControl>
            <FormControl sx={{padding:1,width: '100%' }} variant="outlined">
                <TextField
                    id="outlined-number"
                    label="Nimi"
                    type="string"
                    defaultValue="Hello World"
                />
            </FormControl>
            <FormControl sx={{padding:1,width: '100%' }} variant="outlined">
                <TextField
                    id="outlined-number"
                    label="Nimi"
                    type="string"
                    defaultValue="Hello World"
                />
            </FormControl>
            <Button variant="contained" color="success" onClick={()=>null}>
                MUUDA
            </Button>
        </Item>            
    )
}