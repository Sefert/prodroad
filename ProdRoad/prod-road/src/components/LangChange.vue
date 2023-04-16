<script lang="ts">
import { useI18n } from "vue-i18n";

export default {
  setup() {
    const i18n = useI18n();

    return { i18n };
  },

  methods: {
    onChange() {
      window.localStorage.setItem(
        "prodRoad-locale",
        `${this.i18n.locale.value}`
      );
      console.log(this.i18n.locale.value);
      //window.location.reload();
    },
  },
};
</script>

<template>
  <select
    @change="onChange()"
    class="form-select-sm nav-link dropdown-toggle user-text"
    v-model="$i18n.locale"
  >
    <option class="dropdown-item user-text" value="en-GB">EN</option>
    <!--selected="true"-->
    <option class="dropdown-item user-text" value="et-EE">ET</option>
  </select>
</template>
