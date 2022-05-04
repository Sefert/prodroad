import type { ITeam } from "@/domain/ITeam"
import { defineStore } from "pinia"


export const teamStore = defineStore({
  id: "teams",
  state: () => ({
    teams: [] as ITeam[],
  }),

  getters: {
      getTeams() : ITeam[]{
          return this.teams;
      }
  },
  actions: {
    add(team: ITeam) : void {
        this.teams.push(team);
    },
    delete(id: string) : void{
        this.teams.splice(this.teams.findIndex(team=> team.id == id), 1);
    }
  },
});