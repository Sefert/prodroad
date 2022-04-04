import { bindable, inject} from "aurelia";
import { CategoryService } from "../domain/category/CategoryService";
import { ICategory } from "../domain/category/ICategory";
import { IJoke } from "../domain/joke/IJoke";


@inject(CategoryService)
export class AppState {
    @bindable
    public categories : ICategory[] = [];
    public category : string;
    public newJokes : IJoke[] = [];
    public seenJokes : IJoke[] = [];

    constructor(private categoryService: CategoryService) {
        this.categoryService = categoryService;
        this.getCategoriesAsync(3).then(() => {
            this.categories.concat(this.categories);
            console.log(this.categories);
        });
    }

    async getCategoriesAsync(numofCategories : number) : Promise<void> {
        this.categories = await this.categoryService.getRandomCategoriesAsync(numofCategories);
        console.log(this.categories);
    }


}
