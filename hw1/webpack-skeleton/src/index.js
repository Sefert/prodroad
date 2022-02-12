"use strict";
/**/import css from "./index.css";

/*https://developer.mozilla.org/en-US/docs/Web/API/Document/querySelectorAll  */
/*https://sebhastian.com/javascript-queryselectorall/ */
const buttons = document.querySelectorAll('.btn');
const screen = document.querySelector('.calculator-screen');
const operators = ["+","-","/","*"];

let equation = [];


buttons.forEach(button =>{
    button.onclick = function(event){
        let btnValue = button.getAttribute('value');
        console.log(btnValue);

        switch(btnValue) {
            case 'all-clear':
                clearEquationElement(); 
                break;
            case '=':
                convertFirstMinusToFullNegative();
                calculateValue(equation);
                break;
            default:
                addEquationElement(btnValue);
          }

        displayOnScreen(concatParams(equation));
    }
})

function calculateValue(equation){
    while(countSymbolsInEquation(operators)>0){
        calculateIndexValue(getNextCalculationIndex());
    }
    /*for testing */
    /*for (let sign = 0; sign < countSymbolsInEquation(operators); sign++) {
        calculateIndexValue(getNextCalculationIndex());
    }*/
}

function convertFirstMinusToFullNegative(){
    if ((equation[0] === '-') && (Number.isInteger(parseInt(equation[1])))){
        equation.splice(0,2,equation[0].concat(equation[1]));}
    else {
        clearEquationElement();
        displayOnScreen(equation);
    }
    console.log('Equation: ' + JSON.stringify(equation));
}

function countSymbolsInEquation(operators){
    let symbolCount = 0;
    let counter = 0;
    let symbolError = 0;
    equation.forEach(elem => {
        counter +=1; 
        operators.forEach(operator =>{
            if (elem === operator) symbolCount+=1;
            if (((elem === operator) && (counter === 1)) ||
                ((elem === operator) && (counter === equation.length))) {symbolError = 1};
        }) 
    })
    console.log('Symbol error: ' + symbolError); 
    console.log('Symbols present: ' + symbolCount);
    return (symbolError ? 0 : symbolCount);
}

function getNextCalculationIndex(){
    let startIndex = NaN;

    if (((getIndex('*') !== -1) && (getIndex('*') < getIndex('/'))) || 
        ((getIndex('/') === -1) && (getIndex('*') > getIndex('/')))) startIndex = getIndex('*');
    else if (getIndex('/') >= 1) startIndex = getIndex('/');
    else if (getIndex('+') >= 1) startIndex = getIndex('+');
    else if (getIndex('-') >= 1) startIndex = getIndex('-');

    console.log('Starting with index: ' + startIndex);
    return startIndex;
}

function calculateIndexValue(startIndex){ 
    let value = NaN;

    switch(equation[startIndex]) {
        case '*':
            value = parseFloat(getPreviousNumber(startIndex, operators)) * parseFloat(getNextNumber(startIndex, operators));
            break;
        case '/':
            value = parseFloat(getPreviousNumber(startIndex, operators)) / parseFloat(getNextNumber(startIndex, operators));
            break;
        case '+':
            value = parseFloat(getPreviousNumber(startIndex, operators)) + parseFloat(getNextNumber(startIndex, operators));
            break;
        case '-':
            value = parseFloat(getPreviousNumber(startIndex, operators)) - parseFloat(getNextNumber(startIndex, operators));
            break;
        default:
            value;
      }  
      console.log('Index calculation value: ' + value);
      insertCalculationToEquation(startIndex,operators,value.toString());
      console.log('Equation: ' + JSON.stringify(equation));
}

function insertCalculationToEquation(startIndex, operators, value){
    let previousIndex = getPreviousSymbolIndex(startIndex, operators);
    let nextIndex = getNextSymbolIndex(startIndex, operators);

    console.log('previousIndex: ' + previousIndex);
    console.log('nextIndex: ' + nextIndex);

    if (startIndex  >= 0) equation.splice(previousIndex + 1,nextIndex-previousIndex-1,value);
}

function getPreviousSymbolIndex(startIndex, findOperators){
    let previousIndex = startIndex-1;
    while(!(findOperators.includes(equation[previousIndex])) && previousIndex >= 0){
        previousIndex--;
    }
    return previousIndex;
}

function getNextSymbolIndex(startIndex, findOperators){
    let nextIndex = startIndex+1;
    while(!(findOperators.includes(equation[nextIndex])) && nextIndex < equation.length){
        nextIndex++;
    }
    return nextIndex;
}

function getPreviousNumber(startIndex, findOperators){
    let previousIndex = startIndex-1;
    let previousNumber = NaN;
    while(!(findOperators.includes(equation[previousIndex])) && previousIndex >= 0){

        if (!isNaN(previousNumber)) {previousNumber = equation[previousIndex].concat(previousNumber);}
        else {previousNumber = equation[previousIndex];}
        previousIndex--;
    }
    return previousNumber;
}

function getNextNumber(startIndex, findOperators){
    let nextIndex = startIndex+1;
    let nextNumber = NaN;

    while(!(findOperators.includes(equation[nextIndex])) && nextIndex < equation.length){

        if (!isNaN(nextNumber)) {nextNumber = nextNumber.concat(equation[nextIndex]);}
        else {nextNumber= equation[nextIndex];}  
        nextIndex++;
    }
    return nextNumber;
}

function getIndex(sign){
    return equation.findIndex((element) => element === sign);
}

function addEquationElement(elem){
    equation.push(elem);
    console.log(JSON.stringify(equation));
}

function clearEquationElement(){
    equation = [];
    console.log(JSON.stringify(equation));
}

function displayOnScreen(value){
    screen.value = value;
}

function concatParams(params){ 
    return params.join('');
}