<script lang="ts">
import { Vue } from "vue-class-component";
import { useI18n } from "vue-i18n";

export default class TopNavBar extends Vue {
    i18n= useI18n();
    
    onChange(){
        window.localStorage.setItem("prodRoad-locale", `${this.i18n.locale}`); 
        window.location.reload(); 
    }
}
</script>

<template>
    <select @change="onChange()" class="form-select-sm" v-model="$i18n.locale">
        <option value="en-GB">EN</option> <!--selected="true"-->
        <option value="et-EE">ET</option>
    </select>
</template>