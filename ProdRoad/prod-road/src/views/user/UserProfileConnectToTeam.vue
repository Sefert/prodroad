<script lang="ts">
import TopNavBar from "@/components/TopNavBar.vue";
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

}

</script>

<template>
    <TopNavBar />

    <div class="input-group">
        <div class="form-outline">
            <input type="search" id="form1" class="form-control" />
            <label class="form-label" for="form1">Search</label>
        </div>
        <button type="button" class="btn btn-primary">
            <i class="fas fa-search"></i>
        </button>
    </div>
</template>