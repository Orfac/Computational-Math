function clearPoints() {
    points = [];
}

function getField(name) {
    let name2 = '#' + name;
    let field = $('#' + name)[0].value.replace(/,/g , ".");
    if ("" === field) {
        writeError("Не заполнено поле! ", name);
        return undefined;
    }
    return field;
}


function getFuncNumber() {
    let sin = $('#sin');
    let cube = $('#cube');
    let square = $('#square');

    if (square[0].checked){
        return 2;
    } else {
        if (sin[0].checked){
            return 0;
        } else {
            return 1;
        }
    }
}


function Clear(){
    writeError('');
}

function writeError(msg){
    document.getElementById('input-error-label').innerHTML = msg;
}

