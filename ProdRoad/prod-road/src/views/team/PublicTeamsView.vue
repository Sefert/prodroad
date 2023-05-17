<script lang="ts">
import TopNavBar from "../../components/TopNavBar.vue";

import { userStore } from "../../stores/identity";
import { teamStore } from "../../stores/team";

import type { ITeam } from "../../domain/ITeam";
import type { IServiceResult } from "../../services/contracts/IServiceResult";

import { TeamService } from "../../services/TeamService";

//import TestModal from "../../components/TestModal.vue"
//import CustomModal from "../../components/TestModal.vue";
import { UserTeamService } from "@/services/UserTeamService";
import type { IUserTeam } from "@/domain/IUserTeam";
import { reactive, ref, type PropType } from "vue";
import { IdentityService } from "@/services/identity/IdentityService";

export default {
  components: {
    //TestModal,
    //CustomModal,
    //TopNavBar,
  },

  setup() {
    const identityStore = ref(userStore());
    const userteamStore = ref(teamStore());
    const identityService = new IdentityService();
    const teamService = new TeamService();
    const userTeamService = new UserTeamService();

    const errorMsg = ref<string | null>(null);
    const editTeamId = ref<string | null>(null);
    const publicTeam = ref<ITeam>({
      id: "PublicTeam",
      AppUserId: null,
      name: "Insert",
      code: "Insert",
      isPublic: true,
      userTeams: null,
    });
    const userTeams = ref<IUserTeam[] | null>(null);
    const show = ref<boolean>(false);

    return {
      identityStore,
      userteamStore,
      identityService,
      teamService,
      userTeamService,
      errorMsg,
      editTeamId,
      publicTeam,
      userTeams,
      show,
    };
  },

  methods: {
    async saveRow(team: ITeam) {
      console.log("Save row");

      team.AppUserId = this.identityStore.$id;

      let res: IServiceResult<ITeam> | IServiceResult<void> | null = null;

      if (team.id == "newTeam" || team.id == "PublicTeam") {
        this.userteamStore.delete("newTeam");
        //accepts with no id only
        const teamToAdd: ITeam = {
          name: team.name,
          code: team.code,
          isPublic: team.id == "PublicTeam" ? true : false,
        };
        res = await this.teamService.add(teamToAdd);

        console.log("here-st");
      } else {
        //TODO: should reload when fail or revert back
        if (team.id != null) {
          res = await this.teamService.edit(team.id, team);
        }
      }
      console.log("here-st3");
      if (res != null) {
        if (res.status != null && typeof res.status != "undefined") {
          console.log("here-st34");
          console.log(res);
          console.log(res.status);
          if (res.status >= 300) {
            console.log("here-st2");
            this.errorMsg = "ERRORS.please-try-again";
            console.log(this.errorMsg);
          } else if (res.status == 201) {
            const data = res.data;
            if (data != null && typeof data != "undefined") {
              this.userteamStore.add(data);
            }
            this.cancelChange("newTeam");
          }
        }
      }

      this.editTeamId = null;
    },

    editRow(id: string) {
      this.editTeamId = id;
    },

    /*async deleteRow(id : string){
        var res : IServiceResult<void> = await this.teamService.delete(id);
        console.log('HERE1');
        console.log(res.status);
        if (res != null && typeof(res) != "undefined"){
            if (res.status! >= 300) {
                this.errorMsg = res.status + ' ' + res.errorMsg;
                console.log(this.errorMsg);
            } else {
                    this.teamStore.delete(id);
            }  
        }    
    }*/

    cancelChange(id: string) {
      if (id == null) {
        this.userteamStore.delete(id);
      }
      this.editTeamId = null;
    },

    async acceptUser(id: string, userTeam: IUserTeam) {
      userTeam.accepted = true;
      const res = await this.userTeamService.edit(id, userTeam);
      if (res.status != null && typeof res.status != "undefined") {
        if (res.status >= 300) {
          this.errorMsg = "ERRORS.please-try-again";
          console.log(this.errorMsg);
        }
      }
    },

    async removeUser(id: string, userTeam: IUserTeam) {
      userTeam.accepted = false;
      const res = await this.userTeamService.edit(id, userTeam);
      if (res.status != null && typeof res.status != "undefined") {
        if (res.status >= 300) {
          this.errorMsg = "ERRORS.please-try-again";
          console.log(this.errorMsg);
        }
      }
    },

    async declineRequest(id: string) {
      console.log(id);
      const res = await this.userTeamService.delete(id);
      if (res.status != null && typeof res.status != "undefined") {
        if (res.status >= 300) {
          this.errorMsg = "ERRORS.please-try-again";
          console.log(this.errorMsg);
        }
      }
    },

    async getTeams(): Promise<ITeam[]> {
      const res: IServiceResult<ITeam[]> = await this.teamService.getAll();
      console.log(res);
      if (res.status != null && typeof res.status != "undefined") {
        if (res.status >= 300) {
          this.errorMsg = "ERRORS.please-try-again";
          console.log(this.errorMsg);
        } else {
          const data = res.data;
          console.log(data);
        }
        return res.data != null ? res.data : [];
      }
      return [];
    },
  },

  async mounted(): Promise<void> {
    console.log("PublicTeamsView mounted");

    if (!this.identityStore.isInRole("manager")) {
      this.identityStore.logOut();
    }
    if (this.userteamStore.getPublicTeam().id == "PublicTeam") {
      await this.getTeams()
        .then((data: ITeam[]) => {
          this.userteamStore.$state.teams = data;
        })
        .then(() => {
          this.publicTeam = this.userteamStore.getPublicTeam();
          console.log(this.publicTeam);
        });
    }
  },
};
</script>

