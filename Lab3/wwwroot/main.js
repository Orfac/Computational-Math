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
	{ y: 480,x: 3 },
	{ y: 510,x: 3 }
];

function Draw(points) {
	var chart = new CanvasJS.Chart("graph", {
		animationEnabled: true,
		theme: "light2",
		title:{
			text: "График функции"
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