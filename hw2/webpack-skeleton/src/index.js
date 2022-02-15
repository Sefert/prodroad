"use strict";
/**/import css from "./index.css";

/*https://developer.mozilla.org/en-US/docs/Web/API/Document/querySelectorAll  */
/*https://sebhastian.com/javascript-queryselectorall/ */


const operators = ["+","-","/","*"];

class Equation {

    equation = [];

    constructor(){
        this.equation = [];
    }

    clear(){
        this.equation = [];
        this.number= [];
        console.log(JSON.stringify(this.equation));
    }

    addElement = (elem) => {
        this.equation.push(elem);
        console.log(JSON.stringify(this.equation));
    }

    getCalculation(){

    }

    getConcatEquation = () =>{ 
        return this.equation.join('');
    }
}

class Selector{  
    constructor(tag,selector){
        this.tag = tag;
        this.atribute = document.querySelectorAll(selector);
    }
}

class CalculatorUI{ 

    /*Arrow functions : https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Operators/this */
    constructor(calculus){
        this.calculus = calculus;
        this.selectors = [];

        this.selectors.push(new Selector('button','.btn'));
        this.selectors.push(new Selector('screen','.calculator-screen'));   

        this.selectors.forEach(selector =>{
            if (selector.tag === 'button'){
                selector.atribute.forEach(elem =>{
                    if (elem){
                    elem.onclick = (() => this.action(elem.getAttribute('value')));
                }})
            }
        }); 
    } 



    action = (action) => {
        //console.log(action);
        this.calculus.actionCaller(action);
        this.refresh();
    }

    refresh = () => {
        this.selectors.forEach(selector =>{
            if (selector.tag === 'screen'){
                selector.atribute.forEach(elem =>{
                    if (elem){
                    elem.value = this.calculus.getEquationState();
                }})
            }
        });
    }
}

class Calculus {

    equation;
    equationState;

    constructor(){
        this.equation = new Equation();
        this.equationState = undefined;
    }

    actionCaller = (action) =>{
        console.log(action);
        console.log(JSON.stringify(this.equation));

        switch (action) {
            case 'all-clear':
                this.equation.clear(); 
                break;
            case '=':
                this.equation.getCalculation();
                break;
            default:
                this.equation.addElement(action);
                this.equationState = this.equation.getConcatEquation();
          }
    }

    getEquationState = () =>{
        return this.equationState;
    }

    getIndex(operators, sign){
        return operators.findIndex((element) => element === sign);
    }

}


new CalculatorUI(new Calculus());

