<script lang="ts">
import type { IUserTeam } from "@/domain/IUserTeam";
import { IdentityService } from "@/services/identity/IdentityService";
import { ref, type PropType } from "vue";
import { userStore } from "../stores/identity";
import LangChange from "./LangChange.vue";
import { teamStore } from "@/stores/team";
import type { ITeam } from "@/domain/ITeam";

export default {
  components: {
    LangChange,
  },

  props: {
    publicTeam: {
      type: null as unknown as PropType<string | null>,
      default: null,
      required: true,
    },
    /*errorMsg: {
      type: null as unknown as PropType<string | null>,
      default: null,
      required: true,
    },*/
    editTeamId: {
      type: null as unknown as PropType<string | null>,
      default: null,
      required: true,
    },
    userTeams: { type: Array as () => IUserTeam[] | null, default: () => null },
    show: { type: Boolean, default: false },
  },
  emits: [],

  setup() {
    const identityStore = ref(userStore());
    const userteamStore = ref(teamStore());
    const userManagerRole = ref<string>("");
    const identityService = new IdentityService();
    const errorMsg = ref<string | null>(null);

    return {
      identityStore,
      userteamStore,
      userManagerRole,
      identityService,
      errorMsg,
    };
  },

  mounted() {
    //this.userManagerRole = this.identityStore.isInRole("manager");
  },

  methods: {
    async logOutClicked(): Promise<void> {
      console.log("logOutClicked");

      const res = await this.identityService.logout();

      //TODO: route if login succeeded and inform user
      if (res.status == 200) {
        window.localStorage.removeItem("prodRoad-r");
        window.localStorage.removeItem("prodRoad-j");

        /*TODO: make cleanup method in state*/
        this.identityStore.$state.jwt = null;
        this.identityStore.$id = "";
        this.identityStore.$state.email = null;
        this.identityStore.$state.role = [];
        this.identityStore.$state.jwtExp = null;
        this.identityStore.$state.jwtExp = null;

        this.userteamStore.teams = [];
        this.userteamStore.team = {} as ITeam;
        this.userteamStore.userTeams = [];
        this.userteamStore.publicTeam = {} as ITeam;

        this.$router.push({ name: "Login" });
      } else {
        //TODO: how to propagate
        this.errorMsg = "ERRORS.login-fail-message";
        console.log(this.errorMsg);
      }
    },
  },
};
</script>

<!-- bg-light-->
<template v-if="identityStore.$state.jwt != null">
  <nav
    class="navbar topnav navbar navbar-default navbar-fixed-top navbar-light user-nav"
  >
    <div class="d-flex">
      <RouterLink class="nav-link" to="/home">Home</RouterLink>
      <RouterLink class="nav-link" to="/user">Profile</RouterLink>
      <RouterLink
        v-if="identityStore.isInRole('manager')"
        class="nav-link"
        to="/team"
        >Teams</RouterLink
      >
      <RouterLink class="nav-link" to="/schedule">Schedule</RouterLink>

      <a @click="logOutClicked()" class="nav-link" href="#">Logout</a>
    </div>
    <LangChange style="float; margin-right:10px" />
  </nav>
</template>

<style scoped>
.topnav {
  /*z-index: 1;  Stay on top */
  top: 0; /* Stay at the top*/
  left: 0;
  background-color: #111; /* Black */
  padding-top: 20px;
}
.user-nav {
  color: #577d86;
  background-color: #f9f5eb;
  font-variant: small-caps;
  font-size: medium;
}
</style>
