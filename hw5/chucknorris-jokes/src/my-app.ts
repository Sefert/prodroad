import { AppState } from "./state/AppState";
//import { ICategory } from "./domain/ICategory";

export class MyApp {
  constructor(private appState: AppState) {
    console.log("MyApp constructor");
  }

}
