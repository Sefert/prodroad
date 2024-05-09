'use client'
import * as React from 'react';
import { styled, useTheme, Theme, CSSObject } from '@mui/material/styles';
import Box from '@mui/material/Box';
import MuiDrawer from '@mui/material/Drawer';
import { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import List from '@mui/material/List';
import CssBaseline from '@mui/material/CssBaseline';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import { AppContext } from '@/app/state/AppContext';
import GroupsIcon from '@mui/icons-material/Groups';
import InventoryIcon from '@mui/icons-material/Inventory';
import FactoryIcon from '@mui/icons-material/Factory';
import BusinessCenterIcon from '@mui/icons-material/BusinessCenter';
import ReportProblemIcon from '@mui/icons-material/ReportProblem';
import { useRouter } from 'next/navigation';
import Login from '@/app/login/page';

const openedMixin = (theme: Theme): CSSObject => ({
  width: theme.drawerWidth!.width,
  transition: theme.transitions.create('width', {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.enteringScreen,
  }),
  overflowX: 'hidden',
});
const closedMixin = (theme: Theme): CSSObject => ({
  transition: theme.transitions.create('width', {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  overflowX: 'hidden',
  width: `calc(${theme.spacing(7)} + 1px)`,
  [theme.breakpoints.up('sm')]: {
    width: `calc(${theme.spacing(8)} + 1px)`,
  },
});

const DrawerHeader = styled('div')(({ theme }) => ({
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'flex-end',//arrow to flexed left
  padding: theme.spacing(0, 1),
  // necessary for content to be below app bar
  ...theme.mixins.toolbar,
}));

interface AppBarProps extends MuiAppBarProps {
  sideNav?: boolean;
}

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
  ({ theme, open }) => ({
    width: theme.drawerWidth?.width,
    flexShrink: 0,
    whiteSpace: 'nowrap',
    boxSizing: 'border-box',
    ...(open && {
      ...openedMixin(theme),
      '& .MuiDrawer-paper': openedMixin(theme),
    }),
    ...(!open && {
      ...closedMixin(theme),
      '& .MuiDrawer-paper': closedMixin(theme),
    }),
  }),
);

export default function SideNav({
  children,
}: Readonly<{
  children: React.ReactNode;
}>){
  const theme = useTheme();
  const context = React.useContext(AppContext)!;
  const {sideNav, setSideNav} = context.navContext;
  const {userInfo, setUserInfo} = context.userContext!;
  const router = useRouter();
 

  function handleDrawerClose(){ 
    let open1 = sideNav ? false : true;
    setSideNav(open1);
  }
  //https://sentry.io/answers/why-can-t-the-react-js-onclick-event-pass-a-value-to-a-method/
  function navTo(index:Number){
    if (index === 0) {
      router.push('/teams');
    }
  }
   
  if (userInfo != null){
  return (
    <Box sx={{ display: 'flex'}}>
      <CssBaseline />
      
      <Drawer variant="permanent" open={sideNav}>
        <DrawerHeader>
          <IconButton onClick={handleDrawerClose}>
            {theme.direction === 'rtl' ? <ChevronRightIcon /> : <ChevronLeftIcon />}
          </IconButton>
        </DrawerHeader>
        <Divider />
        <List>
          {['Plan','Stock'].map((text, index) => (
            <ListItem key={text} disablePadding sx={{ display: 'block' }}>
              <ListItemIcon
                sx={{
                  minHeight: 48,
                  justifyContent: sideNav ? 'initial' : 'center',
                  px: 2.5,
                }}
              >
                <ListItemIcon
                  sx={{
                    minWidth: 0,
                    mr: sideNav ? 3 : 'auto',
                    justifyContent: 'center',
                  }}
                >
                  {index == 0 && <FactoryIcon/>}
                  {index == 1 && <InventoryIcon/>}
                </ListItemIcon>
                <ListItemText primary={text} sx={{ opacity: sideNav ? 1 : 0 }} />
              </ListItemIcon>
            </ListItem>
          ))}
        </List>
        <Divider />
        <List>
          {['Teams', 'Business', 'Problems'].map((text, index) => (
            <ListItem key={text} disablePadding sx={{ display: 'block' }}>
              <ListItemButton
                sx={{
                  minHeight: 48,
                  justifyContent: sideNav ? 'initial' : 'center',
                  px: 2.5,
                }}
                href=''
                onClick={() => navTo(index)}
              >
                <ListItemIcon
                  sx={{
                    minWidth: 0,
                    mr: sideNav ? 3 : 'auto',
                    justifyContent: 'center',
                  }}
                >
                  {index == 0 && <GroupsIcon/>}
                  {index == 1 && <BusinessCenterIcon/>}
                  {index == 2 && <ReportProblemIcon/>}
                </ListItemIcon>
                <ListItemText primary={text} sx={{ opacity: sideNav ? 1 : 0 }} />
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </Drawer>
      <Box component="main" sx={{ flexGrow: 1, p: 0 }}>
        <DrawerHeader />
        {children}
      </Box>
    </Box>
  );} else if (userInfo == null) {
    return (<Login/>);
  } else {return (<></>);};
}
