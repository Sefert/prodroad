export class CalculatorModule{

    calculatorContent;

    constructor(){
        this.calculatorContent = document.createElement("div");
        this.calculatorContent.className = "calculator card";
        this.calculatorContent.appendChild(this.calculatorScreen("calculator-screen","z-depth-1"));

        let calculatorKeyModule = this.calculatorKeyModule();
        ['+','-','*','/'].forEach(operator =>{
            calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-info'));
        });
        ['7','8','9'].forEach(operator =>{
            calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-light','waves-effect'));
        });
        calculatorKeyModule.appendChild(this.calculatorKey('keyboard_backspace','operator','material-icons','btn-info'));
        ['4','5','6'].forEach(operator =>{
            calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-light','waves-effect'));
        });
        calculatorKeyModule.appendChild(this.calculatorKey('EQ MODE','operator','btn-info'));
        ['1','2','3','0'].forEach(operator =>{
            calculatorKeyModule.appendChild(this.calculatorKey(operator,'btn-light','waves-effect'));
        });
        calculatorKeyModule.appendChild(this.calculatorKey('.','decimal','function','btn-secondary'));
        calculatorKeyModule.appendChild(this.calculatorKey('AC','all-clear','function','btn-danger','btn-sm'));
        calculatorKeyModule.appendChild(this.calculatorKey('=','equal-sign','operator','btn-danger','btn-sm'));

        this.calculatorContent.append(calculatorKeyModule);
    }

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
        button.classList.add('btn');

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
