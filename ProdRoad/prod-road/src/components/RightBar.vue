<script lang="ts">
import { userStore } from "@/stores/identity";
import type { HtmlAttributes } from "csstype";
import { type PropType, ref, defineComponent } from "vue";
import TeamsView from "../views/team/TeamsView.vue";
import PersonsView from "../views/user/PersonsView.vue";
import { appStore } from "@/stores/appStore";

//https://blog.bitsrc.io/why-you-should-avoid-mutating-or-reassigning-props-in-vue-ed25f27be88d
export default defineComponent({
  components: {
    //TeamsView,
    PersonsView,
  },

  setup() {
    const identityStore = ref(userStore());

    const isActiveTest = ref<boolean>(false);
    const isActiveTeams = ref<boolean>(false);
    const isActiveLink = ref<boolean>(false);
    const isActiveDrop = ref<string>("Teams");
    const appState = ref(appStore());

    const errorMsg = ref<string | null>(null);
    const isNotHiddenSidebar = ref<boolean>(true);

    return {
      identityStore,
      isActiveTest,
      isActiveTeams,
      isActiveLink,
      isActiveDrop,
      errorMsg,
      isNotHiddenSidebar,
      hideClass: "hide-element",
      showClass: "show-element",
      showHide: "hide",
      appState,
    };
  },

  //BEST: https://stackoverflow.com/questions/39868963/vue-2-mutating-props-vue-warn
  //https://vuejs.org/guide/essentials/component-basics.html#using-v-model-on-components
  methods: {
    isActive(active: string) {
      console.log(active);
      switch (active) {
        case "Test":
          this.isActiveTest = !this.isActiveTest;
          this.isActiveTeams = false;
          this.isActiveLink = false;
          break;
        case "Teams":
          this.isActiveTest = false;
          this.isActiveTeams = !this.isActiveTeams;
          this.isActiveLink = false;
          break;
        case "Link":
          this.isActiveTest = false;
          this.isActiveTeams = false;
          this.isActiveLink = !this.isActiveLink;
          break;
        default:
          this.isActiveTest = false;
          this.isActiveTeams = false;
          this.isActiveLink = false;
      }
    },
    handleClick() {
      this.isNotHiddenSidebar = !this.isNotHiddenSidebar;
      this.showHide = this.showHide == "hide" ? "show" : "hide";
      this.appState.setIsExtended(!this.isNotHiddenSidebar);
    },
  },

  mounted() {
    //const userManagerRole = this.identityStore.isInRole("manager");
  },
});
</script>
<!--:class="[isHiddenSidebar ? hideClass : showClass]"
    v-show="isNotHiddenSidebar"-->
