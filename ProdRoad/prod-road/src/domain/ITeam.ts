import type { IUserTeam } from './IUserTeam';

export interface ITeam {
    id?: string | null,
    AppUserId?: string | null,
    name: string | null,
    code: string | null,
    isPublic: boolean | null,
    userTeams?: IUserTeam[] | null
}