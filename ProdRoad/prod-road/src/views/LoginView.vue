<i18n>
{
  "en": {
    "hello": "hello world!"
  },
  "et": {
    "hello": "tere maailm!"
  }
}
</i18n>

<script lang="ts">
    import { Vue } from "vue-class-component";
    import { userStore } from "../stores/identity";
    import {IdentityService } from "../services/identity/IdentityService"
    import type { IJWTResponse } from "../domain/IJWTResponse";
    import jwt_decode from "jwt-decode";
    import type { IServiceResult } from "../services/contracts/IServiceResult";
    
    
    export default class Login extends Vue {
      identityStore = userStore();

      email: string = '';
      password: string = '';
      repeatedPassword: string = '';
      errorMsg: string | null = null;
      isRegister: boolean = false;
      //jwtResp: IJWTResponse = null;


      identityService = new IdentityService();

      /*setup() {
        const { locale, t } = useI18n({
          inheritLocale: true
        })
        return { locale, t }
      }*/

      //TODO: move magic strings to properties
      async loginClicked(): Promise<void> {
        //console.log('submitClicked');
        //console.log(this.email);
        
        var res = await this.identityService.login(this.email, this.password);
        //console.log(res);       
        
        //TODO: route if login succeeded and inform user       
        if (res.status == 200) {
          this.saveStateData(res);
          this.$router.push({name:'Home'})      
        };
      };

      async registerClicked(): Promise<void> {
        console.log('submitClicked');
        console.log(this.email);

        //TODO: move out from registerClicked method
        if (this.password != this.repeatedPassword || this.password == '') {
            this.errorMsg = "Entered passwords do not match!"
        } else {
          var res = await this.identityService.register(this.email, this.password);
          //console.log(res);       

          //TODO: route if register succeeded and inform user     
          if (res.status == 200) {
            this.saveStateData(res);
            this.$router.push({name:'Home'})
          };
        };
        

      };

      mounted(){
        console.log(this.identityStore.$state.jwt);
      }

      saveStateData(result: IServiceResult<IJWTResponse>){
        if (result.data != null) {
        this.identityStore.$state.jwt = result.data;
        
        var decoded = jwt_decode(result.data.token);

        this.identityStore.$id = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
        this.identityStore.$state.email = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        this.identityStore.$state.role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        this.identityStore.$state.jwtExp = decoded["exp"];

        console.log(this.identityStore.$state.jwt);
        console.log(this.identityStore.$id);
        console.log(this.identityStore.$state.email);
        console.log(this.identityStore.$state.role);
        console.log(this.identityStore.$state.jwtExp);

        //TODO: make more secure!!!!
        window.localStorage.setItem("prodRoad-r", result.data.refreshToken);
        window.localStorage.setItem("prodRoad-j", result.data.token);
        }
      }
    }

</script>


<!-- TODO: needs more intuitive error message -->
<template>
    <div class="text-center">
        <div class="form-signin">
            <form>
                <img class="mb-4" src="None" alt="" width="72" height="57">
                <h1 class="h3 mb-3 fw-normal">Please sign in {{ $t('hello') }}</h1>
                <p v-if="errorMsg != null" class="text-danger h3 mb-3 fw-normal">{{errorMsg}}</p>
                <div class="form-floating">
                <input v-model="email" type="email" class="form-control" id="floatingInput" placeholder="name@example.com">
                <label for="floatingInput">Email address</label>
                </div>
                <div class="form-floating">
                <input v-model="password" type="password" class="form-control" id="floatingPassword" placeholder="Password">
                <label for="floatingPassword">Password</label>
                </div>
                <div v-if="isRegister == true" class="form-floating">
                <input v-model="repeatedPassword" type="password" class="form-control" placeholder="Password">
                <label for="floatingPassword">Repeat Password</label>
                </div>

                <div class="checkbox mb-3">
                <label>
                    <input v-model="isRegister" type="checkbox" value="true"> Register
                </label>
                </div>
                <!-- TODO: button type submit fuckes things up why?-->
                <button v-if="isRegister == false" @click="loginClicked()" class="w-100 btn btn-lg btn-primary" type="button">Sign in</button>
                <button v-if="isRegister == true" @click="registerClicked()" class="w-100 btn btn-lg btn-primary" type="button">Register</button>
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


</style>
