"use strict";
import './index.css';
import { CalculatorModule } from "../public/calculatorModule";
import { CalculatorUI } from "../public/calculatorUI";
import { Calculus } from "../public/calculus";

/**
 * Setting up WebPage areas
 */
let navBar = document.createElement('div');
let navBarButton = document.createElement('button');
navBarButton.textContent = 'ADD CALCULATOR';
navBarButton.classList.add('btn','btn-info');

navBar.appendChild(navBarButton);
document.body.appendChild(navBar);

let centerArea = document.createElement('div');

document.body.appendChild(centerArea);

function addCalculator(){
    navBarButton.onclick = function() {
        let calcMod = new CalculatorModule();
        centerArea.appendChild(calcMod.calculatorContent); 
        new CalculatorUI(new Calculus(),calcMod);
    }
}

addCalculator();