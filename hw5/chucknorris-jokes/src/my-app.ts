import { route } from "aurelia";
import { AppState } from "./state/AppState";


@route({
  routes: [
    {
      id: 'home',
      path: ['','home'],
      component: import('./views/home/home'),
      title: 'home',
    },
    {
      id: 'category',
      path: ['/category/:name'],
      component: import('./views/categories/category'),
      title: 'category',
    }
  ]
})


export class MyApp {
  constructor(private appState: AppState) {
    
  }
}

