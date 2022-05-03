<script lang="ts">
import TopNavBar from "@/components/TopNavBar.vue";
import { Options, Vue } from "vue-class-component";
import { userStore } from "../../stores/identity";
import type { ITeam } from "../../domain/ITeam";

@Options({
    components: {
      TopNavBar,
    },
    props: {},
    emits: [],
})
export default class TeamsView extends Vue {
    identity = userStore();
    teams: ITeam[] = [];

    addNewRow(){
        var team: ITeam = {
            id : "teamInEdit",
            name: null,
            code: null,
        }
        this.teams.push(team);
    }

    deleteRow(id : string){
        var index : number = this.teams.map(function(team) {
            return team.id;
        }).indexOf(id);

        this.teams.splice(index, 1);
    }

    saveRow(){

    }
}
</script>

<template>
    <TopNavBar />
    <div class="container card mt-3">
        <div class="d-flex row">
            <div class="float-left col"><h4><small>Teams</small></h4></div>
            <button @click="addNewRow()" type="button" rel="tooltip" class="btn btn-success btn-just-icon btn-sm col-sm-4" data-original-title="" title="">
                <i class="material-icons">ADD NEW</i>
            </button>
        </div>
        <div class="table-responsive ">
            <table class="table">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Code</th>
                    </tr>
                </thead>
                <tbody v-for="team in teams">
                    <tr v-if="team.id == 'teamInEdit'">
                        <td><input v-model="team.name"  class="form-control" placeholder="Add new name"></td>
                        <td><input v-model="team.code"  class="form-control" placeholder="Add new code"></td>
                        <td>
                            <button @click="saveRow()" type="button" rel="tooltip" class="btn btn-success btn-just-icon btn-sm" data-original-title="" title="">
                                <i class="material-icons">SAVE</i>
                            </button>
                            <button @click="deleteRow()" type="button" rel="tooltip" class="btn btn-danger btn-just-icon btn-sm" data-original-title="" title="">
                                <i class="material-icons">CANCEL</i>
                            </button>
                        </td>
                    </tr>
                    <tr v-else>
                        <td>{team.name}</td>
                        <td>{team.code}</td>
                        <td>
                            <button @click="editRow()" type="button" rel="tooltip" class="btn btn-success btn-just-icon btn-sm" data-original-title="" title="">
                                <i class="material-icons">EDIT</i>
                            </button>
                            <button @click="deleteRow()" type="button" rel="tooltip" class="btn btn-danger btn-just-icon btn-sm" data-original-title="" title="">
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
</style>