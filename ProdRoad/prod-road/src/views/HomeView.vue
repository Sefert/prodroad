<script setup lang="ts">
import TopNavBar from "../components/TopNavBar.vue"
import RightBar from "../components/RightBar.vue"

import { Options, Vue } from "vue-class-component";



/*@Options({
    components: {
      TopNavBar,
      RightBar,
    },
    props: {},
    emits: [],
})
export class HomeView extends Vue {}*/
</script>

<template>

  <TopNavBar/>
  <RightBar />
</template>
