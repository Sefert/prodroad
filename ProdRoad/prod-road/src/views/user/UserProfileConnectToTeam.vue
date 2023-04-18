<script lang="ts">
import type { ITeam } from "@/domain/ITeam";
import type { IUserTeam } from "@/domain/IUserTeam";
import { IdentityService } from "@/services/identity/IdentityService";
import { TeamService } from "@/services/TeamService";
import { UserTeamService } from "@/services/UserTeamService";
import { teamStore } from "@/stores/team";
import { ref, type PropType } from "vue";
import { userStore } from "../../stores/identity";
import ErrorParagraph from "../../components/errors/ErrorParagraph.vue";

export default {
  components: {
    //ErrorParagraph,
  },

  //https://stackoverflow.com/questions/58241604/how-do-you-type-hint-vue-props-with-typescript-interfaces
  /*props: {
    publicUserTeams: { type: Array as () => IUserTeam[], default: () => [] },
    emits: ["update:publicUserTeams"],
  },*/

  setup() {
    const identityStore = ref(userStore());
    const userteamStore = ref(teamStore());
    const identityService = new IdentityService();
    const teamService = new TeamService();
    const userTeamService = new UserTeamService();
    const errorMsg = ref<string | null>(null);
    const teamCode = ref<string | null>("");
    const askJoin = ref<boolean>(false);
    const publicUserTeams = ref<IUserTeam[]>([]);

    return {
      identityStore,
      userteamStore,
      identityService,
      teamService,
      userTeamService,
      errorMsg,
      teamCode,
      askJoin,
      publicUserTeams,
    };
  },

  methods: {
    async getPublicTeam(): Promise<void> {
      let res = null;

      this.userteamStore.$state.teams = [];

      if (this.teamCode != null) {
        res = await this.teamService.getPublicTeam(this.teamCode);
      }

      if (res != null && typeof res != "undefined") {
        if (res.status != null && typeof res.status != "undefined") {
          if (res.status >= 300) {
            this.errorMsg = "ERRORS.login-fail-message";
            console.log(this.errorMsg);
          } else {
            const data = res.data;
            console.log(data);
            if (data != null) {
              this.userteamStore.$state.teams.push(data);
              const uT = this.userteamStore.$state.teams[0].userTeams;
              if (uT == null) {
                this.publicUserTeams = [];
              } else {
                this.publicUserTeams = uT;
              }
            }

            //this.publicUserTeams = uT == null ? [] : uT;

            console.log(this.userteamStore.$state.teams);
            console.log(this.publicUserTeams);
            console.log(this.userteamStore.$state.teams[0]);
            console.log(this.userteamStore.$state.teams[0].userTeams);
          }
        }
      }
    },

    async joinPublicTeam(team: ITeam): Promise<void> {
      //console.log(this.identity.$id);
      console.log(team.id);
      const userTeam: IUserTeam = {
        AppUserId: this.identityStore.$id,
        TeamId: team.id,
        accepted: false,
      };

      const res = await this.userTeamService.add(userTeam);

      if (res.status != null && typeof res.status != "undefined") {
        if (res.status >= 300) {
          this.errorMsg = "ERRORS.login-fail-message";
          console.log(this.errorMsg);
        } else {
          if (res.data != null) {
            const data: IUserTeam = res.data;
            this.errorMsg = "ERRORS.login-fail-message";
            console.log(data);
          }
          console.log("Here");
          console.log(res);
        }
      }
    },

    async leavePublicTeam(id: string): Promise<void> {
      console.log(id);
      /*console.log(team.id)
        var userTeam : IUserTeam = {
            AppUserId : this.identity.$id,
            TeamId : team.id,
            Accepted : false
        };*/

      const res = await this.userTeamService.delete(id);

      /*TODO:check if got really deleted*/
      if (res.status != null && typeof res.status != "undefined") {
        if (res.status >= 300) {
          this.errorMsg = "ERRORS.login-fail-message";
          console.log(this.errorMsg);
        } else {
          this.publicUserTeams = [];
        }
      }
    },

    //TODO: ask for only public userteam
    async mounted() {
      console.log("connect to teams");
      this.userteamStore.$state.teams = [];
      /*this.teamStore.$state.teams= [];
        var res = await this.userTeamService.getAll();
        var userTeams : IUserTeam[] | null = res.data;*/
    },
  },
};
</script>

<template>
  <TopNavBar />
  <div class="container" style="margin-top: 10px">
    <div class="row height d-flex justify-content-center align-items-center">
      <div class="col-md-8">
        <div class="search">
          <i class="fa fa-search"></i>
          <input
            v-model="teamCode"
            type="text"
            class="form-control"
            placeholder="Insert given CODE"
          />
          <button @click="getPublicTeam()" type="button" class="btn btn-color">
            Search
          </button>
        </div>
      </div>
    </div>

    <br />
    <br />

    <div
      v-if="
        publicUserTeams.length != 0 && userteamStore.$state.teams.length != 0
      "
      class="form-check form-switch"
    >
      <p>Leave TEAM:</p>
      <input
        checked
        @change="leavePublicTeam(publicUserTeams![0].id!)"
        type="checkbox"
        class="form-check-input"
      />
      <label class="form-check-label" for="flexSwitchCheckDefault">{{
        userteamStore.$state.teams[0].code
      }}</label>
    </div>
    <div
      v-else-if="userteamStore.$state.teams.length != 0"
      class="form-check form-switch"
    >
      <p>Join TEAM:</p>
      <input
        @change="joinPublicTeam(userteamStore.$state.teams[0])"
        type="checkbox"
        class="form-check-input"
      />
      <label class="form-check-label" for="flexSwitchCheckDefault">{{
        userteamStore.$state.teams[0].code
      }}</label>
    </div>
  </div>
</template>

<style scoped>
.user-text {
  color: #577d86;
  font-variant: small-caps;
}

.btn-color {
  background-color: #e4dccf;
  box-shadow: 0px 5px 10px #dbe4c6;
}
.btn:hover {
  background-color: #f4b183;
  box-shadow: 0px 10px 10px #dbe4c6;
}
</style>
