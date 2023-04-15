<script lang="ts">
/*import { Options, Vue } from "vue-class-component";*/
import { RouterLink, RouterView } from "vue-router";
import { userStore } from "@/stores/identity";
import TopNavBar from "./components/TopNavBar.vue";
import RightBar from "./components/RightBar.vue";

//import { computed } from 'vue'
import { ref } from "vue";

export default {
  components: {
    TopNavBar,
    RightBar,
  },
  props: {},
  emits: [],
  setup() {
    const identityStore = ref(userStore());
    // const userManagerRole: boolean = identityStore.isInRole("manager");
    return { identityStore };
  },
};
</script>

<!--TODO:make protection better-->
<template>
  <div class="app wrapper">
    <TopNavBar v-if="identityStore.$state.jwt != null" />
    <div style="width: calc(100% - 280px)">
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
  color: hsla(160, 100%, 37%, 1);
  transition: 0.4s;
}

@media (hover: hover) {
  a:hover {
    background-color: hsla(160, 100%, 37%, 0.2);
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
</style>
