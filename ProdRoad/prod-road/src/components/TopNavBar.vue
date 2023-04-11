<script lang="ts">
import type { IUserTeam } from "@/domain/IUserTeam";
import { ref, type PropType } from "vue";
import { userStore } from "../stores/identity";
import LangChange from "./LangChange.vue";

export default {
  components: {
    LangChange,
  },

  sprops: {
    publicTeam: {
      type: null as unknown as PropType<string | null>,
      default: null,
      required: true,
    },
    errorMsg: {
      type: null as unknown as PropType<string | null>,
      default: null,
      required: true,
    },
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
    return {
      identityStore,
    };
  },

  mounted() {
    //const userManagerRole = this.identityStore.isInRole("manager");
  },

  methods: {
    logOutClicked(): void {
      console.log("logOutClicked");

      window.localStorage.removeItem("prodRoad-r");
      window.localStorage.removeItem("prodRoad-j");

      this.identityStore.$state.jwt = null;
      this.identityStore.$id = "";
      this.identityStore.$state.email = null;
      this.identityStore.$state.role = [];
      this.identityStore.$state.jwtExp = null;
    },
  },
};
</script>

<template v-if="identityStore.$state.jwt != null">
  <nav
    class="navbar topnav navbar navbar-default navbar-fixed-top navbar-light bg-light"
  >
    <div class="d-flex">
      <RouterLink class="nav-link" to="/home">Home</RouterLink>
      <RouterLink class="nav-link" to="/user">Profile</RouterLink>
      <!--<RouterLink v-if="userManagerRole"  class="nav-link" to="/team">Teams</RouterLink>-->

      <a @click="logOutClicked()" class="nav-link" href="#">Logout</a>

      <LangChange />
    </div>
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
</style>
