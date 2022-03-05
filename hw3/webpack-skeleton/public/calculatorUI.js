import { CalculatorModule } from "./calculatorModule";
/**
 * Takes care of calculator user interface, which
 * relays the information from web page to calculation
 * and pack
 */
 export class CalculatorUI{ 

    constructor(calculus,module){
        this.calculus = calculus;
        this.module = module;
     
        module.closeKey.onclick = (() => {
            this.module.freeModuleId();
            this.module.calculatorContent.remove();
        });

        module.calculatorKeyModule.childNodes.forEach(child =>{
            if (child){
                child.onclick = (() => {
                    this.action(child.getAttribute('value'));
                    if (child.getAttribute('value') === 'EQ MODE'){
                        child.style.color= (child.style.color === 'red') ? 'black' : 'red';
                    }
                });
            }
        });
    } 

    action = (action) => {
        this.calculus.actionCaller(action);
        this.refresh();
    }

    //refershes display
    refresh = () => {
        this.module.calculatorScreen.value = this.calculus.getEquationState();
    }
}
