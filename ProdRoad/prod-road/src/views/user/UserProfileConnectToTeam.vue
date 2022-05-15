<script lang="ts">
import TopNavBar from "@/components/TopNavBar.vue";
import { TeamService } from "@/services/TeamService";
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
    errorMsg : null | string = null;
    teamCode : string = "";


    async getPublicTeam() : Promise<void> {
        var res = await this.teamService.getPublicTeam(this.teamCode);

        if (res != null && typeof(res) != "undefined"){
            if (res.status! >= 300) {
                this.errorMsg = res.status + ' ' + res.errorMsg;
                console.log(this.errorMsg);
                } else {
                    var data = res.data;
                    console.log(data);
                    this.teamStore.$state.teams.push(data!);  
                    console.log(this.teamStore.$state.teams);
                }
               
        }
        
    }

    mounted(){
        console.log("connect to teams")
        this.teamStore.$state.teams=[];  
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

        <div v-if="teamStore.$state.teams.length != 0" class="form-check form-switch">
            <p>Join TEAM:</p>
            <input v-model="isJoined" type="checkbox" value="true" class="form-check-input" type="checkbox">
            <label class="form-check-label" for="flexSwitchCheckDefault">{{teamStore.$state.teams[0].code}}</label>
        </div>
    </div>

</template>