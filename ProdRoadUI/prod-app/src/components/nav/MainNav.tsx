'use client'
import { AppBar, Box, Button, IconButton, Toolbar, Typography } from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';
import React from "react";

/*<Typography variant="h6" component="div" sx={{ flexGrow: 1, color:"#2D3D4E" }}>
News
</Typography>*/
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
                <AppBar position="static" sx={{background:'#E9F1FA'}}>
                    <Toolbar>
                        <IconButton
                            size="small"
                            edge="start"
                            aria-label="menu"
                            sx={{ mr: 2, color:"#2D3D4E"}}//margin-right
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
