import type { IJWTResponse } from "@/domain/IJWTResponse"
import { defineStore } from "pinia"


export const userStore = defineStore({
  id: "some ",
  state: () => ({
    jwt: null as IJWTResponse | null
  }),
  getters: {
  },
  actions: {
  },

});