<template>
  <div
    class="sidenav p-3 text-start"
    :class="[isNotHiddenSidebar ? showClass : hideClass]"
  >
    <button
      style="left: -50px; top: 50%"
      class="btn btn-color"
      @click="handleClick()"
    >
      <i class="arrow" :class="[isNotHiddenSidebar ? 'right' : 'left']">{{
        showHide
      }}</i>
    </button>
    <div style="top: -50px">
      <ul class="nav nav-tabs">
        <li class="nav-item">
          <span
            v-bind:class="{ active: isActiveTest }"
            @click="isActive('Test')"
            class="nav-link user-text"
            >Test</span
          >
        </li>
        <li class="nav-item">
          <span
            v-bind:class="{ active: isActiveTeams }"
            @click="isActive('Teams')"
            class="nav-link user-text"
            >Teams</span
          >
        </li>
        <li class="nav-item">
          <span
            v-bind:class="{ active: isActiveLink }"
            @click="isActive('Link')"
            class="nav-link user-text"
            >Link</span
          >
        </li>
      </ul>

      <ul v-if="isActiveTeams" class="nav">
        <li class="nav-item dropdown">
          <a
            class="nav-link dropdown-toggle user-text"
            data-bs-toggle="dropdown"
            href="#"
            role="button"
            aria-expanded="false"
            >{{ isActiveDrop }}</a
          >
          <ul class="dropdown-menu">
            <li>
              <a
                @click="isActiveDrop = 'Teams'"
                class="dropdown-item user-text"
                href="#"
                >Teams</a
              >
            </li>
            <li><hr class="dropdown-divider" /></li>
            <li>
              <a
                @click="isActiveDrop = 'Persons'"
                class="dropdown-item user-text"
                href="#"
                >Persons</a
              >
            </li>
          </ul>
        </li>
      </ul>

      <div
        v-if="isActiveTeams && isActiveDrop == 'Teams'"
        class="container"
      ></div>

      <div
        v-else-if="isActiveTeams && isActiveDrop == 'Persons'"
        class="container"
      >
        <PersonsView />
      </div>
      <!--<h4>Stuff</h4>
        <ul class="list-unstyled ps-0">
            <li class="border-top my-3"></li>
            <li class="mb-1">
                <button class="btn btn-toggle align-items-center rounded collapsed" data-bs-toggle="collapse" data-bs-target="#home-collapse" aria-expanded="false">
                Home
                </button>
                <div class="collapse" id="home-collapse" style="">
                <ul class="btn-toggle-nav align-items-center list-unstyled fw-normal pb-1 small">
                    <li><a href="#" class="link-dark rounded">Overview</a></li>
                    <li><a href="#" class="link-dark rounded">Updates</a></li>
                    <li><a href="#" class="link-dark rounded">Reports</a></li>
                </ul>
                </div>
            </li>
            <li class="mb-1">
                <button class="btn btn-toggle align-items-center rounded collapsed" data-bs-toggle="collapse" data-bs-target="#dashboard-collapse" aria-expanded="false">
                Dashboard
                </button>
                <div class="collapse" id="dashboard-collapse" style="">
                <ul class="btn-toggle-nav list-unstyled fw-normal pb-1 small">
                    <li><a href="#" class="link-dark rounded">Overview</a></li>
                    <li><a href="#" class="link-dark rounded">Weekly</a></li>
                    <li><a href="#" class="link-dark rounded">Monthly</a></li>
                    <li><a href="#" class="link-dark rounded">Annually</a></li>
                </ul>
                </div>
            </li>
            <li class="mb-1">
                <button class="btn btn-toggle align-items-center rounded collapsed" data-bs-toggle="collapse" data-bs-target="#orders-collapse" aria-expanded="false">
                Orders
                </button>
                <div class="collapse" id="orders-collapse" style="">
                <ul class="btn-toggle-nav list-unstyled fw-normal pb-1 small">
                    <li><a href="#" class="link-dark rounded">New</a></li>
                    <li><a href="#" class="link-dark rounded">Processed</a></li>
                    <li><a href="#" class="link-dark rounded">Shipped</a></li>
                    <li><a href="#" class="link-dark rounded">Returned</a></li>
                </ul>
                </div>
            </li>
            <li class="border-top my-3"></li>
            <li class="mb-1">
                <button class="btn btn-toggle align-items-center rounded collapsed" data-bs-toggle="collapse" data-bs-target="#account-collapse" aria-expanded="false">
                Account
                </button>
                <div class="collapse" id="account-collapse" style="">
                <ul class="btn-toggle-nav list-unstyled fw-normal pb-1 small">
                    <li><a href="#" class="link-dark rounded">New...</a></li>
                    <li><a href="#" class="link-dark rounded">Profile</a></li>
                    <li><a href="#" class="link-dark rounded">Settings</a></li>
                    <li><a href="#" class="link-dark rounded">Sign out</a></li>
                </ul>
                </div>
            </li>
        </ul>-->
    </div>
  </div>
</template>

<style scoped>
body {
  min-height: 100vh;
  min-height: -webkit-fill-available;
}

html {
  height: -webkit-fill-available;
}

