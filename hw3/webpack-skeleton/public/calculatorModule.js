import { ModuleId } from "../public/moduleId";
/**
 * Class which groups together all html elements for calculator
 */
export class CalculatorModule{

    calculatorContent;
    calculatorScreen;
    calculatorKeyModule;
    closeKey;
    moduleId;

    constructor(){
        this.moduleId=ModuleId.getNextId();
        this.calculatorContent = document.createElement("div");
        this.calculatorContent.className = "calculator card"+this.moduleId;
        this.calculatorContent.style.display = 'inline-block';

        this.closeKey = this.calculatorKey('close','material-icons','btn-info','button-close')
        this.calculatorContent.appendChild(this.closeKey);

        this.calculatorScreen = this.setCalculatorScreen("calculator-screen","z-depth-1");
        this.calculatorContent.appendChild(this.calculatorScreen);

        this.calculatorKeyModule = this.setCalculatorKeyModule();
        ['+','-','*','/'].forEach(operator =>{
            this.calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-info','btn'));
        });
        ['7','8','9'].forEach(operator =>{
            this.calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-light','waves-effect','btn'));
        });
        this.calculatorKeyModule.appendChild(this.calculatorKey('keyboard_backspace','operator','material-icons','btn-info','btn'));
        ['4','5','6'].forEach(operator =>{
            this.calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-light','waves-effect','btn'));
        });
        this.calculatorKeyModule.appendChild(this.calculatorKey('EQ MODE','operator','btn-info','btn'));
        ['1','2','3','0'].forEach(operator =>{
            this.calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-light','waves-effect','btn'));
        });
        this.calculatorKeyModule.appendChild(this.calculatorKey('.','decimal','function','btn-secondary','btn'));
        this.calculatorKeyModule.appendChild(this.calculatorKey('AC','all-clear','function','btn-danger','btn-sm','btn'));
        this.calculatorKeyModule.appendChild(this.calculatorKey('=','equal-sign','operator','btn-danger','btn-sm','btn'));

        this.calculatorContent.append(this.calculatorKeyModule);
    }

    freeModuleId(){
        ModuleId.freeId(this.moduleId);
    }

    setCalculatorScreen(...cssClasses){
        let calculatorScreen = document.createElement("input");
        calculatorScreen.type = "text";

        cssClasses.forEach(cssClass => {
            calculatorScreen.classList.add(cssClass);
        });

        calculatorScreen.value = "";
        calculatorScreen.disabled = true;
        return calculatorScreen;
    }

    setCalculatorKeyModule(){
        let calcModule = document.createElement("div");
        calcModule.className = "calculator-keys"
        return calcModule;
    }

    calculatorKey(text,...cssClasses){
        let button = document.createElement("button");


        cssClasses.forEach(cssClass => {
            button.classList.add(cssClass);
        });
        
        button.value = text;
        button.textContent = text;
        return button;
    }
}
