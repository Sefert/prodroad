/**
 * Selector class is html similar pointers perserver.
 */
 export class Selector{  
    constructor(tag,selector){
        this.tag = tag;
        this.atribute = document.querySelectorAll(selector);
    }
}