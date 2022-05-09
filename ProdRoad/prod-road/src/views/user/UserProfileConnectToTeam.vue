<script lang="ts">
import TopNavBar from "@/components/TopNavBar.vue";
import type { ITeam } from "@/domain/ITeam";
import type { IServiceResult } from "@/services/contracts/IServiceResult";
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

    async getPublicTeam(code : string) : Promise<ITeam> {
        var res = await this.teamService.getPublicTeam(code);

        if (res != null && typeof(res) != "undefined"){
            if (res.status! >= 300) {
                this.errorMsg = res.status + ' ' + res.errorMsg;
                console.log(this.errorMsg);
                } else {
                    var data = res.data;
                    console.log(data);
                }
               
        }
        return res.data!;  
    }

}

</script>

<template>
    <TopNavBar />

    <div class="input-group">
        <div id="search-autocomplete" class="form-outline">
            <input type="search" id="form1" class="form-control" />
            <label class="form-label" for="form1">Search</label>
        </div>
        <button type="button" class="btn btn-primary">
            <i class="fas fa-search"></i>
        </button>
    </div>
</template>