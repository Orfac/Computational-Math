function parseResponse(stringValues){
    for (let index = 0; index < stringValues.length; index++) {
        let x = parseFloat(stringValues[index]);
        let y = parseFloat(stringValues[index]);
        let point = {'x': x, 'y': y};
        points.push(point);
    }
}