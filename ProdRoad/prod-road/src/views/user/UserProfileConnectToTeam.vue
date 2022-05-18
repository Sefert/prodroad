<script lang="ts">
import TopNavBar from "@/components/TopNavBar.vue";
import type { ITeam } from "@/domain/ITeam";
import type { IUserTeam } from "@/domain/IUserTeam";
import { TeamService } from "@/services/TeamService";
import { UserTeamService } from "@/services/UserTeamService";
import { teamStore } from "@/stores/team";
import { Options, Vue } from "vue-class-component";
import { userStore } from "../../stores/identity";

@Options({
    components: {
    TopNavBar,
    },
    props: {},
    emits: [],
})
export default class UserProfileConnectToTeam extends Vue {
    identity = userStore();
    teamStore = teamStore();
    teamService = new TeamService();
    userTeamService = new UserTeamService();
    errorMsg : null | string = null;
    teamCode : string = "";

    askJoin : boolean = false;
    //isJoined : boolean | null = false;

    publicUserTeams : IUserTeam[] = [];


    async getPublicTeam() : Promise<void> {
        this.teamStore.$state.teams = [];
        var res = await this.teamService.getPublicTeam(this.teamCode);

        if (res != null && typeof(res) != "undefined"){
            if (res.status! >= 300) {
                this.errorMsg = res.status + ' ' + res.errorMsg;
                console.log(this.errorMsg);
                } else {
                    var data = res.data;
                    console.log(data);
                    this.teamStore.$state.teams.push(data!); 
                    var uT = this.teamStore.$state.teams[0].userTeams;
                    this.publicUserTeams = (uT == null) ? [] : uT;
                    console.log(this.teamStore.$state.teams);
                    console.log(this.publicUserTeams);
                    console.log(this.teamStore.$state.teams[0]);
                    console.log(this.teamStore.$state.teams[0].userTeams);
                }            
        }     
    }

    async joinPublicTeam(team : ITeam) : Promise<void> {
        console.log(this.identity.$id)
        console.log(team.id)
        var userTeam : IUserTeam = {
            AppUserId : this.identity.$id,
            TeamId : team.id,
            Accepted : false
        };

        var res = await this.userTeamService.add(userTeam);

        if (res != null && typeof(res) != "undefined"){
            if (res.status! >= 300) {
                this.errorMsg = res.status + ' ' + res.errorMsg;
                console.log(this.errorMsg);
                } else {
                    var data : IUserTeam = res.data!;
                    this.publicUserTeams = [data];
                    console.log("Here");
                    console.log(res);
                    console.log(data);
                }             
        }       
    }

    async leavePublicTeam(id:string) : Promise<void> {
        console.log(id)
        /*console.log(team.id)
        var userTeam : IUserTeam = {
            AppUserId : this.identity.$id,
            TeamId : team.id,
            Accepted : false
        };*/

        var res = await this.userTeamService.delete(id);

        /*TODO:check if got really deleted*/
        if (res != null && typeof(res) != "undefined"){
            if (res.status! >= 300) {
                this.errorMsg = res.status + ' ' + res.errorMsg;
                console.log(this.errorMsg);
                } else {
                    this.publicUserTeams = [];
                }             
        } 
    }

    //TODO: ask for only public userteam
    async mounted(){
        console.log("connect to teams");
        this.teamStore.$state.teams = [];
        /*this.teamStore.$state.teams= [];
        var res = await this.userTeamService.getAll();
        var userTeams : IUserTeam[] | null = res.data;*/

    }

}

</script>

<template>
    <TopNavBar />

    <div class="container">
        <div class="row height d-flex justify-content-center align-items-center">

            <div class="col-md-8">

                <div class="search">
                    <i class="fa fa-search"></i>
                    <input v-model="teamCode" type="text" class="form-control" placeholder="Insert given CODE">
                    <button @click="getPublicTeam()" type="button" class="btn btn-primary">Search</button>
                </div>
                
            </div>
                
        </div>

        <br/>
        <br/>

        <div v-if="publicUserTeams.length != 0 && teamStore.$state.teams.length != 0" class="form-check form-switch">
            <p>Leave TEAM:</p>
            <input checked  @change="leavePublicTeam(publicUserTeams![0].id!)" type="checkbox" class="form-check-input">
            <label class="form-check-label" for="flexSwitchCheckDefault">{{teamStore.$state.teams[0].code}}</label>
        </div>
        <div v-else-if="teamStore.$state.teams.length != 0" class="form-check form-switch">
            <p>Join TEAM:</p>
            <input @change="joinPublicTeam(teamStore.$state.teams[0])" type="checkbox" class="form-check-input">
            <label class="form-check-label" for="flexSwitchCheckDefault">{{teamStore.$state.teams[0].code}}</label>
        </div>
    </div>

</template>