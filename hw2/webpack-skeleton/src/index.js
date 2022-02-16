"use strict";
/**/import css from "./index.css";

/*https://developer.mozilla.org/en-US/docs/Web/API/Document/querySelectorAll  */
/*https://sebhastian.com/javascript-queryselectorall/ */

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


    convertEquationToFullNumbers = (operators) =>{
        let index=0;
        let number="";
        let compressedEquation=[];

        this.equation.forEach(elem =>{
            if (this.getIndex(operators,elem) > -1 && !(elem === '-' && index === 0)){
                compressedEquation.push(number);
                compressedEquation.push(elem);
                number="";
            } else {
                if (number === "") number = elem;
                else number = number.concat(this.equation[index]);
            }
            index++;
        });
        if (number !== ""){
            compressedEquation.push(number);
        }

        this.equation = compressedEquation;
        console.log(this.equation);
    }

    getNumberOfSymbolsInEquation = (operators) =>{
        let counter = 0;

        this.equation.forEach(elem => {
            if (this.getIndex(operators,elem) > -1) counter +=1; 
        });

        return counter;
    }

    getIndex(operators,sign){
        return operators.findIndex((element) => element === sign);
    }

    getConcatEquation = () =>{ 
        return this.equation.join('');
    }

    getIndexValue = (index) => {
        return this.equation[index];
    }

    getPreviousValue = (index) => {
        return this.equation[index-1];
    }

    getNextValue = (index) => {
        return this.equation[index+1];
    }

    changeEquation = (index,value) =>{
        if (index  >= 0) this.equation.splice(index - 1,3,value);
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
    operators;

    constructor(){
        this.equation = new Equation();
        this.equationState = undefined;
        this.operators = ['+','-','*','/'];
    }

    actionCaller = (action) =>{
        console.log(action);
        console.log(JSON.stringify(this.equation));

        if (!(this.findErrorAction(action))){
            switch (action) {
                case 'all-clear':
                    this.equation.clear(); 
                    break;
                case '=':
                    this.equation.convertEquationToFullNumbers(this.operators);
                    this.calculate();
                    break;
                default:
                    this.equation.addElement(action);
            }
        }
        this.equationState = this.equation.getConcatEquation();
    }

    getEquationState = () =>{
        return this.equationState;
    }

    findErrorAction = (action) =>{
        let error = 0;
        if (this.equation.getConcatEquation().length === 0 && 
            this.equation.getIndex(this.operators,action) > -1 &&
            action !== '-') error = 1;
        else if (this.equation.getIndex(this.operators,this.equation.equation[this.equation.equation.length-1]) > -1 &&
            action === '=') error = 1;
        else if (this.equation.getIndex(this.operators,this.equation.equation[this.equation.equation.length-1]) > -1 &&
            this.equation.getIndex(this.operators,action) > -1) error = 1;
        return error;
    }

    calculate = () =>{
        while(this.equation.getNumberOfSymbolsInEquation(this.operators)){
            this.calculateIndexValue(this.getNextCalculationIndex());
        }
    }

    calculateIndexValue = (startIndex) =>{ 
        let value = NaN;

        switch(this.equation.getIndexValue(startIndex)) {
            case '*':
                value = parseFloat(this.equation.getPreviousValue(startIndex)) * 
                        parseFloat(this.equation.getNextValue(startIndex));
                break;
            case '/':
                value = parseFloat(this.equation.getPreviousValue(startIndex)) / 
                        parseFloat(this.equation.getNextValue(startIndex));
                break;
            case '+':
                value = parseFloat(this.equation.getPreviousValue(startIndex)) + 
                        parseFloat(this.equation.getNextValue(startIndex));
                break;
            case '-':
                value = parseFloat(this.equation.getPreviousValue(startIndex)) - 
                        parseFloat(this.equation.getNextValue(startIndex));
                break;
            default:
                value;
          }  
          console.log('Index calculation value: ' + value);
          this.equation.changeEquation(startIndex,value.toString());
          console.log('Equation: ' + JSON.stringify(this.equation.equation));
    }


    getNextCalculationIndex = () =>{
        let startIndex = NaN;
    
        if (((this.getEquationIndex('*') !== -1) && (this.getEquationIndex('*') < this.getEquationIndex('/'))) || 
            ((this.getEquationIndex('/') === -1) && (this.getEquationIndex('*') > this.getEquationIndex('/')))) 
                startIndex = this.getEquationIndex('*');
        else if (this.getEquationIndex('/') >= 1) startIndex = this.getEquationIndex('/');
        else if (this.getEquationIndex('+') >= 1) startIndex = this.getEquationIndex('+');
        else if (this.getEquationIndex('-') >= 1) startIndex = this.getEquationIndex('-');
    
        console.log('Starting with index: ' + startIndex);
        return startIndex;
    }

    getEquationIndex = (value) =>{
        return this.equation.getIndex(this.equation.equation,value);
    }
}


new CalculatorUI(new Calculus());

