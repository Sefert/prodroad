import { createRouter, createWebHistory } from "vue-router";
import LoginView from "@/views/LoginView.vue";
import UserProfileView from "@/views/user/UserProfileView.vue";
import ScheduleView from "@/views/schedule/ScheduleView.vue"
import UserProfileEditView from "@/views/user/UserProfileEditView.vue";
import TeamsView from "@/views/team/TeamsView.vue";
import PublicTeamsView from "@/views/team/PublicTeamsView.vue";
import PersonsTeamsView from "@/views/team/PersonsTeamsView.vue";
import HomeView from "@/views/HomeView.vue";
import { userStore } from "../stores/identity";
import { IdentityService } from "../services/identity/IdentityService";
import jwt_decode from "jwt-decode";
import type { IJWTResponse } from "@/domain/IJWTResponse";
import type { IJWT } from "@/domain/IJWT";
import UserProfileConnectToTeamVue from "@/views/user/UserProfileConnectToTeam.vue";
//import PersonsTeamsViewVue from '@/views/team/PersonsTeamsView.vue';

//import "vite/client"; //needed for meta.env.BASE_URL
//https://stackoverflow.com/questions/66039933/typescript-types-for-import-meta-env
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: "/login", name: "Login", component: LoginView },
    { path: "/home", name: "Home", component: HomeView, alias: "" },

    { path: "/user", name: "Profile", component: UserProfileView },
    {
      path: "/user/edit-profile",
      name: "EditProfile",
      component: UserProfileEditView,
    },
    {
      path: "/user/connect-team",
      name: "ConnectTeam",
      component: UserProfileConnectToTeamVue,
    },

    { path: "/team", name: "Teams", component: TeamsView },
    {
      path: "/team/publicTeam",
      name: "PublicTeams",
      component: PublicTeamsView,
    },
    {
      path: "/team/manageTeam",
      name: "ManageTeams",
      component: PersonsTeamsView,
      props: true,
    },
    {
      path: "/schedule",
      name: "ScheduleView",
      component: ScheduleView,
      props: true,
    },
  ],
});

//TODO: make auth and direction guards better
router.beforeEach(async (to, from) => {
  const identityStore = userStore();
  let refreshToken: string | null = null;
  let jwToken: string | null = null;

  //test app identity state
  if (identityStore.$state.jwt != null) {
    //test app identity jwt expiration state
    const current_time = new Date().getTime() / 1000;

    if (identityStore.$state.jwtExp !== null) {
      if (current_time > identityStore.$state.jwtExp) {
        await refreshUserData();
      }
    } else {
      //TODO: not implemented!
    }
  } else {
    //test identity browser persistance
    refreshToken = window.localStorage.getItem("prodRoad-r");
    jwToken = window.localStorage.getItem("prodRoad-j");

    if (refreshToken != null && jwToken != null) {
      const jwtResp: IJWTResponse = {
        token: jwToken,
        refreshToken: refreshToken,
      };

      identityStore.$state.jwt = jwtResp;

      await refreshUserData();
    }
  }

  console.log(identityStore.$id);
  console.log(identityStore.$state.email);
  console.log(identityStore.$state.role);
  console.log(identityStore.$state.jwtExp);

  if (identityStore.$state.jwt == null && to.name !== "Login")
    return { name: "Login" };
});

async function refreshUserData() {
  const identityStore = userStore();
  const identityService = new IdentityService();

  const result = await identityService.refreshIdentity();

  //TODO: make more secure!!!!
  if (result.status == 200) {
    window.localStorage.setItem("prodRoad-r", identityStore.$state.jwt!.refreshToken!);
    window.localStorage.setItem("prodRoad-j", identityStore.$state.jwt!.token!);

    if (result.data != null) {
      identityStore.$state.jwt = result.data;
      if (result.data.token != null) {
        //https://stackoverflow.com/questions/61199530/typescript-error-with-accessing-jwt-decode-object
        const decoded = jwt_decode<IJWT>(result.data.token);
        identityStore.$id =
          decoded[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
          ];
        identityStore.$state.email =
          decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        identityStore.$state.role =
          decoded[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
          ];
        identityStore.$state.jwtExp = decoded["exp"];
      } else {
        //TODO: not implemented;
      }
    } else {
      //TODO: not implemented!;
    }
  } else {
    window.localStorage.removeItem("prodRoad-r");
    window.localStorage.removeItem("prodRoad-j");

    identityStore.$state.jwt = null;
    identityStore.$id = "";
    identityStore.$state.email = null;
    identityStore.$state.role = [];
    identityStore.$state.jwtExp = null;
  }
}

export default router;
