import { CalculatorModule } from "./calculatorModule";
import { Calculus } from "./calculus";
/**
 * Takes care of calculator user interface, which
 * relays the information from web page to calculation
 * and pack
 */
 export class CalculatorUI{ 

     calculus: Calculus;
     module: CalculatorModule;

    constructor(calculus:Calculus,module:CalculatorModule){
        this.calculus = calculus;
        this.module = module;
     
        module.closeKey.onclick = (() => {
            this.module.freeModuleId();
            this.module.calculatorContent.remove();
        });

        Array.from(module.calculatorKeyModule.children).forEach(child =>{
            if (child instanceof HTMLButtonElement){
                child.onclick = (() => {
                    this.action(child.getAttribute('value')!);
                    if (child.getAttribute('value') === 'EQ MODE'){
                        child.style.color= (child.style.color === 'red') ? 'black' : 'red';
                    }
                });
            }
        });
    } 

    action = (action:string) => {
        this.calculus.actionCaller(action);
        this.refresh();
    }

    //refershes display
    refresh = () => {      
        this.module.calculatorScreen.value = (typeof this.calculus.getEquationState() === undefined) ? 
            'Invalid value' : this.calculus.getEquationState()!;
    }


}
