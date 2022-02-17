"use strict";
import css from "./index.css";

/**
 * Equation is class to preserve user clicked
 * information and perform basic operations with
 * array equation
 */
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


    // all individual numeric elements between symbols are converted to full numbers
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

    removeLast = () =>{
        this.equation.pop();
    }
}

/**
 * Selector class is html similar pointers perserver.
 */
class Selector{  
    constructor(tag,selector){
        this.tag = tag;
        this.atribute = document.querySelectorAll(selector);
    }
}

/**
 * Takes care of calculator user interface, which
 * relays the information from web page to calculation
 * and pack
 */
class CalculatorUI{ 


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

/**
 * Businesslogic class to perform calculations and find errors
 * Todo: move errors to separate class
 */
class Calculus {

    equation;
    equationState;
    operators;
    eqMode;

    constructor(){
        this.equation = new Equation();
        this.equationState = undefined;
        this.operators = ['+','-','*','/'];
        this.eqMode = 0;
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
                case 'undo':
                    this.equation.removeLast();
                    break; 
                case 'eqMode':
                    this.eqMode = this.setEqMode();
                    console.log("Eqmode :" + JSON.stringify(this.eqMode));
                    break;   
                default:
                    this.equation.addElement(action);
            }
        }
        this.equationState = this.equation.getConcatEquation();
    }

    //switches system between mathematical standard and linear calculation modes
    setEqMode = () =>{
        return (this.eqMode === 1) ? 0 : 1;
    }

    //returns equation to screen
    getEquationState = () =>{
        return this.equationState;
    }

    //find user enetered problems
    findErrorAction = (action) =>{
        let error = 0;
        if (this.equation.getConcatEquation().length === 0 && this.equation.getIndex(this.operators,action) > -1 &&
            action !== '-') error = 1;
        else if ((this.isPreviousEntryAnOperator(this.operators) || this.isPreviousEntryAnOperator(['.'])) &&
                action === '=') error = 1;
        else if ((this.isPreviousEntryAnOperator(this.operators) || this.isPreviousEntryAnOperator(['.'])) &&
                (this.equation.getIndex(this.operators,action) > -1 || action === '.')) error = 1;
        return error;
    }

    isPreviousEntryAnOperator = (operators) =>{
        return (this.equation.getIndex(operators,
            this.equation.equation[this.equation.equation.length-1]) > -1) ? 1 : 0;
    }

    calculate = () =>{
        while(this.equation.getNumberOfSymbolsInEquation(this.operators)){
            if (this.eqMode === 1) this.calculateIndexValue(this.getNextCalculationIndex());
            else this.calculateIndexValue(1);
        }
    }

    //calculate equation value on index operator with before and after numbers
    //and inserts subcalculation back to equation
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

    //based on mathemathical logic get first calcualtion operator 
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

/**
 * Calling calculation program
 */
new CalculatorUI(new Calculus());

