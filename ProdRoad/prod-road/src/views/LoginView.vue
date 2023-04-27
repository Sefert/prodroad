<script lang="ts">
import { userStore } from "../stores/identity";
import { IdentityService } from "../services/identity/IdentityService";
import type { IJWTResponse } from "../domain/IJWTResponse";
import jwt_decode from "jwt-decode";
import type { IServiceResult } from "../services/contracts/IServiceResult";
import LangChange from "../components/LangChange.vue";
import ErrorParagraph from "../components/errors/ErrorParagraph.vue";
import { ref, toRef, watch } from "vue";
import type { PropType } from "vue";
import type { IJWT } from "@/domain/IJWT";
import { useI18n } from "vue-i18n";
import type { IUserTeam } from "@/domain/IUserTeam";
import { teamStore } from "@/stores/team";
import { UserTeamService } from "@/services/UserTeamService";

export default {
  components: {
    LangChange,
    ErrorParagraph,
  },

  /*TODO: make an interface for it */
  /*props: {
    email: { type: String, default: "" },
    password: { type: String, default: "" },
    repeatedPassword: { type: String, default: "" },
    //https://stackoverflow.com/questions/59125043/vuejs-using-prop-type-validation-with-null-and-undefined-values
    errorMsg: {
      type: null as unknown as PropType<string | null>,
      default: null,
    },
  },*/

  /*emits: [
    "update:email",
    "update:password",
    "update:repeatedPassword",
    "update:errorMsg",
    "update:isNotRegistered",
  ],*/

  setup(props, context) {
    //https://stackoverflow.com/questions/64775876/vue-3-pass-reactive-object-to-component-with-two-way-binding
    const identityStore = ref(userStore());
    const identityService = new IdentityService();
    const userteamStore = ref(teamStore());
    const userTeamService = new UserTeamService();

    const i18n = useI18n();

    const isNotRegistered = ref<boolean>(false);
    const email = ref<string>("");
    const password = ref<string>("");
    const repeatedPassword = ref<string>("");
    const errorMsg = ref<string | null>(null);

    /*const register = () => {
      context.emit("update:isNotRegistered", !props.isNotRegistered);
      console.log(props.isNotRegistered);
    }; */

    /*const register = () => {
      isNotRegistered.value = !isNotRegistered.value;
    };*/
    const setEmail = (message: string) => {
      email.value = message;
      console.log(message);
    };
    const setErrorMsg = (message: string) => {
      errorMsg.value = message;
    };

    //https://stackoverflow.com/questions/66753488/vue-3-call-emit-on-variable-change
    /*watch(props, (newVal) => {
      context.emit("update:isNotRegistered", { newVal });
      console.log(props.isNotRegistered);
    }); */

    return {
      identityStore,
      identityService,
      userteamStore,
      isNotRegistered,
      email,
      password,
      repeatedPassword,
      errorMsg,
      i18n,
      setEmail,
      setErrorMsg,
      userTeamService,
    };
  },

  methods: {
    //TODO: move magic strings to properties
    async loginClicked(): Promise<void> {
      console.log("submitClicked");

      const res = await this.identityService.login(this.email, this.password);

      //TODO: route if login succeeded and inform user
      if (res.status == 200) {
        await this.saveStateData(res)
          .then(async () => {
            console.log("connect to teams");
            //
            const uTres: IServiceResult<IUserTeam[]> =
              await this.userTeamService.getAll();
            if (uTres.data != null) {
              this.userteamStore.$state.userTeams = uTres.data;
            } else {
              this.userteamStore.$state.teams = [];
            }
            console.log(this.userteamStore.$state.userTeams);
          })
          .then(() => {
            this.$router.push({ name: "Profile" });
          });
      } else {
        //TODO: how to propagate
        this.errorMsg = "ERRORS.login-fail-message";
        console.log(this.errorMsg);
      }
    },

    async registerClicked(): Promise<void> {
      console.log("submitClicked");
      console.log(this.email);

      //TODO: move out from registerClicked method
      if (this.password != this.repeatedPassword || this.password == "") {
        this.errorMsg = "ERRORS.passwords-do-not match"; //this.i18n.t("ERRORS.passwords-do-not match");
        //this.errorMsg = "Entered passwords do not match!";
      } else {
        const res = await this.identityService.register(
          this.email,
          this.password
        );

        //TODO: route if register succeeded and inform user
        if (res.status == 200) {
          await this.saveStateData(res).then(() => {
            this.$router.push({ name: "Profile" });
          });
        }
      }
    },

    async saveStateData(result: IServiceResult<IJWTResponse>) {
      if (result.status == 200) {
        if (result.data != null) {
          this.identityStore.$state.jwt = result.data;
          if (result.data.token != null && result.data.refreshToken != null) {
            window.localStorage.setItem("prodRoad-r", result.data.refreshToken);
            window.localStorage.setItem("prodRoad-j", result.data.token);
            //https://stackoverflow.com/questions/61199530/typescript-error-with-accessing-jwt-decode-object
            const decoded = jwt_decode<IJWT>(result.data.token);
            this.identityStore.$id =
              decoded[
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
              ];
            this.identityStore.$state.email =
              decoded[
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
              ];
            this.identityStore.$state.role =
              decoded[
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
              ];
            this.identityStore.$state.jwtExp = decoded["exp"];
          } else {
            //TODO: not implemented;
          }
        } else {
          //TODO: not implemented!;
        }
      } else {
        window.localStorage.removeItem("prodRoad-r");
        window.localStorage.removeItem("prodRoad-j");

        this.identityStore.$state.jwt = null;
        this.identityStore.$id = "";
        this.identityStore.$state.email = null;
        this.identityStore.$state.role = [];
        this.identityStore.$state.jwtExp = null;
      }
    },
  },
};
</script>

