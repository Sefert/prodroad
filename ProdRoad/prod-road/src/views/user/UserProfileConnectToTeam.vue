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
    const publicUserTeam = ref<IUserTeam | null>(null);
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
      publicUserTeam,
      publicUserTeams,
    };
  },

  methods: {
    async getPublicTeam(): Promise<void> {
      let res = null;

      console.log("NewTest");
      console.log(this.userteamStore.$state.userTeams);
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
              this.userteamStore.$state.publicTeam = data;
              //this.userteamStore.$state.teams.push(data);
              //const uT = this.userteamStore.publicTeam;

              //TODO: nver enters here
              if (data.userTeams == null) {
                this.publicUserTeam = null;
              } else {
                if (data.userTeams.length == 1) {
                  this.publicUserTeam = data.userTeams[0];
                  console.log(`HERE ${this.publicUserTeam}`);
                } else {
                  //TODO: implent it
                }
              }
            }
            //this.publicUserTeams = uT == null ? [] : uT;

            console.log(this.userteamStore.$state.teams);
            console.log(this.publicUserTeam);
          }
        }
      }
    },

    async joinPublicTeam(id: string): Promise<void> {
      //console.log(this.identity.$id);
      console.log(id);
      const userTeam: IUserTeam = {
        AppUserId: this.identityStore.$id,
        TeamId: id,
        accepted: false,
        team: null,
      };

      if (id != null) {
        /*check is joined*/
        const isJoinedRes = await this.teamService.getPublicTeam(id); //await this.userTeamService.getByTeamId(id);
        let res = null;

        if (isJoinedRes.data?.userTeams == null) {
          res = await this.userTeamService.add(userTeam);
        } else if (isJoinedRes.data?.userTeams[0]?.id != null) {
          res = await this.userTeamService.edit(
            isJoinedRes.data?.userTeams[0]?.id,
            userTeam
          );
        }

        if (res != null) {
          if (res.status != null && typeof res.status != "undefined") {
            if (res.status >= 300) {
              this.errorMsg = "ERRORS.login-fail-message";
              console.log(this.errorMsg);
            } else {
              if (res.data != null) {
                const data: IUserTeam = res.data;
                this.userteamStore.addUserTeam(data);
                this.publicUserTeam = data;
                //this.errorMsg = "ERRORS.login-fail-message";
                console.log(data);
              }
              console.log("Here");
              console.log(res);
            }
          }
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
          this.publicUserTeam = null;
        }
      }
    },
  },

  //TODO: ask for only public userteam
  mounted() {
    console.log("NewTest");
    this.publicUserTeam = this.userteamStore.getPublicUserTeam();
  },
};
</script>

<template>
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

    <div v-if="publicUserTeam != null" class="form-check form-switch">
      <p style="left: -40px">Leave TEAM:</p>
      <input
        checked
        @change="leavePublicTeam(publicUserTeam.id!)"
        type="checkbox"
        class="form-check-input"
      />
      <label class="form-check-label" for="flexSwitchCheckDefault">{{
        userteamStore.$state.publicTeam.code
      }}</label>
    </div>
    <div
      v-else-if="userteamStore.$state.publicTeam.id != null"
      class="form-check form-switch"
    >
      <p style="left: -40px">Join TEAM:</p>
      <input
        @change="joinPublicTeam(userteamStore.$state.publicTeam.id!)"
        type="checkbox"
        class="form-check-input"
      />
      <label class="form-check-label" for="flexSwitchCheckDefault">{{
        userteamStore.$state.publicTeam.code
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
