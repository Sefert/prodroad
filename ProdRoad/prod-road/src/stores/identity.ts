import type { IJWTResponse } from "@/domain/IJWTResponse"
import { defineStore } from "pinia"


export const userStore = defineStore({
  id: "",
  state: () => ({
    jwt: null as IJWTResponse | null,
    email: null as string | null,
    role: [] as string[],
    jwtExp: null as number | null,
  }),
  getters: {
    getJWT(): IJWTResponse | null {
      return this.jwt;
    }
  },
  actions: {
    isInRole(access: string) : boolean {
      var exist : boolean = false;
      if (typeof(this.role) == "string"){
        if (this.role == access){
          exist = true;
        }
      } else {
        var index  = this.role.findIndex(role => role == access);
        if (index >= 0) {
          exist = true;
        }
      }
      
      return exist;
    }
  },

});