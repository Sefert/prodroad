import { Category } from "./Category";

export interface ICategoryRepo {
    categories : Category[];
    getJokeCategoriesAsync(): Promise<void>;
}