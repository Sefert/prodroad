/**
 * Keeps distinct id-s.
 */
export class ModuleId {
    static idCounter = 0;
    static freeIds = [];
     
    static getNextId() {
        if (this.freeIds.length > 0) return this.freeIds.pop();
        else return ++this.idCounter;
    }
    
    static freeId(id) {
        this.freeIds.push(id);
    }
}