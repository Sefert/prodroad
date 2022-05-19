import type { IAppUser } from './IAppUser';

export interface IUserTeam {
    id?: string | null,
    AppUserId?: string | null,
    TeamId?: string | null,
    accepted: boolean | null,
    appUser?: IAppUser | null,
}