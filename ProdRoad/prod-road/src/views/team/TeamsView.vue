
<script lang="ts">
import TopNavBar from "../../components/TopNavBar.vue";
import { Options, Vue } from "vue-class-component";

import { userStore } from "../../stores/identity";
import { teamStore } from "../../stores/team";

import type { ITeam } from "../../domain/ITeam";
import type { IServiceResult } from "../../services/contracts/IServiceResult";

import { TeamService } from "../../services/TeamService";

//import TestModal from "../../components/TestModal.vue"
import CustomModal from "../../components/TestModal.vue"


@Options({
    components: {
        //TestModal,
        CustomModal
    },
    props: {},
    emits: [],
})

export default class TeamsView extends Vue {
    identityStore = userStore();
    teamStore = teamStore();
    teamService = new TeamService();
    editTeamId : string | null = null;
    errorMsg: string | null = null;
    publicTeam: ITeam | null = null;

    show: boolean = false;

    confirm() {
      // some code...
      this.show = false;
    }

    cancel(close) {
      // some code...
      close()
    }

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
        console.log('Team mounted'); 

        if (!this.identityStore.isInRole("manager")){
            this.identityStore.logOut();
        }

        await this.getTeams().then((data : ITeam[])=>{
            this.teamStore.$state.teams = data;
        }).then(() =>{
            this.publicTeam = this.teamStore.getPublicTeam();
        })
    }


    private async getTeams() : Promise<ITeam[]> {
        var res : IServiceResult<ITeam[]> = await this.teamService.getAll();
        console.log(res);
        if (res != null && typeof(res) != "undefined"){
            if (res.status! >= 300) {
                this.errorMsg = res.status + ' ' + res.errorMsg;
                console.log(this.errorMsg);
                } else {
                    var data = res.data;
                    console.log(data);
                }
            return res.data!;      
        }
        return [];
    }
}


</script>


<template>

    <div>
        <custom-modal v-model="show" @confirm="confirm" @cancel="cancel">
            <template v-slot:title>Hello, vue-final-modal</template>
            <p>Vue Final Modal is a renderless, stackable, detachable and lightweight modal component.</p>
        </custom-modal>

        <v-button @click="show = true">Open modal</v-button>
    </div>
    
    <div v-if="publicTeam != null" class="container card mt-3">
        <div class="d-flex row">
            <div class="float-left col"><h4><small>Manage public team connection</small></h4></div>
        </div>
        <div class="table-responsive">
            <table class="table">
                <tbody>
                    <!-- Vertically centered modal -->
                    <tr v-if="publicTeam.id == editTeamId && publicTeam.isPublic == true">
                        <td><input v-model="publicTeam.name"  class="form-control" placeholder="Add new name"></td>
                        <td><input v-model="publicTeam.code"  class="form-control" placeholder="Add new code"></td>
                        <td>
                            <button @click="saveRow(publicTeam!)" type="button" rel="tooltip" class="btn btn-success btn-just-icon btn-sm" data-original-title="" title="">
                                <i class="material-icons">SAVE</i>
                            </button>
                            <button @click="cancelChange(publicTeam!.id)" type="button" rel="tooltip" class="btn btn-danger btn-just-icon btn-sm" data-original-title="" title="">
                                <i class="material-icons">CANCEL</i>
                            </button>
                        </td>
                    </tr>
                    <tr v-else-if="publicTeam.isPublic == true">
                        <td>{{publicTeam.name}}</td>
                        <td>{{publicTeam.code}}</td>
                        <td>
                            <button @click="editRow(publicTeam!.id)" type="button" rel="tooltip" class="btn btn-success btn-just-icon btn-sm" data-original-title="" title="">
                                <i class="material-icons">EDIT</i>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>


    <div class="container card mt-3">
        <div class="d-flex row">
            <div class="float-left col"><h4><small>Teams</small></h4></div>
            <button @click="addNewRow()" type="button" rel="tooltip" class="btn btn-success btn-just-icon btn-sm col-sm-4" data-original-title="" title="">
                <i class="material-icons">ADD NEW</i>
            </button>
        </div>
        <div draggable="true" class="table-responsive card shadow-lg" v-for="team in teamStore.getTeams">
            <table class="table">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Code</th>
                    </tr>
                </thead>
                <tbody v-if="teamStore.getTeams.length != 0" >
                    <!--TODO: fix cant find id null bug  -->
                    <tr v-if="(team.id == null || team.id == editTeamId) && team.isPublic == false">
                        <td><input v-model="team.name"  class="form-control" placeholder="Add new name"></td>
                        <td><input v-model="team.code"  class="form-control" placeholder="Add new code"></td>
                        <td>
                            <button @click="saveRow(team)" type="button" rel="tooltip" class="btn btn-success btn-just-icon btn-sm" data-original-title="" title="">
                                <i class="material-icons">SAVE</i>
                            </button>
                            <button @click="cancelChange(team.id!)" type="button" rel="tooltip" class="btn btn-danger btn-just-icon btn-sm" data-original-title="" title="">
                                <i class="material-icons">CANCEL</i>
                            </button>
                        </td>
                    </tr>
                    <tr v-else-if="team.isPublic == false">
                    
                        <td>{{team.name}}</td>
                        <td>{{team.code}}</td>
                        <td>
                            <button @click="editRow(team.id!)" type="button" rel="tooltip" class="btn btn-success btn-just-icon btn-sm" data-original-title="" title="">
                                <i class="material-icons">EDIT</i>
                            </button>
                            <button @click="deleteRow(team.id!)" type="button" rel="tooltip" class="btn btn-danger btn-just-icon btn-sm" data-original-title="" title="">
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