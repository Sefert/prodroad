//import { userStore } from '@/stores/identity';
import type { IUserTeam } from "./../domain/IUserTeam";
import type { ITeam } from "@/domain/ITeam";
import { defineStore } from "pinia";

export const teamStore = defineStore({
  id: "teams",
  state: () => ({
    teams: [] as ITeam[],
    team: {} as ITeam,
    userTeams: [] as IUserTeam[],
  }),

  getters: {
    getTeams(): ITeam[] {
      return this.teams;
    },
    getUserTeams(): IUserTeam[] {
      return this.userTeams;
    },
  },

  actions: {
    add(team: ITeam): void {
      this.teams.push(team);
    },
    addUserTeam(ut: IUserTeam): void {
      this.userTeams.push(ut);
    },
    delete(id: string): void {
      this.teams.splice(
        this.teams.findIndex((team) => team.id == id),
        1
      );
    },
    //TODO: create backend endpoint
    getPublicTeam(): ITeam {
      let index: number = this.teams.findIndex((x) => x.isPublic == true);
      let team = this.teams.slice(index, ++index)[0];

      console.log(team);

      if (team == null) {
        team = {
          id: "PublicTeam",
          name: "Add new name",
          code: "Add new code",
          isPublic: true,
        };
      }
      //console.log("Getting public team");
      //console.log(team.isPublic);
      //console.log(team);
      //console.log("Getting public team");
      return team;
    },
  },
});
