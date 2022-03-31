import { bindable } from "aurelia";
import { ICategory } from "../domain/ICategory";

export class Category {
    @bindable
    public name: ICategory;

}
