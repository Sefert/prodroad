import type { IJWTResponse } from "@/domain/IJWTResponse";
import router from "@/router";
import { defineStore } from "pinia";

export const userStore = defineStore({
  id: "" as string,
  state: () => ({
    jwt: null as IJWTResponse | null,
    email: null as string | null,
    role: [] as string[],
    jwtExp: null as number | null,
  }),
  getters: {
    getJWT(): IJWTResponse | null {
      return this.jwt;
    },
  },
  actions: {
    isInRole(access: string): boolean {
      let exist = false;

      console.log(this.role);
      console.log(this.email);

      if (typeof this.role == "string") {
        if (this.role == access) {
          exist = true;
        }
      } else {
        const index = this.role.findIndex((role) => role == access);
        if (index >= 0) {
          exist = true;
        }
      }
      return exist;
    },
    logOut(): void {
      console.log("logOut");

      window.localStorage.removeItem("prodRoad-r");
      window.localStorage.removeItem("prodRoad-j");

      this.$state.jwt = null;
      this.$id = "";
      this.$state.email = null;
      this.$state.role = [];
      this.$state.jwtExp = null;

      router.push("Login");
    },
  },
});
