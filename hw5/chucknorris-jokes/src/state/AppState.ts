import { inject } from "aurelia";
import { CategoryRepo } from "../domain/category/CategoryRepo";
import { ICategory } from "../domain/category/ICategory";

@inject(CategoryRepo)
export class AppState {
    public randCategories : ICategory[] = [];

    constructor(private categoryRepo: CategoryRepo) {
        console.log(this.categoryRepo);
        this.getSelectedAmountIds(3);
    }

    private async getSelectedAmountIds(amount : number){
        console.log(this.categoryRepo.categories);
        let times : number = 0;
        while(times < amount){
            let copyLength = this.categoryRepo.categories.length-1;
            console.log(copyLength);
            if (copyLength < 2) {copyLength = 1;}

            console.log('copyLength');
            console.log(copyLength);
            console.log(this.getRandomIntInclusive(0,copyLength));
            times++;
            this.randCategories.push();
        };
        console.log(this.randCategories);
    }

    private getRandomIntInclusive(min : number, max : number) {
        min = Math.ceil(min);
        max = Math.floor(max);
        //The maximum is inclusive and the minimum is inclusive 
        return Math.floor(Math.random() * (max - min + 1) + min); 
    }
}
