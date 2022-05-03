import type { ITeam } from "@/domain/IPerson";
import httpCLient from "@/http-client";
import { BaseService } from "./BaseService";
import { teamStore } from "../stores/team";

export class TeamService extends BaseService<ITeam> {

    constructor() {
        super("teams");
    }
}
