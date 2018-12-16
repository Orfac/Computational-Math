function Draw() {
    let chart = new CanvasJS.Chart("graph", {
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
            color: "#ff341d"
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