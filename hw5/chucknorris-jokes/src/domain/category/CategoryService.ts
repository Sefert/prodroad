import { IHttpClient } from "aurelia";
import { Category } from "./Category";
import { ICategory } from "./ICategory";

export class CategoryService {
    //public categories : ICategory[] = [];

    constructor(@IHttpClient private http: IHttpClient){
    }
  
    async getCategoriesAsync(): Promise<Category[]> {
      try {
        let categories : ICategory[] = [];
        let index : number = 0;
        let result = await this.http.get('https://api.chucknorris.io/jokes/categories');
        let json = await result.json();
        json.forEach(elem => {
          //console.log(elem);
            categories.push(new Category(++index, elem));//new Category(++index, elem)
          });
          return categories;
      } catch (error) {
        console.log(error);
      }
    }

    async getRandomCategoriesAsync(amountToReturn : number): Promise<Category[]> {
      try {
        let categories : ICategory[] = await this.getCategoriesAsync();
        let askedCategories : ICategory[] = [];
        let times : number = 0;
        while(times < amountToReturn){
          let index = this.getRandomIntInclusive(0,categories.length-1);
          askedCategories.push(categories.splice(index,1)[0]);
          times++;
        };
          return askedCategories;
      } catch (error) {
        console.log(error);
      }
    }

    private getRandomIntInclusive(min : number, max : number) {
        min = Math.ceil(min);
        max = Math.floor(max);
        //The maximum is inclusive and the minimum is inclusive 
        return Math.floor(Math.random() * (max - min + 1) + min); 
    }
}


