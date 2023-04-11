<template>
  <p>
    <button class="btn btn-success btn-just-icon btn-sm" @click="showModal">
      Show modal
    </button>
  </p>
  <Modals
    v-model="isShow"
    :close="closeModal"
    @before-enter="beforeEnter"
    @after-enter="afterEnter"
    @before-leave="beforeLeave"
    @after-leave="afterLeave"
  >
    <div class="modals">
      <p>Hello</p>
      <button class="btn btn-success btn-just-icon btn-sm" @click="closeModal">
        close
      </button>
    </div>
  </Modals>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";

export default defineComponent({
  setup() {
    const isShow = ref(false);

    function showModal() {
      isShow.value = true;
    }

    function closeModal() {
      isShow.value = false;
    }

    function beforeEnter() {
      console.log("before enter");
    }

    function afterEnter() {
      console.log("after enter");
    }

    function beforeLeave() {
      console.log("before leave");
    }

    function afterLeave() {
      console.log("after leave");
    }

    return {
      isShow,
      showModal,
      closeModal,
      beforeEnter,
      afterEnter,
      beforeLeave,
      afterLeave,
    };
  },
});
</script>
