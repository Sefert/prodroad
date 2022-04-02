import { inject } from "aurelia";
import { Category } from "../components/CategoryComponent";
import { CategoryService } from "../domain/category/CategoryService";
import { ICategory } from "../domain/category/ICategory";


@inject(CategoryService)
export class AppState {
    public categories : ICategory[] = [];

    constructor(private categoryService: CategoryService) {
        this.categoryService = categoryService;
        this.getCategoriesAsync(3);
    }

    async getCategoriesAsync(numofCategories : number) : Promise<void> {
        this.categories = await this.categoryService.getRandomCategoriesAsync(numofCategories);
        console.log(this.categories[1].name);
    }
}
