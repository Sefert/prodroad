import { bindable, inject} from "aurelia";
import { CategoryService } from "../domain/category/CategoryService";
import { ICategory } from "../domain/category/ICategory";


@inject(CategoryService)
export class AppState {
    @bindable
    public categories : ICategory[] = [];

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
