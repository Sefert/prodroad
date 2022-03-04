"use strict";
import css from "./index.css"
import { CalculatorModule } from "../public/calculatorModule";
import { CalculatorUI } from "../public/calculatorUI";
import { Calculus } from "../public/calculus";

/**
 * Calling calculation program
 */
let calculators = [];
let calculatorCounter = 0;
let navBar = document.createElement('div');
let navBarButton = document.createElement('button');
navBarButton.textContent = 'ADD CALCULATOR';

navBar.appendChild(navBarButton);
document.body.appendChild(navBar);

let centerArea = document.createElement('div');
//centerArea.style.display = 'inline-block';

document.body.appendChild(centerArea);


function addCalculator(){
    navBarButton.onclick = function() {
        calculatorCounter++;
        let calcMod = new CalculatorModule(calculatorCounter);
        centerArea.appendChild(calcMod.getCalculatorModule()); 
        new CalculatorUI(new Calculus(),calculatorCounter)
        //calculators.push();
    }
    
    //let buttonClose = document.querySelector('.button-close');

    /*centerArea.childNodes.forEach(childnode => {
        childnode.onclick
    })*/

}

addCalculator();