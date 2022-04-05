import { IJoke } from "./IJoke";

export interface IJokeService {
    getCategoryJokesAsync(name: string, amount: number): Promise<IJoke[]>;
}