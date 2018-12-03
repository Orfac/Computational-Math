var points = [];
var realPoints = [
	
];
var funcName = "sin(x)";

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
			color: "#F08080"
		},{ type: "line",
			dataPoints: realPoints,
			showInLegend: true,
			name: funcName
		}
		]
	});
	chart.render();
}

window.onload = function () {
	Draw(points);
}	

function interpolate() {
	let xData = $('#xData')[0].value;
	let number = getFuncNumber();

	$.ajax({
		type: 'GET',
		url: "Interpolate",
		data: { 'xData': xData, 'funcNumber':number},
		success: function (data,textStatus, xhr) {
			let stringValues = data.split(" ");
			points = [];
			let floatValues = [];
			for (let index = 0; index < stringValues.length; index++) {
				floatValues = parseFloat(stringValues[index]);
			}
			for (let index = 0; index < stringValues.length; index++) {
				let x = parseFloat(stringValues[index]);
				index++;
				let y = parseFloat(stringValues[index]);
				index++;
				let realY = parseFloat(stringValues[index]);
				let point = {'x': x, 'y':y};
				points.push(point);	
				point['y'] = realY;
				realPoints.push(point);
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