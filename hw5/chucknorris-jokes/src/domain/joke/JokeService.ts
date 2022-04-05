import { IHttpClient } from "aurelia";
import { IJoke } from "./IJoke";

export class JokeService {

    public jokes : IJoke[] = []

    constructor(@IHttpClient private http: IHttpClient){

    }

    async getCategoryJokesAsync(name: string, amount: number): Promise<IJoke[]> {
        try {
            this.jokes = [];
            for (let index = 0; index < amount; index++) {           
                let result = await this.http.get(`https://api.chucknorris.io/jokes/random?category=${name}`);
                let json = await result.json();
                this.jokes.push(json);
            }
            console.log(this.jokes); 
            return this.jokes;
            //console.log(this.appState.seenJokes);
        }catch (error) {
            console.log(error);
            }
    }
}