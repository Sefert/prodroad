import { Selector } from "./selector";
/**
 * Takes care of calculator user interface, which
 * relays the information from web page to calculation
 * and pack
 */
 export class CalculatorUI{ 

    constructor(calculus,moduleId){
        this.calculus = calculus;
        this.selectors = [];

        this.selectors.push(new Selector('button','.card'+moduleId+' .btn'));
        this.selectors.push(new Selector('screen','.card'+moduleId+' .calculator-screen'));   
    
        //iterate through all monitored elements
        this.selectors.forEach(selector =>{
            if (selector.tag === 'button'){
                selector.atribute.forEach(elem =>{
                    if (elem){
                    elem.onclick = (() => {
                        this.action(elem.getAttribute('value'));
                        if (elem.getAttribute('value') === 'EQ MODE'){
                            elem.style.color= (elem.style.color === 'red') ? 'black' : 'red';
                        }
                    })}
                })
            }
        });
        console.log(this.calcCounter);
    } 

    action = (action) => {
        this.calculus.actionCaller(action);
        this.refresh();
    }

    //refershes display
    refresh = () => {
        this.selectors.forEach(selector =>{
            if (selector.tag === 'screen'){
                selector.atribute.forEach(elem =>{
                    if (elem){
                        elem.value = this.calculus.getEquationState();
                    }
                })
            }
        });
    }
}
