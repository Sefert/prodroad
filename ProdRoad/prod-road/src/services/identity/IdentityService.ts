import type { IJWTResponse } from "@/domain/IJWTResponse";
import httpCLient from "@/http-client";
import { userStore } from "@/stores/identity";
import type { AxiosError } from "axios";
import type { IServiceResult } from "../contracts/IServiceResult";

//TODO: Refactor login and register
export class IdentityService {
  identityStore = userStore();

  async login(
    email: string,
    password: string
  ): Promise<IServiceResult<IJWTResponse>> {
    try {
      const loginInfo = {
        email,
        password,
      };
      const response = await httpCLient.post(
        "/Identity/Account/Login",
        loginInfo
      );

      console.log(response.status);
      console.log(response.data);

      return {
        status: response.status,
        data: response.data as IJWTResponse,
      };
    } catch (e) {
      const response = {
        status: (e as AxiosError).response!.status,
        //errorMsg: (e as AxiosError).response!.data.error,
      };

      console.log(response);

      console.log((e as AxiosError).response);

      return response;
    }
  }

  async register(
    email: string,
    password: string
  ): Promise<IServiceResult<IJWTResponse>> {
    try {
      const loginInfo = {
        email,
        password,
      };
      const response = await httpCLient.post(
        "/Identity/Account/Register",
        loginInfo
      );

      console.log(response.status);
      console.log(response.data);

      return {
        status: response.status,
        data: response.data as IJWTResponse,
      };
    } catch (e) {
      const response = {
        status: (e as AxiosError).response!.status,
        //errorMsg: (e as AxiosError).response!.data.error,
      };

      console.log(response);

      console.log((e as AxiosError).response);

      return response;
    }
  }

  async refreshIdentity(): Promise<IServiceResult<IJWTResponse>> {
    try {
      console.log("Here");
      console.log(this.identityStore.$state.jwt);

      const response = await httpCLient.post("/identity/account/refreshtoken", {
        token: this.identityStore.$state.jwt?.token,
        refreshToken: this.identityStore.$state.jwt?.refreshToken,
      });
      return {
        status: response.status,
        data: response.data as IJWTResponse,
      };
    } catch (e) {
      const response = {
        status: (e as AxiosError).response!.status,
        //errorMsg: (e as AxiosError).response!.data.error,
      };

      console.log(response);
      console.log((e as AxiosError).response);

      return response;
    }
  }
}
