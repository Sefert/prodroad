<script lang="ts">
import { userStore } from "@/stores/identity";
import { type PropType, ref, defineComponent } from "vue";
import TeamsView from "../views/team/TeamsView.vue";
import PersonsView from "../views/user/PersonsView.vue";

//https://blog.bitsrc.io/why-you-should-avoid-mutating-or-reassigning-props-in-vue-ed25f27be88d
export default defineComponent({
  components: {
    TeamsView,
    PersonsView,
  },

  props: {
    isActiveTest: { type: Boolean, default: false },
    isActiveTeams: { type: Boolean, default: false },
    isActiveLink: { type: Boolean, default: false },
    isActiveDrop: { type: String, default: "Teams" },
    errorMsg: {
      type: null as unknown as PropType<string | null>,
      default: null,
      required: true,
    },
  },

  emits: [
    "update:isActiveTest",
    "update:isActiveTeams",
    "update:isActiveLink",
    "update:isActiveDrop",
    "update:errorMsg",
  ],

  setup() {
    const identityStore = ref(userStore());

    return {
      identityStore,
    };
  },

  //BEST: https://stackoverflow.com/questions/39868963/vue-2-mutating-props-vue-warn
  //https://vuejs.org/guide/essentials/component-basics.html#using-v-model-on-components
  methods: {
    isActive(active: string) {
      console.log(active);
      switch (active) {
        case "Test":
          this.$emit("update:isActiveTest", !this.isActiveTest);
          this.$emit("update:isActiveTeams", false);
          this.$emit("update:isActiveLink", false);
          break;
        case "Teams":
          this.$emit("update:isActiveTest", false);
          this.$emit("update:isActiveTeams", !this.isActiveTeams);
          this.$emit("update:isActiveLink", false);
          break;
        case "Link":
          this.$emit("update:isActiveTest", false);
          this.$emit("update:isActiveTeams", false);
          this.$emit("update:isActiveLink", !this.isActiveLink);
          break;
        default:
          this.$emit("update:isActiveTest", false);
          this.$emit("update:isActiveTeams", false);
          this.$emit("update:isActiveLink", false);
      }
    },
  },

  mounted() {
    //const userManagerRole = this.identityStore.isInRole("manager");
  },
});
</script>

<template>
  <div class="sidenav p-3 bg-white text-start" style="width: 280px">
    <ul class="nav nav-tabs">
      <li class="nav-item">
        <span
          v-bind:class="{ active: isActiveTest }"
          @click="isActive('Test')"
          class="nav-link"
          >Test</span
        >
      </li>
      <li class="nav-item">
        <span
          v-bind:class="{ active: isActiveTeams }"
          @click="isActive('Teams')"
          class="nav-link"
          >Teams</span
        >
      </li>
      <li class="nav-item">
        <span
          v-bind:class="{ active: isActiveLink }"
          @click="isActive('Link')"
          class="nav-link"
          >Link</span
        >
      </li>
    </ul>

    <ul v-if="isActiveTeams" class="nav">
      <li class="nav-item dropdown">
        <a
          class="nav-link dropdown-toggle"
          data-bs-toggle="dropdown"
          href="#"
          role="button"
          aria-expanded="false"
          >{{ isActiveDrop }}</a
        >
        <ul class="dropdown-menu">
          <li>
            <a
              @click="$emit('update:isActiveDrop', 'Teams')"
              class="dropdown-item"
              href="#"
              >Teams</a
            >
          </li>
          <li><hr class="dropdown-divider" /></li>
          <li>
            <a
              @click="$emit('update:isActiveDrop', 'Persons')"
              class="dropdown-item"
              href="#"
              >Persons</a
            >
          </li>
        </ul>
      </li>
    </ul>

    <div v-if="isActiveTeams && isActiveDrop == 'Teams'" class="container">
      <TeamsView />
    </div>
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
  background-color: #d2f4ea;
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
  background-color: #d2f4ea;
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
  background-color: #111; /* Black */
  overflow-x: hidden; /* Disable horizontal scroll */
  padding-top: 20px;
  box-shadow: 5px 10px 18px #888888;
}
</style>
