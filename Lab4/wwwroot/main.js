var points = [];
var funcName = "sin(x)";



window.onload = function () {
    Draw(points);
};

function differentiate() {
    clearPoints();

    let x0 = getField('x0');
    if (x0 === undefined) return;
    let y0 = getField('y0');
    if (y0=== undefined) return;
    let xn = getField('xn');
    if (xn === undefined) return;
    let accuracy = getField('accuracy');
    if (accuracy === undefined) return;
    
    if (isNaN(parseFloat(x0)) || isNaN(parseFloat(y0)) ||
        isNaN(parseFloat(xn)) || isNaN(parseFloat(accuracy))) {
        Clear();
        writeError('Только вещ. числа');
        return;
    }

    let funcNumber = getFuncNumber();
    doRequest(x0, y0, xn, accuracy, funcNumber);
}

function singleRequest() {
    let xi = getField('xi');
    if (xi === undefined) return;
    if (isNaN(xi)){
        writeError('Только вещ. числа')
        return;
    }

    $.ajax({
        type: 'GET',
        url: "getSingle",
        data: {
            'xi': xi
        },
        success: function (data, textStatus, xhr) {
            $('#yi')[0].value = data;
        },
        error: function (a, jqXHR, exception) {
            onError();
        }
    });
}

function doRequest(x0, y0, xn, accuracy, funcNumber) {
    $.ajax({
        type: 'GET',
        url: "diffirentiate",
        data: {
            'x0': x0,
            'y0': y0,
            'xn': xn,
            'accuracy': accuracy,
            'funcNumber': funcNumber
        },
        success: function (data, textStatus, xhr) {
            onSuccess(data);
        },
        error: function (a, jqXHR, exception) {
            onError();
        }
    });
}

function onSuccess(data) {
    let stringValues = data.replace(/,/g, ".").split(" ");
    parseResponse(stringValues);
    Draw();
}

function onError() {
    writeError("Не удалось получить ответ от сервера");
}