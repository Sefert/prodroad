import { ModuleId } from "../public/moduleId";

export class CalculatorModule{

    calculatorContent;
   // moduleId;

    constructor(moduleId){
        //this.moduleId=ModuleId.getNextId();
        this.calculatorContent = document.createElement("div");
        this.calculatorContent.className = "calculator card"+moduleId;
        this.calculatorContent.style.display = 'inline-block';

        //this.calculatorContent.appendChild()

        this.calculatorContent.appendChild(this.calculatorKey('close','material-icons','btn-info','button-close'));

        this.calculatorContent.appendChild(this.calculatorScreen("calculator-screen","z-depth-1"));

        let calculatorKeyModule = this.calculatorKeyModule();
        ['+','-','*','/'].forEach(operator =>{
            calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-info','btn'));
        });
        ['7','8','9'].forEach(operator =>{
            calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-light','waves-effect','btn'));
        });
        calculatorKeyModule.appendChild(this.calculatorKey('keyboard_backspace','operator','material-icons','btn-info','btn'));
        ['4','5','6'].forEach(operator =>{
            calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-light','waves-effect','btn'));
        });
        calculatorKeyModule.appendChild(this.calculatorKey('EQ MODE','operator','btn-info','btn'));
        ['1','2','3','0'].forEach(operator =>{
            calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-light','waves-effect','btn'));
        });
        calculatorKeyModule.appendChild(this.calculatorKey('.','decimal','function','btn-secondary','btn'));
        calculatorKeyModule.appendChild(this.calculatorKey('AC','all-clear','function','btn-danger','btn-sm','btn'));
        calculatorKeyModule.appendChild(this.calculatorKey('=','equal-sign','operator','btn-danger','btn-sm','btn'));

        this.calculatorContent.append(calculatorKeyModule);

        this.calculatorContent.childNodes.forEach(node =>{         
            node.onclick = (() => {
                if (node.classList.contains('button-close')) {
                    this.calculatorContent.remove();
                    ModuleId.freeId(moduleId);
                }
            })          
        });
    }

    /*closeModule(...cssClasses){
        let closeModule = document.createElement("div");
        closeModule.className = "calculator-keys"
        return calcModule;
    }*/

    calculatorScreen(...cssClasses){
        let calculatorScreen = document.createElement("input");
        calculatorScreen.type = "text";

        cssClasses.forEach(cssClass => {
            calculatorScreen.classList.add(cssClass);
        });

        calculatorScreen.value = "";
        calculatorScreen.disabled = true;
        return calculatorScreen;
    }

    calculatorKeyModule(){
        let calcModule = document.createElement("div");
        calcModule.className = "calculator-keys"
        return calcModule;
    }

    calculatorKey(text,...cssClasses){
        let button = document.createElement("button");
        //button.classList.add('btn');

        cssClasses.forEach(cssClass => {
            button.classList.add(cssClass);
        });
        
        button.value = text;
        button.textContent = text;
        return button;
    }

    getCalculatorModule = () => {
        return this.calculatorContent;
    }
}
