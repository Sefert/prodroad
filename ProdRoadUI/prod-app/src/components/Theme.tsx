'use client';
import { Roboto } from 'next/font/google';
import { createTheme, withTheme } from '@mui/material/styles';
import { blue, green, orange } from '@mui/material/colors';
import { Theme } from '@emotion/react';

const roboto = Roboto({
  weight: ['300', '400', '500', '700', '900'],
  subsets: ['latin'],
  display: 'swap',
});

declare module '@mui/material/styles' {
  // fix the type error when referencing the Theme object in your styled component
  interface Theme {
    drawerWidth?: {
      width?: number;
    };
  }
  // fix the type error when calling `createTheme()` with a custom theme option
  interface ThemeOptions {
    drawerWidth?: {
      width?: number;
    };
  }
}


//https://mui.com/material-ui/customization/default-theme/?expand-path=$.palette.warning
const theme : Theme = createTheme({
  typography: {
    fontFamily: roboto.style.fontFamily,
  },
  palette: {
    primary: {
      main: green[50], 
      contrastText: blue[800]
    },
    secondary: {
        main: green[200],     
    },
    success:{
        main: green[50],  
    },
    tonalOffset: 0.5,
  },
  unstable_sxConfig: {
    borderColor:{
        themeKey:"palette",
    }
  },
},{
  //custom props
  drawerWidth: {
    width: 240,
  },
});

export default theme;