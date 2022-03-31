import { IHttpClient } from "aurelia";
//import { ICategory } from "./domain/ICategory";

export class MyApp {
  public message : string[] = [];

  constructor(@IHttpClient private http: IHttpClient){
    this.getJokeCategories().then();
  }

  async getJokeCategories(): Promise<void> {
    try {
      let result = await this.http.get('https://api.chucknorris.io/jokes/categories');
      (await result.json()).forEach(element => {
         this.message.push(element)});
    } catch (error) {
      console.log(error);
    }
  }
}
