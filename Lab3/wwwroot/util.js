function clearPoints() {
    points = [];
    basePoints = [];
    realPoints = [];
    parsedX = [];
}

function getOffset() {
    let offset = $('#offset')[0].value.replace(/,/g , ".");
    if ("" === offset) offset = 0;
    return offset;
}


function getFuncNumber() {
    let e = $('#e');
    let square = $('#square');
    let sin = $('#sin');

    if (square[0].checked){
        return 0;
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

