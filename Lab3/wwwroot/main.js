var points = [];
var realPoints = [];
var funcName = "sin(x)";
var basePoints = [];

function Draw() {
	var chart = new CanvasJS.Chart("graph", {
		animationEnabled: true,
		theme: "light2",
		axisY:{
			includeZero: false
		},
		data: [{        
			type: "line",       
			dataPoints: points,
			showInLegend: true,
			name: "Интерполяция",
			color: "#008080"
		},{ type: "line",
			dataPoints: realPoints,
			showInLegend: true,
			name: funcName,
			color: "#FF0000"
		},{
			type: "scatter",
			markerType: "triangle",
			dataPoints: basePoints,
			color: "#000000"
		}
		]
	});
	chart.render();
}

window.onload = function () {
	Draw(points);
}	

var parsedX = [];

function interpolate() {
	parsedX = [];
	basePoints = [];
	points = [];
	realPoints = [];
	let xData = $('#xData')[0].value.replace(/,/g , ".");
	if(!parseX(xData,parsedX)){
		return;
	}

	if (parsedX.length < 3) {
		writeError("Слишком мало входных данных");
		return false;
	}
	parsedX.sort(function(x, y) {
		return x - y;
	});

	
	let number = getFuncNumber();
	let offset = $('#offset')[0].value.replace(/,/g , ".");
	if (offset == "") offset = 0;

	let xHandledData = [];
	for (let index = 0; index < parsedX.length; index++) {
		xHandledData.push(parsedX[index].toString());
	}
	console.log(xHandledData.join(" "));
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
	funcName = stringValues[0];
	
	var index = 1;
	for (index; index <= parsedX.length; index++) {
		let y = parseFloat(stringValues[index]);
		let point3 = {'x': parsedX[index-1], 'y': y};
		console.log(point3);
		basePoints.push(point3);
	}

	for (index; index < stringValues.length; index++) {
		let x = parseFloat(stringValues[index]);
		index++;
		let y = parseFloat(stringValues[index]);
		index++;
		let realY = parseFloat(stringValues[index]);
		let point = {'x': x, 'y':y};
		points.push(point);	
		let point2 = {'x': x, 'y': realY};
		realPoints.push(point2);
	}
	Draw();
}

function parseX(xData, parsedX) {
	let splitXdata = xData.split(' ');
	for (let index = 0; index <= splitXdata.length; index++) {
		if (isStringNumber(splitXdata[index])){
			let newNumber = parseFloat(splitXdata[index]);
			if (isNaN(newNumber)){
				writeError("Необходимо вводить только вещественные числа");
				return false;
			}
			if (!parsedX.includes(newNumber)){
				parsedX.push(parseFloat(splitXdata[index]));
			}
		}	
	}
	return true;
}

function isStringNumber(stringNumber) {
	return stringNumber != " " && stringNumber != "" &&
	stringNumber != undefined && stringNumber != null &&
	stringNumber != "\n" && stringNumber != "\t";
}

function Clear(){
	writeError('');
}

function writeError(msg){
	document.getElementById('input-error-label').innerHTML = msg;
}