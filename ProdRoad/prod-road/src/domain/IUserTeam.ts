import type { IAppUser } from "./IAppUser";
import type { ITeam } from "./ITeam";

export interface IUserTeam {
  id?: string | null;
  AppUserId?: string | null;
  TeamId?: string | null;
  accepted: boolean | null;
  appUser?: IAppUser | null;
  team: ITeam | null;
}
