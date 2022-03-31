import { IHttpClient } from "aurelia";
import { Category } from "./Category";
import { ICategory } from "./ICategory";

export class CategoryRepo {
    public categories : ICategory[] = [];

    constructor(@IHttpClient private http: IHttpClient){
      this.getJokeCategories();
    }
  
    async getJokeCategories(): Promise<void> {
      try {
        let index : number = 0;
        let result = await this.http.get('https://api.chucknorris.io/jokes/categories');
        let json = await result.json();
        json.forEach(elem => {
            this.categories.push(new Category(++index, elem));
            console.log(this.categories);
          });
      } catch (error) {
        console.log(error);
      }
    }
}


