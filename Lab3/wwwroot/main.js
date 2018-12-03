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
			points = [];
			let floatValues = [];
			for (let index = 0; index < stringValues.length; index++) {
				floatValues = parseFloat(stringValues[index]);
			}
			floatValues.sort();
			for (let index = 0; index < stringValues.length; index++) {
				let x = parseFloat(stringValues[index]);
				index++;
				let y = parseFloat(stringValues[index]);
				if (Math.abs())
				let point = {'x': x, 'y':y};
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