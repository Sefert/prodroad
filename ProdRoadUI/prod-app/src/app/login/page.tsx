"use client"

import { styled } from '@mui/material/styles';
import { FormControl, FormHelperText, Grid, Input, InputLabel, Paper } from "@mui/material"

const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: 'center',
    color: theme.palette.text.secondary,
  }));

export default function Login(){
    return (
        <Grid container spacing={0} marginTop={5}>
            <Grid xs={4}/>
            <Grid xs={4}>
                <Item>
                <FormControl margin="normal">
                    <InputLabel htmlFor="e-mail">Email address</InputLabel>
                    <Input id="mail" aria-describedby="my-helper-text" />
                    <InputLabel htmlFor="my-input">Email address</InputLabel>
                    <Input id="mail" aria-describedby="my-helper-text" />
                    {<FormHelperText id="my-helper-text"></FormHelperText>}
                </FormControl>
                </Item>
            </Grid>
            <Grid xs={4}/>
        </Grid>
    );
}
