"use client"

import { styled } from '@mui/material/styles';
import { Button, FormControl, FormHelperText, Grid, IconButton, Input, InputAdornment, InputLabel, OutlinedInput, Paper } from "@mui/material"
import React from 'react';
import { Visibility, VisibilityOff } from '@mui/icons-material';

const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'light' ? '#fff' : '#fff',
    ...theme.typography.body2, 
    color: theme.palette.primary.dark,
    padding: theme.spacing(1),
    textAlign: 'center',
  }));

export default function Login(){
    const [showPassword, setShowPassword] = React.useState(false);
    const [signUp, setSignUp] = React.useState(false);

    const handleClickShowPassword = () => setShowPassword((show) => !show);
    const handleClickSignUp = () => setSignUp((show) => !show);

    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    };
    //https://blog.hubspot.com/website/center-div-css
    return (
        <Grid container paddingTop={5}>
            <Grid xs={3}/>
            <Grid xs={6}>
                <Item sx={{ background:'#929cb3', maxWidth:500, minWidth:350,position:"absolute",left: '50%', transform: 'translate(-50%, 50%)'}}>
                    <FormControl sx={{padding:1, width: '100%'}} variant="outlined">
                        <InputLabel htmlFor="outlined-adornment-password" sx={{color:"#ffffff",borderColor:"#ffffff"}}>E-mail</InputLabel>
                        <OutlinedInput                   
                        id="outlined-adornment-password"
                        type={showPassword ? 'text' : 'password'}
                        label="Password"
                        />
                    </FormControl>
                    <FormControl sx={{padding:1,width: '100%' }} variant="outlined">
                        <InputLabel htmlFor="outlined-adornment-password" sx={{color:"#ffffff"}}>Password</InputLabel>
                        <OutlinedInput
                        id="outlined-adornment-password"
                        type={showPassword ? 'text' : 'password'}
                        endAdornment={
                            <InputAdornment position="end">
                                <IconButton
                                aria-label="toggle password visibility"
                                onClick={handleClickShowPassword}
                                onMouseDown={handleMouseDownPassword}
                                edge="end"
                                sx={{color:"#ffffff"}}
                                >
                                {showPassword ? <VisibilityOff /> : <Visibility />}
                                </IconButton>
                            </InputAdornment>
                        }
                        label="Password"
                        />
                    </FormControl>
                    <FormControl sx={{padding:1,width: '100%' }} variant="outlined">
                        <InputLabel htmlFor="outlined-adornment-password" sx={{color:"#ffffff"}}>Confirm Password</InputLabel>
                        <OutlinedInput
                        id="outlined-adornment-password"
                        type={showPassword ? 'text' : 'password'}
                        endAdornment={
                            <InputAdornment position="end">
                                <IconButton
                                aria-label="toggle password visibility"
                                onClick={handleClickShowPassword}
                                onMouseDown={handleMouseDownPassword}
                                edge="end"
                                sx={{color:"#ffffff"}}
                                >
                                {showPassword ? <VisibilityOff /> : <Visibility />}
                                </IconButton>
                            </InputAdornment>
                        }
                        label="Password"
                        />
                    </FormControl>
                    <Button variant="contained" color="success">
                        LOGIN
                    </Button>
                    <p>
                        Don&apos;t have account?
                        <Button variant="text">
                            SIGNUP
                        </Button>
                    </p>
                </Item>
            </Grid>
            <Grid xs={3}/>
        </Grid>
    );
}
