var points = [];
var realPoints = [];
var funcName = "sin(x)";
var basePoints = [];

function Draw(points) {
	var chart = new CanvasJS.Chart("graph", {
		animationEnabled: true,
		theme: "light2",
		title:{
			text: "Графики функции"
		},
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

var prevX = [];

function interpolate() {
	prevX = [];
	let xData = $('#xData')[0].value;
	let splitXdata = xData.split(' ');
	splitXdata.sort();
	for (let index = 0; index <= splitXdata.length; index++) {
		console.log(splitXdata[index]);
		if (splitXdata[index] != " " && splitXdata[index] != "")
			prevX.push(parseFloat(splitXdata[index]));
	}
	prevX.sort();
	let check = false;
	for (let index = 0; index < prevX.length; index++) {
		for (let index2 = 0; index2 < prevX.length; index2++) {
			const element = prevX[index];
			
		}
		
	}
	
	if (splitXdata.length < 3) return;
	let number = getFuncNumber();
	let offset = $('#offset')[0].value;
	if (offset == "") offset = 0;
	$.ajax({
		type: 'GET',
		url: "Interpolate",
		data: { 'xData': xData, 'funcNumber':number, 'offset': offset},
		success: function (data,textStatus, xhr) {
			let stringValues = data.split(" ");
			basePoints = [];
			funcName = stringValues[0];
			points = [];
			realPoints = [];
			var index = 1;
			for (index; index < prevX.length; index++) {
				let y = parseFloat(stringValues[index]);
				let point3 = {'x': prevX[index-1], 'y': y};
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
			Draw(points);
		},
		error: function (a, jqXHR, exception) { }
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