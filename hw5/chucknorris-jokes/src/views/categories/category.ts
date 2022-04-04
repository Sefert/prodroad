import { bindable,  IHttpClient,  Params} from "aurelia";
import { IJoke } from "../../domain/joke/IJoke";
import { AppState } from "../../state/AppState";

export class Category {

    //static parameters = ['bar'];
    public jokes : IJoke[] = [];

    @bindable category: string;

    constructor(private appState: AppState,
        @IHttpClient private http: IHttpClient){
            console.log("Category constructor");  
    }

    load(params: Params) {
        console.log(params.name);
        this.getCategoryJokesAsync(params.name,5);
    }

    async getCategoryJokesAsync(name: string, amount: number): Promise<void> {
        try {
          for (let index = 0; index < amount; index++) {           
            let result = await this.http.get(`https://api.chucknorris.io/jokes/random?category=${name}`);
            let json = await result.json();
            console.log(json);
            } 
        }catch (error) {
            console.log(error);
            }
    }
}
