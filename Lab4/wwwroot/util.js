function clearPoints() {
    basePoints = [];
    points = [];
    realPoints = [];
}

function getField(name) {
    let name2 = '#' + name;
    let field = $('#' + name)[0].value.replace(/,/g , ".");
    if ("" === field) {
        writeError("Забыли обязательное поле ", name);
        return undefined;
    }
    return field;
}


function getFuncNumber() {
    let sin = $('#sin');
    let cube = $('#cube');
    let square = $('#square');

    if (square[0].checked){
        return 3;
    } else {
        if (sin[0].checked){
            return 1;
        } else {
            return 2;
        }
    }
}


function Clear(){
    writeError('');
}

function writeError(msg){
    document.getElementById('input-error-label').innerHTML = msg;
}

