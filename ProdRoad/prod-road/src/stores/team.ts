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
    },
    //TODO: create backend endpoint
    getPublicTeam() : ITeam {
      var team = this.teams.slice(this.teams.findIndex(team=> team.isPublic == true), 1)[0];
      if (team == null){
        team = {
          id : "PublicTeam",
          name: "Add new name",
          code: "Add new code",
          isPublic: true
        }
      } 
      console.log("Getting public team");
      console.log(team);
      return team;
    }
  },
});