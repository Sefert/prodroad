<script lang="ts">
/*import { Options, Vue } from "vue-class-component";*/
import { RouterLink, RouterView } from "vue-router";
import { userStore } from "@/stores/identity";
import TopNavBar from "./components/TopNavBar.vue";
import RightBar from "./components/RightBar.vue";
import { appStore } from "./stores/appStore";

//import { computed } from 'vue'
import { ref } from "vue";

export default {
  components: {
    TopNavBar,
    RightBar,
  },

  setup(props, { emit }) {
    const identityStore = ref(userStore());
    const appState = ref(appStore());

    // const userManagerRole: boolean = identityStore.isInRole("manager");
    return { identityStore, appState};
  },
};
</script>

<!--TODO:make protection better-->
<template>
  <div class="app wrapper">
    <div :class="[appState.getIsExtended ? 'largeAppWidth' : 'smallAppWidth']">
      <TopNavBar v-if="identityStore.$state.jwt != null" />
    </div>
    <div :class="[appState.getIsExtended ? 'largeAppWidth' : 'smallAppWidth']">
      <RouterView />
    </div>
    <RightBar v-if="identityStore.$state.jwt != null" />
  </div>
  <!--<div class="modals"></div>-->
</template>

<style>
@import "@/assets/base.css";

#app {
  max-width: 1280px;
  margin: 0 auto;
  padding: 0rem;
  font-weight: normal;
}

header {
  line-height: 1.5;
  max-height: 100vh;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

a,
.green {
  text-decoration: none;
  color: #d5b4b4;
  transition: 0.4s;
}

@media (hover: hover) {
  a:hover {
    background-color: #d5b4b4;
  }
}

nav {
  font-size: 12px;
  text-align: center;
  margin-top: 0rem;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}

@media (min-width: 1024px) {
}

.modals {
  width: 300px;
  padding: 30px;
  box-sizing: border-box;
  background-color: #fff;
  font-size: 20px;
  text-align: center;
}

.btn:hover {
  background-color: #f4b183;
  box-shadow: inset 0px 10px 10px #dbe4c6;
}

.smallAppWidth {
  width: calc(100% - 280px);
}

.largeAppWidth {
  width: calc(100% - 30px);
}
</style>
