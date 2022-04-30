<script lang="ts">
    import { Vue } from "vue-class-component";
    import { userStore } from "../stores/identity";

    export default class TopNavBar extends Vue {
        identityStore = userStore();


      logOutClicked(): void {
        console.log('logOutClicked');
        
        this.identityStore.$state.jwt = null;

        console.log(this.identityStore.$state.jwt);
      }
    }
</script>

<template v-if="identityStore.$state.jwt != null">
    <nav class="navbar topnav navbar navbar-default navbar-fixed-top navbar-light bg-light">
        <div class="d-flex">
            <a class="nav-link" href="#">Home</a>
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