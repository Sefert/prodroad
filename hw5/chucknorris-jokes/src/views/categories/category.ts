import { observable,  Params} from "aurelia";
import { IJoke } from "../../domain/joke/IJoke";
import { JokeService } from "../../domain/joke/JokeService";
import { AppState } from "../../state/AppState";

/**
 * class for category view and state change handlig
 */

export class Category {

    @observable viewNr : number = 0;

    constructor(private appState: AppState,
        private jokeService : JokeService){
            this.appState = appState;
            this.jokeService = jokeService;
            console.log("Category constructor");
    }

    async load(params: Params) {

        this.appState.category = params.name;

        let catNr = 0;
        let counter = 0;

        this.appState.categories.forEach(cat => {
            counter++;
            if (cat.name == params.name){
                catNr = counter;
                console.log(catNr);
            }
        });

        this.viewNr = catNr;

        this.appState.newJokes = await this.jokeService.getCategoryJokesAsync(params.name,5);
        this.saveSeenJokes(this.appState.newJokes);

    }

    public saveSeenJokes(jokes : IJoke[]){
       jokes.forEach(joke => {
            if (!this.existJoke(joke)){
                this.appState.seenJokes.push(joke);
            } 
        });             
    }

    public existJoke(joke : IJoke) : boolean{
        let exist = false;
        this.appState.seenJokes.forEach(j => {
            if (j.id == joke.id){
                exist = true;
            }
        });
        return exist;
    }
}
