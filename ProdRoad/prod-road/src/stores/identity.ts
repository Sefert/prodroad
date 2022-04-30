import type { IJWTResponse } from "@/domain/IJWTResponse"
import { defineStore } from "pinia"


export const userStore = defineStore({
  id: "",
  state: () => ({
    jwt: null as IJWTResponse | null,
    email: null as string | null,
  }),
  getters: {
    getJWT(): IJWTResponse | null {
      return this.jwt;
    }
  },
  actions: {
  },

});