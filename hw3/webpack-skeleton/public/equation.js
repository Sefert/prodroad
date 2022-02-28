/**
 * Equation is class to preserve user clicked
 * information and perform basic operations with
 * array equation
 */
 export class Equation {

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