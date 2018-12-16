var points = [];
var realPoints = [];
var funcName = "sin(x)";
var basePoints = [];



window.onload = function () {
	Draw(points);
};

var parsedX = [];

function interpolate() {
	clearPoints();
	
	let number = getFuncNumber();
	let offset = getOffset();
	let xHandledData = parseX();
	
	doRequest(xHandledData,number,offset);
}


function doRequest(xHandledData,number,offset) {
    $.ajax({
        type: 'GET',
        url: "Interpolate",
        data: { 'xData': xHandledData.join(" "), 'funcNumber':number, 'offset': offset},
        success: function (data,textStatus, xhr) {
            onSuccess(data);
        },
        error: function (a, jqXHR, exception) {
            onError();
        }
    });
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

function onSuccess(data) {
	let stringValues = data.split(" ");
	parseResponse(stringValues);
	Draw();
}

function onError() {
    writeError("Не удалось получить ответ от сервера");
}