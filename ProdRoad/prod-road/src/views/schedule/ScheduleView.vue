<script lang="ts" setup>
import { ref } from "vue";
//https://stackoverflow.com/questions/40616272/an-import-path-cannot-end-with-ts-nodejs-and-visual-code
import type { IBar } from "@/domain/IBar";
import { uuid } from "vue-uuid";

const myBarList = ref<IBar[]>([]);
const addNewBar = () => {
  const bar = {
    myBeginDate: "2021-04-17 17:00",
    myEndDate: "2021-04-18 03:00",
    ganttBarConfig: {
      id: uuid.v1(), // make sure this is unique!
      label: "TEXT",
      hasHandles: true,
    },
  };
  //https://www.explainprogramming.com/typescript/never-type/
  myBarList.value.push(bar);
};
</script>

<template>
  <div class="scrollable">
    <g-gantt-chart
      chart-start="2021-04-17 00:00"
      chart-end="2021-04-21 23:59"
      precision="hour"
      width="300%"
      bar-start="myBeginDate"
      bar-end="myEndDate"
      grid="true"
      row-height="60"
    >
      <g-gantt-row label="Assembly" :bars="myBarList" />
      <g-gantt-row label="Assembly2" />
    </g-gantt-chart>
  </div>
  <button
    type="button"
    rel="tooltip"
    class="btn btn-success btn-just-icon btn-sm col-sm-4 btnheight"
    @click="addNewBar"
  >
    ADD
  </button>
</template>

<!--https://stackoverflow.com/questions/66229852/how-to-add-scrollbars-to-vue-and-vuetify#:~:text=Try%20adding%20overflow%2Dy%20to,elements%20you%20want%20to%20scroll-->
<style scoped>
.scrollable {
  overflow-x: auto;
}
.btnheigth {
  height: 300px;
}
</style>
