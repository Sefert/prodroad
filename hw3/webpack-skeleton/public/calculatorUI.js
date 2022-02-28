import { Selector } from "./selector";
/**
 * Takes care of calculator user interface, which
 * relays the information from web page to calculation
 * and pack
 */
 export class CalculatorUI{ 


    constructor(calculus){
        this.calculus = calculus;
        this.selectors = [];

        this.selectors.push(new Selector('button','.btn'));
        this.selectors.push(new Selector('screen','.calculator-screen'));   

        //iterate through all monitored elements
        this.selectors.forEach(selector =>{
            if (selector.tag === 'button'){
                selector.atribute.forEach(elem =>{
                    if (elem){
                    elem.onclick = (() => {
                        this.action(elem.getAttribute('value'));
                        if (elem.getAttribute('value') === 'eqMode'){
                            elem.style.color= (elem.style.color === 'red') ? 'black' : 'red';
                        }
                    })}
                })
            }
        }); 
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