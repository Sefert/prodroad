import { inject, route } from "aurelia";
import { AppState } from "./state/AppState";
//import { ICategory } from "./domain/ICategory";


/*@route({
  routes: [
    {
      id: 'persons',
      path: ['', '/persons'],
      component: import('./views/persons/persons'),
      title: 'Persons',
    },
    {
      id: 'persons-edit',
      path: '/persons/edit/:id',
      component: import('./views/persons/person-edit'),
      title: 'Person Edit',
    },
    {
      id: 'contacts',
      path: '/contacts',
      component: import('./views/contacts/contacts'),
      title: 'Contacts',
    },
    {
      id: 'contactTypes',
      path: '/contacttypes',
      component: import('./views/contactTypes/contactTypes'),
      title: 'ContactTypes',
    },
  ]
})*/

export class MyApp {
  constructor(private appState: AppState) {
    console.log("MyApp constructor");
    console.log(appState.name);
    console.log(appState.categories);
  }
}
