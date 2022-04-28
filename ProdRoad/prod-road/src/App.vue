<script setup lang="ts">
import { Options, Vue } from "vue-class-component";
import { RouterLink, RouterView } from 'vue-router'
import TopNavBar from '@/components/TopNavBar.vue'
import RightBar from '@/components/RightBar.vue'
import Login from '@/components/Login.vue'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.min.js'

import { userStore } from "./stores/identity";
</script>

<!-- TODO: change to better login data -->
<template>
  <header>
    <!--<img alt="Vue logo" class="logo" src="@/assets/logo.svg" width="125" height="125" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">-->
    <!--<div class="wrapper">
      <HelloWorld msg="You did it!" />
      <nav>
        <RouterLink to="/">Home</RouterLink>
        <RouterLink to="/about">About</RouterLink>
      </nav>
    </div>-->
  </header>
    <div class="wrapper" v-if="userStore.id === 'true@true'">
      <TopNavBar />
      <nav class="wrapper">
        <RightBar />
      </nav>
    <!--<RouterLink to="/">Home</RouterLink>
    <RouterLink to="/about">About</RouterLink>-->  
    </div>
    <div class="wrapper" v-else>
      <Login />
    </div>
  <RouterView />
</template>


<style>
@import '@/assets/base.css';

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
</style>
