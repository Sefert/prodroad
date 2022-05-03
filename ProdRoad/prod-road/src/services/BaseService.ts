import httpCLient from "@/http-client";
import { userStore } from "@/stores/identity";
import type { AxiosError, AxiosResponse } from "axios";
import { IdentityService } from "./identity/IdentityService";
import type { IServiceResult } from "./contracts/IServiceResult";

export class BaseService<TEntity> {
    identityStore = userStore();

    constructor(private path: string) {
    }

    async getAll(): Promise<IServiceResult<TEntity[]>> {

        console.log("getAll");
        var response : AxiosResponse;
        //var respData : TEntity[] ;
        var serviceResult : IServiceResult<TEntity[]> = {};
        
        try {
            response = await httpCLient.get(`/${this.path}`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }
            });
            
            console.log(response);

            serviceResult = {
                status: response.status,
                data: response.data as TEntity[]
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
                        response = await httpCLient.get(`/${this.path}`, {
                            headers: {
                                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                            }
                        });
                        serviceResult = {
                            status: response.status,
                            data: response.data as TEntity[]
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

    /*async get(id: string): Promise<TEntity> {
        console.log("get");
        let response = await httpCLient.get(`/${this.path}/${id}`);
        console.log(response);
        let res = response.data as TEntity;
        return res;
    }*/

    async add(entity: TEntity): Promise<IServiceResult<void>> {
        console.log("add");

        var response : AxiosResponse;
        //var respData : TEntity[] ;
        var serviceResult : IServiceResult<void> = {};
        
        try {
            response = await httpCLient.post(`/${this.path}`, entity,
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                }
            );
            
            console.log(response);

            serviceResult = {
                status: response.status,
                //data: response.data as TEntity[]
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
                        response = await httpCLient.post(`/${this.path}`, entity,
                            {
                                headers: {
                                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                                }
                            }
                        );
                        serviceResult = {
                            status: response.status,
                            //data: response.data as TEntity[]
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

    /*async edit(id: string, entity: TEntity): Promise<IServiceResult<void>> {
        console.log("put");
        let response;
        try {
            response = await httpCLient.put(`/${this.path}/${id}`, entity,
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                }
            );
        } catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                //if (!this.identityStore.$state.jwt) return [];
                
                response = await httpCLient.put(`/${this.path}/${id}`, entity,
                    {
                        headers: {
                            "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                        }
                    }
                );
                console.log(response);

            }
        }
        return { status: response.status };
    }*/
}
