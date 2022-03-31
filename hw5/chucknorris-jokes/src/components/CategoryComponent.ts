import { bindable } from "aurelia";
import { ICategory } from "../domain/category/ICategory";

export class Category {
    @bindable
    public name: ICategory;
}
