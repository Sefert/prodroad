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

}
