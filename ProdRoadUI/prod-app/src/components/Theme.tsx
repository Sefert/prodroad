'use client';
import { Roboto } from 'next/font/google';
import { createTheme, withTheme } from '@mui/material/styles';
import { blue, green, orange } from '@mui/material/colors';

const roboto = Roboto({
  weight: ['300', '400', '500', '700', '900'],
  subsets: ['latin'],
  display: 'swap',
});


//https://mui.com/material-ui/customization/default-theme/?expand-path=$.palette.warning
const theme = createTheme({
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
  }
});

export default theme;