
<script lang="ts">
import { Options, Vue } from "vue-class-component";

import { userStore } from "../../stores/identity";
import { teamStore } from "../../stores/team";

import type { ITeam } from "../../domain/ITeam";
import type { IServiceResult } from "../../services/contracts/IServiceResult";

import { TeamService } from "../../services/TeamService";

import { UserTeamService } from "@/services/UserTeamService";
import type { IUserTeam } from "@/domain/IUserTeam";
import router from "@/router";


@Options({
    components: {
        //TestModal,
        //CustomModal
    },
    props: {
    },
    emits: [],
})

export default class TeamsView extends Vue {
    identityStore = userStore();
    teamStore = teamStore();
    teamService = new TeamService();
    userTeamService = new UserTeamService
    editTeamId : string | null = null;
    errorMsg: string | null = null;
    publicTeam: ITeam | null = null;

    show: boolean = false;


    addNewRow(){
        if (this.editTeamId == null){
            
            var team: ITeam = {
                id : null,
                name: null,
                code: null,
                isPublic: false
            }
            this.editTeamId = "newTeam";
            this.teamStore.add(team);
        }
    }

    async saveRow(team : ITeam){
        console.log('Save row');
        //change because of html check
        if (team.id == null){
            team.id = "newTeam";
        }

        team.AppUserId = this.identityStore.$id;

        var res : IServiceResult<ITeam> | IServiceResult<void>;
        if (team.id == "newTeam" || team.id == "PublicTeam"){
            this.teamStore.delete("newTeam");
            //accepts with no id only
            var teamToAdd : ITeam = {
                name : team.name,
                code : team.code,
                isPublic : (team.id == "PublicTeam") ? true : false,
            }
            res = await this.teamService.add(teamToAdd);
            console.log("here-st");
        } else {
            //TODO: should reload when fail or revert back
            res = await this.teamService.edit(team.id,team);
        }
        console.log("here-st3");
        if (res != null && typeof(res) != "undefined"){
            console.log("here-st34");
            console.log(res);
            console.log(res.status);
            if (res.status! >= 300) {
                console.log("here-st2");
                    this.errorMsg = res.status + ' ' + res.errorMsg;
                    console.log(this.errorMsg);
            } else if (res.status == 201){
                var data = res.data;
                if (data != null && typeof(data) != 'undefined'){
                    this.teamStore.add(data);
                }
                this.cancelChange("newTeam");
            }
        }

        this.editTeamId = null;
    }

    editRow(id : string){
        this.editTeamId = id;
    }

    async deleteRow(id : string){
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
    }

    cancelChange(id : string){
        if (id == null){          
            this.teamStore.delete(id);
        }
        this.editTeamId = null;
    }

    async mounted(): Promise<void> {
        console.log('Personsview mounted'); 

        this.publicTeam = this.teamStore.getPublicTeam();

        if (!this.identityStore.isInRole("manager")){
            this.identityStore.logOut();
        }

    }
}


</script>


<template> 
    <!--Accept person to public team -->
     <div class="d-flex row">
        <div class="float-left col"><h4><small>People in public teams</small></h4></div>
    </div>
    <div v-if="publicTeam?.userTeams != null" class="container mt-3">
        <div class="container" v-for="userTeam in publicTeam!.userTeams">
            <div v-if="userTeam.accepted == true" class="table-responsive card">
                <table draggable="true" class="table">
                    <thead>
                        <tr>
                            <th>USER</th>
                        </tr>
                    </thead>
                    <tbody >
                        <tr>
                            <td>{{userTeam.appUser?.userName}}</td>                   
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<style scoped>  
body{
    margin-top:20px;
    color: #1a202c;
    text-align: left;
    background-color: #e2e8f0;    
}
.main-body {
    padding: 15px;
}
.card {
    box-shadow: 0 1px 3px 0 rgba(0,0,0,.1), 0 1px 2px 0 rgba(0,0,0,.06);
}

.card {
    position: relative;
    display: flex;
    flex-direction: column;
    min-width: 0;
    word-wrap: break-word;
    background-color: #fff;
    background-clip: border-box;
    border: 0 solid rgba(0,0,0,.125);
    border-radius: .25rem;
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

.gutters-sm>.col, .gutters-sm>[class*=col-] {
    padding-right: 8px;
    padding-left: 8px;
}
.mb-3, .my-3 {
    margin-bottom: 1rem!important;
}

.bg-gray-300 {
    background-color: #e2e8f0;
}
.h-100 {
    height: 100%!important;
}
.shadow-none {
    box-shadow: none!important;
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