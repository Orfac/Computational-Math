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
            name: "Решение ОДУ",
            color: "#008080"
        }
        ]
    });
    chart.render();
}