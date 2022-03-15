import { Equation } from "./equation";
/**
 * Businesslogic class to perform calculations and find errors
 * Todo: move errors to separate class
 */
 export class Calculus {

    equation: Equation;
    equationState: string | undefined;
    operators: string[];
    eqMode: number;

    constructor(){
        this.equation = new Equation();
        this.equationState = undefined;
        this.operators = ['+','-','*','/'];
        this.eqMode = 0;
    }

    actionCaller = (action:string) =>{
        console.log(action);
        console.log(JSON.stringify(this.equation));

        if (!(this.findErrorAction(action))){
            switch (action) {
                case 'AC':
                    this.equation.clear(); 
                    break;
                case '=':
                    this.equation.convertEquationToFullNumbers(this.operators);
                    this.calculate();
                    break;
                case 'keyboard_backspace':
                    this.equation.removeLast();
                    break; 
                case 'EQ MODE':
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
    setEqMode = () : number =>{
        return (this.eqMode === 1) ? 0 : 1;
    }

    //returns equation to screen
    getEquationState = () : string=>{
        return (typeof this.equationState === 'string') ? this.equationState! : 'Error';
    }

    //find user enetered problems
    findErrorAction = (action:string) : number =>{
        let error = 0;
        if (this.equation.getConcatEquation().length === 0 && this.equation.getIndex(this.operators,action) > -1 &&
            action !== '-') error = 1;
        else if ((this.isPreviousEntryAnOperator(this.operators) || this.isPreviousEntryAnOperator(['.'])) &&
                action === '=') error = 1;
        else if ((this.isPreviousEntryAnOperator(this.operators) || this.isPreviousEntryAnOperator(['.'])) &&
                (this.equation.getIndex(this.operators,action) > -1 || action === '.')) error = 1;
        return error;
    }

    isPreviousEntryAnOperator = (operators:string[]) =>{
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
    calculateIndexValue = (startIndex:number) =>{ 
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
    getNextCalculationIndex = () :number =>{
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

    getEquationIndex = (value:string) :number =>{
        return this.equation.getIndex(this.equation.equation,value);
    }
}