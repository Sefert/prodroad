import { inject, IRouter, route } from "aurelia";
import { AppState } from "./state/AppState";
//import { ICategory } from "./domain/ICategory";


@route({
  routes: [
    {
      id: 'category',
      path: ['','/category/:name'],
      component: import('./views/categories/category'),
      title: 'category',
    }
  ]
})


export class MyApp {
  constructor(private appState: AppState) {
  }
}

