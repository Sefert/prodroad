/**
 * Keeps distinct id-s.
 */
export class ModuleId {
    static idCounter : number = 0;
    static freeIds : number[]  = [];
     
    static getNextId() :number {
        if (this.freeIds.length > 0) return this.freeIds.pop()!;
        else return ++this.idCounter;
    }
    
    static freeId(id:number) {
        this.freeIds.push(id);
    }
}