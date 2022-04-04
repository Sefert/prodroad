import { AppState } from "../../../../state/AppState";

export class CategoryVisualOne {

    constructor(private appState: AppState) {
        this.appState = appState;
        console.log("CategoryVisualOne constructor"); 
    }
}