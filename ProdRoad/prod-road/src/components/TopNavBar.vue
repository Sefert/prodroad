<script lang="ts">
    import { Vue } from "vue-class-component";
    import { userStore } from "../stores/identity";

    export default class TopNavBar extends Vue {
        identityStore = userStore();


      logOutClicked(): void {
        console.log('logOutClicked');
        
        window.localStorage.removeItem("prodRoad-r");
        window.localStorage.removeItem("prodRoad-j");

        this.identityStore.$state.jwt = null;
        this.identityStore.$id = '';
        this.identityStore.$state.email = null;
        this.identityStore.$state.role = [];
        this.identityStore.$state.jwtExp = null;
      }
    }
</script>

<template v-if="identityStore.$state.jwt != null">
    <nav class="navbar topnav navbar navbar-default navbar-fixed-top navbar-light bg-light">
        <div class="d-flex">
            <RouterLink class="nav-link" to="/home">Home</RouterLink>
            <RouterLink class="nav-link" to="/user">Profile</RouterLink>

            <a @click="logOutClicked()" class="nav-link" href="#">Logout</a>
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