import { createRouter, createWebHistory } from 'vue-router'
import Login from '@/components/Login.vue'
import { userStore } from "../stores/identity";
import TopNavBar from '@/components/TopNavBar.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/login",
      name: "Login",
      component: Login,
    },
    {
      path: "/home",
      name: "Home",
      component: TopNavBar,
    },
  ]
})

//TODO: make auth and direction guards better
router.beforeEach(async (to, from) => {
  var identityStore = userStore();
  console.log(identityStore.getJWT);
  if (identityStore.$state.jwt == null && to.name !== 'Login') return { name: 'Login' }
})

export default router
