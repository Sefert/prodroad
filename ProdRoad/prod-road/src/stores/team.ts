import type { ITeam } from "@/domain/ITeam"
import { defineStore } from "pinia"


export const teamStore = defineStore({
  id: "teams",
  state: () => ({
    teams: [
    ] as ITeam[],
  }),

  getters: {
  },
  actions: {
    add(team: ITeam) {
        this.teams.push(team);
      } 
  },
});