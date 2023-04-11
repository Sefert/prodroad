<script lang="ts">
import type { PropType } from "vue";

export default {
  props: {
    errorMsg: {
      type: null as unknown as PropType<string | null>,
      default: null,
    },
  },

  emits: ["update:errorMsg"],

  setup(props, { emit }) {
    const setErrorMessage = (message: string | null) => {
      emit("update:errorMsg", message);
      console.log(props.errorMsg);
      console.log('HERE');
    };
    return {
      setErrorMessage,
    };
  },
};
</script>

<!--@change="setErrorMessage(($event.target as HTMLInputElement).value)"
  https://stackoverflow.com/questions/44321326/property-value-does-not-exist-on-type-eventtarget-in-typescript -->
<template>
  <p
    class="flex error"
    :value="errorMsg"
    @change="
      $emit('update:errorMsg', ($event.target as HTMLInputElement).value)
    "
  >
    {{ $t(`${errorMsg}`) }}
  </p>
</template>

<style scoped>
.error {
  text-emphasis-color: 100%;
  color: red;
}
</style>
