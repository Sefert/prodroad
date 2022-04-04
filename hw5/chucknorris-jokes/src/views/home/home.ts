import { IHttpClient } from "aurelia";
import { AppState } from "../../state/AppState";

export class Home {

    constructor(private appState: AppState,
        @IHttpClient private http: IHttpClient){
    }
}