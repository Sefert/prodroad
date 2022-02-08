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
                calculateValue(equation);
                break;
            default:
                addEquationElement(btnValue);
          }

        displayOnScreen(concatParams(equation));
    }
})

function calculateValue(equation){


    /*https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/find*/
    while(getIndex('*') >= 0 || getIndex('/') >= 0){
        multiplyDivide();
        console.log(equation);
    } 
    /*while(getIndex('+') >= 0 || getIndex('-') >= 0){
        addSubstract();
        console.log(equation);
    } */

}

function addSubstract(){
    equation.forEach(elem => {
        operators.includes(elem)
    })
}

function multiplyDivide(){
    let multiplyIndex = NaN;
    let previousNumber = NaN;
    let nextNumber = NaN;
    let value = NaN;

    if (getIndex('*') >= getIndex('/')) multiplyIndex = getIndex('*');
    else multiplyIndex = getIndex('/');
  
    let i = multiplyIndex-1;

    while(!(operators.includes(equation[i])) && i >= 0){

        if (!isNaN(previousNumber)) {previousNumber = equation[i].concat(previousNumber);}
        else {previousNumber = equation[i];}
        i--;
    }

    let j = multiplyIndex+1;

    while(!(operators.includes(equation[j])) && j < equation.length){

        if (!isNaN(nextNumber)) {nextNumber = nextNumber.concat(equation[j]);}
        else {nextNumber= equation[j];}  
        j++;
    }

    if (equation[multiplyIndex] === '*') {value = previousNumber*nextNumber;}
    else if (equation[multiplyIndex] === '/') {value = previousNumber/nextNumber;}

    if (multiplyIndex >= 0) equation.splice(i+1,j-multiplyIndex+1,value);
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