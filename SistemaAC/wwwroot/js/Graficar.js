
class Graficas {
    constructor() {

    }
    estadosCursos(response, div) {
        var array = [
            ["", 'Activos', 'No Activos'],
            ["Cursos", response[0], response[1]]
        ];
        //var div = "graficasCursos";
        var subtitle = "Estados de cursos";
        this.dataChar(array, subtitle, div);
    }

    dataChar(array, subtitle, div) {
        google.charts.load('current', { 'packages': ['bar'] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable(array);
            var options = {
                chart: {
                    title: "",
                    subtitle: "",
                },
                bars: 'vertical' // Required for Material Bar Charts
            };
            var chart = new google.charts.Bar(document.getElementById(div));
            chart.draw(data, google.charts.Bar.convertOptions(options));
        }

    }
}
