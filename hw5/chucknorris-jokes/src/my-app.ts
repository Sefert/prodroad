import { IHttpClient } from "aurelia";
import { CategoryRepo } from "./domain/category/CategoryRepo";
//import { ICategory } from "./domain/ICategory";

export class MyApp {
  constructor(private categoryRepo: CategoryRepo) {
    console.log("MyApp constructor");
  }

}