main {
  display: flex;
  flex-wrap: nowrap;
  height: 100vh;
  height: -webkit-fill-available;
  max-height: 100vh;
  overflow-x: auto;
  overflow-y: hidden;
}

.b-example-divider {
  flex-shrink: 0;
  width: 1.5rem;
  height: 100vh;
  background-color: rgba(0, 0, 0, 0.1);
  border: solid rgba(0, 0, 0, 0.15);
  border-width: 1px 0;
  box-shadow: inset 0 0.5em 1.5em rgba(0, 0, 0, 0.1),
    inset 0 0.125em 0.5em rgba(0, 0, 0, 0.15);
}

.bi {
  vertical-align: -0.125em;
  pointer-events: none;
  fill: currentColor;
}

.dropdown-toggle {
  outline: 0;
}

.nav-flush .nav-link {
  border-radius: 0;
}

.btn-toggle {
  display: inline-flex;
  align-items: center;
  padding: 0.25rem 0.5rem;
  font-weight: 600;
  color: rgba(0, 0, 0, 0.65);
  background-color: transparent;
  border: 0;
}
.btn-toggle:hover,
.btn-toggle:focus {
  color: rgba(0, 0, 0, 0.85);
  background-color: #f9f5eb;
}

.btn-toggle::before {
  width: 1.25em;
  line-height: 0;
  content: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' viewBox='0 0 16 16'%3e%3cpath fill='none' stroke='rgba%280,0,0,.5%29' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M5 14l6-6-6-6'/%3e%3c/svg%3e");
  transition: transform 0.35s ease;
  transform-origin: 0.5em 50%;
}

.btn-toggle[aria-expanded="true"] {
  color: rgba(0, 0, 0, 0.85);
}
.btn-toggle[aria-expanded="true"]::before {
  transform: rotate(90deg);
}

.btn-toggle-nav a {
  display: inline-flex;
  padding: 0.1875rem 0.5rem;
  margin-top: 0.125rem;
  margin-left: 1.25rem;
  text-decoration: none;
}
.btn-toggle-nav a:hover,
.btn-toggle-nav a:focus {
  background-color: #f9f5eb;
}

.scrollarea {
  overflow-y: auto;
}

.fw-semibold {
  font-weight: 600;
}
.lh-tight {
  line-height: 1.25;
}

.bd-placeholder-img {
  font-size: 1.125rem;
  text-anchor: middle;
  -webkit-user-select: none;
  -moz-user-select: none;
  user-select: none;
}

@media (min-width: 768px) {
  .bd-placeholder-img-lg {
    font-size: 3.5rem;
  }
}

.sidenav {
  height: 100%; /* Full-height: remove this if you want "auto" height */
  width: 160px; /* Set the width of the sidebar */
  position: fixed; /* Fixed Sidebar (stay in place on scroll) */
  /*z-index: 1;  Stay on top */
  top: 0; /* Stay at the top */
  right: 0;
  background-color: #f9f5eb; /* Black */
  color: #577d86;
  /* overflow-x: hidden; Disable horizontal scroll */
  padding-top: 20px;
  box-shadow: 5px 10px 18px #888888;
}

.user-text {
  color: #577d86;
  font-variant: small-caps;
}

.btn-color {
  background-color: #e4dccf;
}
.btn:hover {
  background-color: #f4b183;
  box-shadow: 0px 10px 10px #dbe4c6;
}

.arrow {
  /*https://www.w3schools.com/howto/howto_css_arrows.asp*/
  border: solid black;
  border-width: 0 3px 3px 0;
  display: inline-block;
  padding: 3px;
}

.right {
  transform: rotate(-45deg);
  -webkit-transform: rotate(-45deg);
}

.left {
  transform: rotate(135deg);
  -webkit-transform: rotate(135deg);
}

.hide-element {
  width: 10px;
  transition: 0.3s display ease;
}
.show-element {
  width: 280px;
  transition: 0.3s display ease;
}
</style>
