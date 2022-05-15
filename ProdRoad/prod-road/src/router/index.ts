import { createRouter, createWebHistory } from 'vue-router';
import LoginView from '@/views/LoginView.vue';
import UserProfileView from '@/views/user/UserProfileView.vue';
import UserProfileEditView from '@/views/user/UserProfileEditView.vue';
import TeamsView from '@/views/team/TeamsView.vue';
import HomeView from '@/views/HomeView.vue';
import { userStore } from "../stores/identity";
import {IdentityService } from "../services/identity/IdentityService";
import jwt_decode from "jwt-decode";
import type { IJWTResponse } from '@/domain/IJWTResponse';
import UserProfileConnectToTeamVue from '@/views/user/UserProfileConnectToTeam.vue';


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {path: "/login", name: "Login", component: LoginView},
    {path: "/home", name: "Home", component: HomeView, alias: ""},

    {path: "/user", name: "Profile", component: UserProfileView},
    {path: "/user/edit-profile", name: "EditProfile", component: UserProfileEditView},
    {path: "/user/connect-team", name: "ConnectTeam", component: UserProfileConnectToTeamVue},

    {path: "/Team", name: "Teams", component: TeamsView},
  ]
})

//TODO: make auth and direction guards better
router.beforeEach(async (to, from) => {
 
  var identityStore = userStore();
  var refreshToken : string | null = null;
  var jwToken : string | null = null;

  //test app identity state
  if (identityStore.$state.jwt != null){

    //test app identity jwt expiration state
    var current_time = new Date().getTime() / 1000;
    if (current_time > identityStore.$state.jwtExp!){
      await refreshUserData();
    }  
  } else {

    //test identity browser persistance
    refreshToken = window.localStorage.getItem("prodRoad-r");
    jwToken = window.localStorage.getItem("prodRoad-j");

    if (refreshToken != null && jwToken != null) {
      var jwtResp : IJWTResponse = {
        token: jwToken,
        refreshToken: refreshToken
      };

      identityStore.$state.jwt = jwtResp;

      await refreshUserData();
    }
  }

  console.log(identityStore.$id);
  console.log(identityStore.$state.email);
  console.log(identityStore.$state.role);
  console.log(identityStore.$state.jwtExp);

  if (identityStore.$state.jwt == null && to.name !== 'Login') return { name: 'Login' }
})

async function refreshUserData() {
  var identityStore = userStore();
  var identityService = new IdentityService();

  var result = await identityService.refreshIdentity();
    
  //TODO: make more secure!!!!
  if (result.status == 200){
    window.localStorage.setItem("prodRoad-r", identityStore.$state.jwt!.refreshToken!);
    window.localStorage.setItem("prodRoad-j", identityStore.$state.jwt!.token!);
    identityStore.$state.jwt = result.data!;
      
    var decoded = jwt_decode(result.data!.token!);

    identityStore.$id = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    identityStore.$state.email = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    identityStore.$state.role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    identityStore.$state.jwtExp = decoded["exp"];

  } else {
    window.localStorage.removeItem("prodRoad-r");
    window.localStorage.removeItem("prodRoad-j");

    identityStore.$state.jwt = null;
    identityStore.$id = '';
    identityStore.$state.email = null;
    identityStore.$state.role = [];
    identityStore.$state.jwtExp = null;
  }
}

export default router


