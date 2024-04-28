'use client'
import { Box, Button, CssBaseline, IconButton, Toolbar, styled, useTheme } from "@mui/material";
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import MenuIcon from '@mui/icons-material/Menu';
import React, { useContext } from "react";
import { AppContext } from "@/app/state/AppContext";

/*<Typography variant="h6" component="div" sx={{ flexGrow: 1, color:"#2D3D4E" }}>
News
</Typography>*/
//https://mui.com/material-ui/react-menu/

interface AppBarProps extends MuiAppBarProps {
    open?: boolean;
  }

const AppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== 'open',
  })<AppBarProps>(({ theme, open }) => ({
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    ...(open && {
      marginLeft: theme.drawerWidth!.width,
      width: `calc(100% - ${theme.drawerWidth?.width}px)`,
      transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
      }),
    }),
  }));
  
export default function MainNav() {
    
    const context = React.useContext(AppContext)!;
    const {sideNav, setSideNav} = context.navContext;
    const {userInfo, setUserInfo} = context.userContext!;

    function handleDrawerOpen(){ 
      if (userInfo != null){
        let open = sideNav ? false : true;
        setSideNav(open);
      }
    }

    const logout = () =>{
      setUserInfo(null);    
      //router.push('./');
  }

    return (
        <header>
            <Box sx={{ flexGrow: 1}}>
            <CssBaseline />
                <AppBar position="fixed" sx={{background:'#E9F1FA'}} open={sideNav}>
                    <Toolbar>
                        <IconButton
                            size="small"
                            aria-label="open drawer"
                            onClick={handleDrawerOpen}
                            edge="start"
                            sx={{ mr: 2, 
                                color:"#2D3D4E",
                                marginRight: 5,
                                ...(sideNav && { display: 'none' }),}}//margin-right
                        >
                            <MenuIcon />
                        </IconButton>
                        {userInfo != null  && <Button sx={{ mr: 2, color:"#2D3D4E" }} onClick={logout}>Logout</Button>}
                    </Toolbar>
                </AppBar>
            </Box>
        </header>
    );
}
