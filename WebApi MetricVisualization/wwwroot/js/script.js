$(document).ready(function () {
    var newmassive;
    setInterval(function () {

        $.ajax({
            url: "https://localhost:44367/api/metric/interval/" + metricName,
            success: function (data) {
                setInterval(callback(data), 2000);
            }
        });

        function callback(data) {
            newmassive = data;

            function time() {
                var array2 = data;
                return Object.keys(array2);
            }

            function count() {
                var array = new Array;
                for (var k in data) {
                    array.push(data[k])
                };
                return array;
            }

            function update(chart) {
                chart.data.labels = time();
                chart.data.datasets[0].data = count();
                chart.update();
            }

            update(myLineChart);

        };
    }, 3000 );

    var ctx = document.getElementById('myChart').getContext('2d');
    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [],
            datasets: [{
                label: '# of counts',
                data: [0],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                fill: false,
            }],
        },
        options: {
            legend: {
                labels: {
                    fontColor: "white",
                    fontSize: 18
                }
            },
            scales: {
                xAxes: [{
                    gridLines: {
                        display: true,
                        color: "#FFFFFF"
                    },
                    ticks: {
                        fontColor: "white",
                        fontSize: 18,
                        beginAtZero: true
                    }
                }],
                yAxes: [{
                    gridLines: {
                        display: true,
                        color: "#FFFFFF"
                    },
                    ticks: {
                        fontColor: "white",
                        fontSize: 18,
                        stepSize: 1,
                        beginAtZero: true
                    }
                }],
            },
        },
    });
});

