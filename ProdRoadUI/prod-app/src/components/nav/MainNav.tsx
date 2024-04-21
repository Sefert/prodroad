'use client'
import { AppBar, AppBarProps, Box, Button, IconButton, Toolbar, Typography, styled } from "@mui/material";
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import MenuIcon from '@mui/icons-material/Menu';
import React from "react";

/*<Typography variant="h6" component="div" sx={{ flexGrow: 1, color:"#2D3D4E" }}>
News
</Typography>*/
//https://mui.com/material-ui/react-menu/

  
export default function MainNav() {
    const [open, setOpen] = React.useState(false);
    const handleDrawerOpen = () => {
        setOpen(true);
    };

    return (
        <header>
            <Box sx={{ flexGrow: 1}}>
                <AppBar position="fixed" sx={{background:'#E9F1FA'}}>
                    <Toolbar>
                        <IconButton
                            size="small"
                            aria-label="open drawer"
                            onClick={handleDrawerOpen}
                            edge="start"
                            sx={{ mr: 2, 
                                color:"#2D3D4E",
                                marginRight: 5,
                                ...(open && { display: 'none' }),}}//margin-right
                        >
                            <MenuIcon />
                        </IconButton>
                        <Button sx={{ mr: 2, color:"#2D3D4E" }}>Login</Button>
                    </Toolbar>
                </AppBar>
            </Box>
        </header>
    );
}
