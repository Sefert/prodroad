import { bindable,  Params} from "aurelia";
import { AppState } from "../../state/AppState";

export class Category {

    static parameters = ['bar'];

    @bindable category: string;

    constructor(private appState: AppState){
            console.log("Category constructor");  
    }

    load(params: Params) {
        console.log(params);
    }

}