<!-- TODO: needs more intuitive error message 
https://eslint.vuejs.org/rules/no-mutating-props.html-->
<!-- @input="getEmail()
  @get-email="email"
https://dev.to/denisseab/how-to-emit-a-value-from-an-input-component-vue-3-1lla
@input="register()"
<input @change="register()" type="checkbox" 
 @change="setEmail(($event.target as HTMLInputElement).value)"/>-->
<template>
  <LangChange />
  <div class="text-center">
    <div class="form-signin">
      <form>
        <img
          style="visibility: hidden"
          class="mb-4"
          src="None"
          alt=""
          width="72"
          height="57"
        />
        <h1 class="h3 mb-3 fw-normal">{{ $t("LoginView.sign-in-sign") }}</h1>
        <div v-if="errorMsg != null">
          <ErrorParagraph v-model:error-msg="errorMsg" />
        </div>
        <!--<p v-if="errorMsg != null" class="text-danger h3 mb-3 fw-normal">
          {{ errorMsg }}
        </p>-->

        <div class="form-floating">
          <input
            v-model="email"
            type="email"
            class="form-control"
            id="floatingInput"
            placeholder="name@example.com"
            :required="true"
          />
          <label for="floatingInput">{{ $t("LoginView.email") }}</label>
        </div>
        <div class="form-floating">
          <input
            v-model="password"
            type="password"
            class="form-control"
            id="floatingPassword"
            placeholder="Password"
          />
          <label for="floatingPassword">{{ $t("LoginView.password") }}</label>
        </div>
        <div v-if="isNotRegistered == true" class="form-floating">
          <input
            v-model="repeatedPassword"
            type="password"
            class="form-control"
            placeholder="Password"
          />
          <label for="floatingPassword">{{
            $t("LoginView.repeat-password")
          }}</label>
        </div>
        <div class="checkbox mb-3">
          <label>
            <input v-model="isNotRegistered" type="checkbox" />
            {{ $t("LoginView.register-radio") }}
          </label>
        </div>
        <!-- TODO: button type submit fuckes things up why?-->
        <button
          v-if="isNotRegistered == false"
          @click="loginClicked()"
          class="w-100 btn btn-lg btn-color"
          type="button"
        >
          {{ $t("LoginView.sign-in-button") }}
        </button>
        <button
          v-if="isNotRegistered == true"
          @click="registerClicked()"
          class="w-100 btn btn-lg btn-color"
          type="button"
        >
          {{ $t("LoginView.sign-up-button") }}
        </button>
        <p class="mt-5 mb-3 text-muted">© ME</p>
      </form>
    </div>
  </div>
</template>

<style scoped>
html,
body {
  height: 100%;
}

body {
  display: flex;
  align-items: center;
  padding-top: 40px;
  padding-bottom: 40px;
  background-color: #f5f5f5;
}

.form-signin {
  width: 100%;
  max-width: 330px;
  padding: 15px;
  margin: auto;
}

.form-signin .checkbox {
  font-weight: 400;
}

.form-signin .form-floating:focus-within {
  z-index: 2;
}

.form-signin input[type="email"] {
  margin-bottom: -1px;
  border-bottom-right-radius: 0;
  border-bottom-left-radius: 0;
}

.form-signin input[type="password"] {
  margin-bottom: 10px;
  border-top-left-radius: 0;
  border-top-right-radius: 0;
}

.btn-color {
  background-color: #e4dccf;
}
.btn:hover {
  background-color: #f4b183;
  box-shadow: inset 0px 10px 10px #dbe4c6;
}
</style>
