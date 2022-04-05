import { bindable, inject, observable} from "aurelia";
import { CategoryService } from "../domain/category/CategoryService";
import { ICategory } from "../domain/category/ICategory";
import { IJoke } from "../domain/joke/IJoke";

/**
 * Keeping app information
 **/
@inject(CategoryService)
export class AppState {
    
    //keep all categories
     public categories : readonly ICategory[] = [];
     //saving route id
     public category : string;
     //keeping view jokes
     public newJokes : IJoke[] = [];
     //keeping all jokes
     public seenJokes : IJoke[] = [];

    constructor(private categoryService: CategoryService) {
        this.categoryService = categoryService;
        this.categoryService.getRandomCategoriesAsync(3).then((cat) => {
            this.categories=[...cat];
            console.log(this.categories);
        });
    }
}
