import type { AxiosError, AxiosResponse } from 'axios';
import type { IUserTeam } from "@/domain/IUserTeam";
import httpCLient from "@/http-client";
import { BaseService } from "./BaseService";
import type { IServiceResult } from './contracts/IServiceResult';
import { IdentityService } from './identity/IdentityService';

export class UserTeamService extends BaseService<IUserTeam> {
    
    constructor() {
        super("UserTeam");
    }

    async manageUserTeam(code : string) : Promise<IServiceResult<IUserTeam>> {
        console.log("getAll");
        var response : AxiosResponse;
        //var respData : TEntity[] ;
        var serviceResult : IServiceResult<IUserTeam> = {};
        
        try {
            response = await httpCLient.post(`/UserTeam/ManageTeam?culture=${this.i18n.locale}`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }
            });
            
            console.log(response);

            serviceResult = {
                status: response.status,
                data: response.data as IUserTeam
            }
        } catch (e) {
            response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if (!this.identityStore.$state.jwt) { 
                    serviceResult = {
                        status: response.status,
                        errorMsg: response.data.error ,
                    }
                } else {
                    try {
                        response = await httpCLient.get(`/UserTeam/ManageTeam?culture=${this.i18n.locale}`, {
                            headers: {
                                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                            }
                        });
                        serviceResult = {
                            status: response.status,
                            data: response.data as IUserTeam
                        }
                    } catch (e) {
                        response = (e as AxiosError).response!;
                        serviceResult = {
                            status: response.status,
                            errorMsg: response.data.error,
                        }
                    }
                    console.log(response);
                }
            }

        }
        return serviceResult;
    }
}
