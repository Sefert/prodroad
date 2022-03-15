/**
 * Equation is class to preserve user clicked
 * information and perform basic operations with
 * array equation
 */
 export class Equation {

    equation: string[] = [];
    number: string[] = [];

    constructor(){
        this.equation = [];
    }

    clear(){
        this.equation = [];
        this.number= [];
        console.log(JSON.stringify(this.equation));
    }

    addElement = (elem:string) => {
        this.equation.push(elem);
        console.log(JSON.stringify(this.equation));
    }


    // all individual numeric elements between symbols are converted to full numbers
    convertEquationToFullNumbers = (operators:string[]) =>{
        let index: number = 0;
        let number: string = "";
        let compressedEquation: string[] = [];

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

    getNumberOfSymbolsInEquation = (operators:string[]) =>{
        let counter = 0;

        this.equation.forEach(elem => {
            if (this.getIndex(operators,elem) > -1) counter +=1; 
        });

        return counter;
    }

    getIndex(operators:string[],sign:string) :number{
        return operators.findIndex((element) => element === sign);
    }

    getConcatEquation = () :string =>{ 
        return this.equation.join('');
    }

    getIndexValue = (index:number) :string => {
        return this.equation[index];
    }

    getPreviousValue = (index:number) :string => {
        return this.equation[index-1];
    }

    getNextValue = (index:number) :string => {
        return this.equation[index+1];
    }

    changeEquation = (index:number,value:string) =>{
        if (index  >= 0) this.equation.splice(index - 1,3,value);
    }

    removeLast = () =>{
        this.equation.pop();
    }
}