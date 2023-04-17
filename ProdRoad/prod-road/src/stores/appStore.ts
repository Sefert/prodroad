import { defineStore } from "pinia";

export const appStore = defineStore({
  id: "appStore",
  state: () => ({
    isExtended: false as boolean,
  }),

  getters: {
    getIsExtended(): boolean {
      return this.isExtended;
    },
  },

  actions: {
    setIsExtended(isEx: boolean): void {
      this.isExtended = isEx;
    },
  },
});
