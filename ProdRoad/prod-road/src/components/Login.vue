<script lang="ts">
    import { Options, Vue } from "vue-class-component";
    import { userStore } from "../stores/identity";
    import {IdentityService } from "../services/identity/IdentityService"
    import { RouterLink } from "vue-router";

    export default class Login extends Vue {
        identityStore = userStore();

        email: string = '';
        password: string = '';
        errorMsg: string | null = null;


        identityService = new IdentityService();

      async loginClicked(): Promise<void> {
        console.log('submitClicked');
        console.log(this.email);
        
        var res = await this.identityService.login(this.email, this.password);
        console.log(res);

        this.identityStore.$state.jwt = res.data!;

        //TODO: route if login succeeded
        
        if (res.status == 200) {
          this.$router.push({name:'Home'})
        }
      }

      mounted(){
        console.log(this.identityStore.$state.jwt);
      }
    }

</script>


<template>
    <div class="text-center">
        <div class="form-signin">
            <form>
                <img class="mb-4" src="None" alt="" width="72" height="57">
                <h1 class="h3 mb-3 fw-normal">Please sign in</h1>

                <div class="form-floating">
                <input v-model="email" type="email" class="form-control" id="floatingInput" placeholder="name@example.com">
                <label for="floatingInput">Email address</label>
                </div>
                <div class="form-floating">
                <input v-model="password" type="password" class="form-control" id="floatingPassword" placeholder="Password">
                <label for="floatingPassword">Password</label>
                </div>

                <div class="checkbox mb-3">
                <label>
                    <input type="checkbox" value="remember-me"> Remember me
                </label>
                </div>
                <!-- TODO: button type submit fuckes things up why?-->
                <button @click="loginClicked()" class="w-100 btn btn-lg btn-primary" type="button">Sign in</button>
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
