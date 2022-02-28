"use strict";
import css from "./index.css"
import { CalculatorModule } from "../public/calculatorModule";


/**
 * Calling calculation program
 */

let navBar = document.createElement('div');
let navBarButton = document.createElement('button');
navBarButton.textContent = 'ADD CALCULATOR';
navBarButton.classList.add('btn','btn-add','btn-info');

navBar.appendChild(navBarButton);
document.body.appendChild(navBar);
let data = 'aa';

function addCalculator(){
    let calcMod = new CalculatorModule();
    navBarButton.onclick = () => {
        navBar.appendChild(calcMod.getCalculatorModule());
        console.log(data);
    }
}

addCalculator();