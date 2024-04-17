'use client'
import { AppBar, Box, Button, IconButton, Toolbar, Typography } from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';
import React from "react";

//https://mui.com/material-ui/react-menu/
export default function MainNav() {
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

    return (
        <header>
            <Box sx={{ flexGrow: 1 }}>
                <AppBar position="static" sx={{background:'#ba9b0d'}}>
                    <Toolbar>
                        <IconButton
                            size="small"
                            edge="start"
                            aria-label="menu"
                            sx={{ mr: 2, color:"#ffffff"}}//margin-right
                        >
                            <MenuIcon />
                        </IconButton>
                        <Typography variant="h6" component="div" sx={{ flexGrow: 1, color:"#ffffff" }}>
                            News
                        </Typography>
                        <Button sx={{ mr: 2, color:"#ffffff" }}>Login</Button>
                    </Toolbar>
                </AppBar>
            </Box>
        </header>
    );
}
