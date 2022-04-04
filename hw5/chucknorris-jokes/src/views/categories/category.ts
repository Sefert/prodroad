import { bindable,  IHttpClient,  Params} from "aurelia";
import { IJoke } from "../../domain/joke/IJoke";
import { AppState } from "../../state/AppState";

export class Category {

    constructor(private appState: AppState,
        @IHttpClient private http: IHttpClient){
            console.log("Category constructor");  
    }

    load(params: Params) {
        //console.log(params.name);
        this.appState.category = params.name;
        console.log(this.appState.category);
        this.getCategoryJokesAsync(params.name,5);
    }

    async getCategoryJokesAsync(name: string, amount: number): Promise<void> {
        try {
            this.appState.newJokes = [];
            for (let index = 0; index < amount; index++) {           
                let result = await this.http.get(`https://api.chucknorris.io/jokes/random?category=${name}`);
                let json = await result.json();
                this.appState.newJokes.push(json);
                this.saveSeenJokes(json);
            }
            //console.log(this.appState.newJokes); 
            //console.log(this.appState.seenJokes);
        }catch (error) {
            console.log(error);
            }
    }

    public saveSeenJokes(joke : IJoke){
        if (!this.existJoke(joke)){
            this.appState.seenJokes.push(joke);
        }           
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
