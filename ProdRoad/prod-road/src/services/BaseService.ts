import { useI18n } from 'vue-i18n';
import httpCLient from "@/http-client";
import { userStore } from "@/stores/identity";
import type { AxiosError, AxiosResponse } from "axios";
import { IdentityService } from "./identity/IdentityService";
import type { IServiceResult } from "./contracts/IServiceResult";

export class BaseService<TEntity> {
    identityStore = userStore();
    i18n= useI18n();

    constructor(private path: string) {
    }

    async getAll(): Promise<IServiceResult<TEntity[]>> {

        var i18n= useI18n();
        console.log("getAll");

        console.log(`${this.path}?culture=${this.i18n.locale}`);
        var response : AxiosResponse;
        //var respData : TEntity[] ;
        var serviceResult : IServiceResult<TEntity[]> = {};
        

        try {
            response = await httpCLient.get(`/${this.path}?culture=${this.i18n.locale}`, {
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
                        response = await httpCLient.get(`/${this.path}?culture=${this.i18n.locale}`, {
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
        
        console.log(`/${this.path}?culture=${this.i18n.locale}`);
        var serviceResult : IServiceResult<void> = {};
        try {
            response = await httpCLient.post(`/${this.path}?culture=${this.i18n.locale}`, entity,
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                }
            );
            
            console.log(response.status);

            serviceResult = {
                status: response.status,
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
                        response = await httpCLient.post(`/${this.path}?culture=${this.i18n.locale}`, entity,
                            {
                                headers: {
                                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                                }
                            }
                        );
                        serviceResult = {
                            status: response.status,
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
            } else {
                serviceResult = {
                    status: response.status,
                    errorMsg: response.data.error ,
                }
            }

        }
        return serviceResult;
    }

    async edit(id: string, entity: TEntity): Promise<IServiceResult<void>> {          
        console.log("add");

        var response : AxiosResponse;

        var serviceResult : IServiceResult<void> = {};
        
        try {
            response = await httpCLient.put(`/${this.path}/${id}?culture=${this.i18n.locale}`, entity,
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                }
            );
            
            console.log(response.status);

            serviceResult = {
                status: response.status,
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
                        response = await httpCLient.put(`/${this.path}/${id}?culture=${this.i18n.locale}`, entity,
                            {
                                headers: {
                                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                                }
                            }
                        );
                        serviceResult = {
                            status: response.status,
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

    async delete(id: string): Promise<IServiceResult<void>> {
        console.log("delete");

        var response : AxiosResponse;

        var serviceResult : IServiceResult<void> = {};
        
        try {
            response = await httpCLient.delete(`/${this.path}/${id}?culture=${this.i18n.locale}`, 
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                }
            );

            console.log(response.status);

            serviceResult = {
                status: response.status,
            }
        } catch (e) {
            console.log('Here2');
            response = (e as AxiosError).response!;
            console.log(response);
            console.log(response.status);
            console.log((e as AxiosError).response!);
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if (!this.identityStore.$state.jwt) { 
                    serviceResult = {
                        status: response.status,
                        errorMsg: response.data.error,
                    }
                } else {
                    try {
                        console.log('Here3');
                        response = await httpCLient.put(`/${this.path}/${id}?culture=${this.i18n.locale}`,
                            {
                                headers: {
                                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                                }
                            }
                        );
                        serviceResult = {
                            status: response.status,
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
            } else {
                serviceResult = {
                    status: response.status,
                    errorMsg: response.data.error,
                }
            }

        }
        return serviceResult;
    }
}