<template>
  <!--Manage public team -->
  <div class="container card mt-3">
    <div class="d-flex row">
      <div class="float-left col">
        <h4><small>Manage public team connection</small></h4>
      </div>
    </div>
    <div class="table-responsive">
      <table class="table">
        <tbody>
          <!-- Vertically centered modal -->
          <tr v-if="publicTeam.id == editTeamId && publicTeam.isPublic == true">
            <td>
              <input
                v-model="publicTeam.name"
                class="form-control"
                placeholder="Add new name"
              />
            </td>
            <td>
              <input
                v-model="publicTeam.code"
                class="form-control"
                placeholder="Add new code"
              />
            </td>
            <td>
              <button
                @click="saveRow(publicTeam!)"
                type="button"
                rel="tooltip"
                class="btn btn-success btn-just-icon btn-sm"
                data-original-title=""
                title=""
              >
                <i class="material-icons">SAVE</i>
              </button>
              <button
                @click="cancelChange(publicTeam.id!)"
                type="button"
                rel="tooltip"
                class="btn btn-danger btn-just-icon btn-sm"
                data-original-title=""
                title=""
              >
                <i class="material-icons">CANCEL</i>
              </button>
            </td>
          </tr>
          <tr v-else-if="publicTeam.isPublic == true">
            <td>{{ publicTeam.name }}</td>
            <td>{{ publicTeam.code }}</td>
            <td>
              <button
                @click="editRow(publicTeam!.id!)"
                type="button"
                rel="tooltip"
                class="btn btn-success btn-just-icon btn-sm"
                data-original-title=""
                title=""
              >
                <i class="material-icons">EDIT</i>
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>

  <!--Accept person to public team 
  https://learnvue.co/articles/vue-for-loop-tips-->
  <div v-if="publicTeam?.userTeams != null" class="container card mt-3">
    <div class="d-flex row">
      <div class="float-left col">
        <h4><small>People in public teams</small></h4>
      </div>
    </div>
    <div
      class="table-responsive"
      v-for="(userTeam) in publicTeam!.userTeams"
      v-bind:key="userTeam.id!"
    >
      <table class="table">
        <thead>
          <tr>
            <th>CODE</th>
            <th v-if="userTeam.accepted != null">USER</th>
            <th>ACCEPTED</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{{ publicTeam?.code }}</td>
            <td v-if="userTeam.accepted != null">
              {{ userTeam.appUser?.userName }}
            </td>
            <td v-if="userTeam.accepted == false">NOT ACCEPTED</td>
            <td v-else-if="userTeam.accepted == true">ACCEPTED</td>

            <td>
              <button
                v-if="userTeam.accepted == false"
                @click="acceptUser(userTeam.id!, userTeam)"
                type="button"
                rel="tooltip"
                class="btn btn-success btn-just-icon btn-sm"
                data-original-title=""
                title=""
              >
                <i class="material-icons">ACCEPT</i>
              </button>
              <button
                v-else-if="userTeam.accepted == true"
                @click="removeUser(userTeam.id!, userTeam)"
                type="button"
                rel="tooltip"
                class="btn btn-danger btn-just-icon btn-sm"
                data-original-title=""
                title=""
              >
                <i class="material-icons">CANCEL</i>
              </button>
              <button
                v-if="userTeam.accepted == false"
                @click="declineRequest(userTeam.id!)"
                type="button"
                rel="tooltip"
                class="btn btn-danger btn-just-icon btn-sm"
                data-original-title=""
                title=""
              >
                <i class="material-icons">DELETE</i>
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped>
body {
  margin-top: 20px;
  color: #1a202c;
  text-align: left;
  background-color: #e2e8f0;
}
.main-body {
  padding: 15px;
}
.card {
  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06);
}

.card {
  position: relative;
  display: flex;
  flex-direction: column;
  min-width: 0;
  word-wrap: break-word;
  background-color: #fff;
  background-clip: border-box;
  border: 0 solid rgba(0, 0, 0, 0.125);
  border-radius: 0.25rem;
}

.card-body {
  flex: 1 1 auto;
  min-height: 1px;
  padding: 1rem;
}

.gutters-sm {
  margin-right: -8px;
  margin-left: -8px;
}

.gutters-sm > .col,
.gutters-sm > [class*="col-"] {
  padding-right: 8px;
  padding-left: 8px;
}
.mb-3,
.my-3 {
  margin-bottom: 1rem !important;
}

.bg-gray-300 {
  background-color: #e2e8f0;
}
.h-100 {
  height: 100% !important;
}
.shadow-none {
  box-shadow: none !important;
}

.modals {
  width: 300px;
  padding: 30px;
  box-sizing: border-box;
  background-color: #fff;
  font-size: 20px;
  text-align: center;
}
</style>
