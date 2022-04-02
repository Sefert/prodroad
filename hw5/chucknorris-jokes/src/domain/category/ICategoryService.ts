import { Category } from "./Category";

export interface ICategoryService {
    getCategoriesAsync(): Promise<Category[]>
    getRandomCategoriesAsync(amountToReturn : number): Promise<Category[]>
}