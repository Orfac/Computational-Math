var points = [];

function Draw(points) {
	var chart = new CanvasJS.Chart("graph", {
		animationEnabled: true,
		theme: "light2",
		title:{
			text: ""
		},
		axisY:{
			includeZero: false
		},
		data: [{        
			type: "line",       
			dataPoints: points
		}]
	});
	chart.render();
}

window.onload = function () {
	Draw(points);
}	

function interpolate() {
	let xData = $('#xData')[0].value;
	$.ajax({
		type: 'GET',
		url: "Interpolate",
		data: { 'xData': xData},
		success: function (data,textStatus, xhr) {
			let stringValues = data.split(" ");
			let values = [];
			for (let index = 0; index < stringValues.length; index++) {
				values[index] = parseFloat(stringValues[index]);
			}
			points = [];
			for (let i = 0; i < values.length; i++) {
				let point = {'x': i, 'y': values[i]};
				points.push(point);	
			}
			Draw(points);
		},
		error: function (a, jqXHR, exception) { }
	});
}

function sinX(x){
	return Math.sin(x);
}