"use strict";
import css from "./index.css"
import { CalculatorModule } from "../public/calculatorModule";
import { CalculatorUI } from "../public/calculatorUI";
import { Calculus } from "../public/calculus";
import { ModuleId } from "../public/moduleId";

/**
 * Calling calculation program
 */
let navBar = document.createElement('div');
let navBarButton = document.createElement('button');
navBarButton.textContent = 'ADD CALCULATOR';

navBar.appendChild(navBarButton);
document.body.appendChild(navBar);

let centerArea = document.createElement('div');

document.body.appendChild(centerArea);

function addCalculator(){
    navBarButton.onclick = function() {
        let calcMod = new CalculatorModule(ModuleId.getNextId());
        centerArea.appendChild(calcMod.getCalculatorModule()); 
        new CalculatorUI(new Calculus(),ModuleId.idCounter)
    }
}

addCalculator();