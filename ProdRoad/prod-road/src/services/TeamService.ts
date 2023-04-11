import type { AxiosError, AxiosResponse } from "axios";
import type { ITeam } from "@/domain/ITeam";
import httpCLient from "@/http-client";
import { BaseService } from "./BaseService";
//import { teamStore } from "../stores/team";
import type { IServiceResult } from "./contracts/IServiceResult";
import { IdentityService } from "./identity/IdentityService";
//import { useI18n } from "vue-i18n";

export class TeamService extends BaseService<ITeam> {
  constructor() {
    super("Team");
  }

  async getPublicTeam(code: string): Promise<IServiceResult<ITeam>> {
    console.log("getAll");
    let response: AxiosResponse;
    //var respData : TEntity[] ;
    let serviceResult: IServiceResult<ITeam> = {};

    try {
      response = await httpCLient.get(
        `/Team/GetPublicTeam/${code}?culture=${this.i18n.locale}`,
        {
          headers: {
            Authorization: "bearer " + this.identityStore.$state.jwt?.token,
          },
        }
      );

      console.log(response);

      serviceResult = {
        status: response.status,
        data: response.data as ITeam,
      };
    } catch (e) {
      response = (e as AxiosError).response!;
      if (response.status == 401 && this.identityStore.jwt) {
        const identityService = new IdentityService();
        const refreshResponse = await identityService.refreshIdentity();
        this.identityStore.$state.jwt = refreshResponse.data!;

        if (!this.identityStore.$state.jwt) {
          serviceResult = {
            status: response.status,
            errorMsg: response.data.error,
          };
        } else {
          try {
            response = await httpCLient.get(
              `/Team/GetPublicTeam/${code}?culture=${this.i18n.locale}`,
              {
                headers: {
                  Authorization:
                    "bearer " + this.identityStore.$state.jwt?.token,
                },
              }
            );
            serviceResult = {
              status: response.status,
              data: response.data as ITeam,
            };
          } catch (e) {
            response = (e as AxiosError).response!;
            serviceResult = {
              status: response.status,
              errorMsg: response.data.error,
            };
          }
          console.log(response);
        }
      }
    }
    return serviceResult;
  }
}
