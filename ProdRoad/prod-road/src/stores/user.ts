import { defineStore } from "pinia";

export const userStore = defineStore({
  id: "counter",
  state: () => ({
    email: 'false',
    password: 0,
  }),
});