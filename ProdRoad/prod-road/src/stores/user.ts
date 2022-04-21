import { defineStore } from "pinia";

export const userStore = defineStore({
  id: "counter",
  state: () => ({
    email: 'not set',
    password: 0,
  }),
});