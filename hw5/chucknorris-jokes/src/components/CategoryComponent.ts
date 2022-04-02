import { bindable } from "aurelia";
import { ICategory } from "../domain/category/ICategory";

export class CategoryComponent {
    @bindable
    public categories: ICategory[];
}
