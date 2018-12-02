var points = [
	{ y: 450, x: 5.13},
	{ y: 414, x: 3},
	{ y: 524,x: 3},
	{ y: 460,x: 3 },
	{ y: 450,x: 3 },
	{ y: 500,x: 3 },
	{ y: 480,x: 3 },
	{ y: 480,x: 3 },
	{ y: 410,x: 3 },
	{ y: 500,x: 3 },
	{ y: 480,x: 9 },
	{ y: 510,x: 3 }
];

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
	

	$.ajax({
		type: 'GET',
		url: "Interpolate",
		data: { 'xData': xData, 'yData': yData},
		success: function (data,textStatus, xhr) { console.log(data)},
		error: function (a, jqXHR, exception) { }
	});
}

function sinX(x){
	return Math.sin(x);
}