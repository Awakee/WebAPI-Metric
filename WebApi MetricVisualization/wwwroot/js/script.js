$(document).ready(function () {
    getData();
    setInterval(function () {
        getData();
    }, 3000);

        function getData() {
            $.ajax({
                url: "https://localhost:44367/api/metric/interval/" + metricName,
                success: function (data) {
                    setInterval(callback(data), 2000);
                }
            });

            function callback(data) {
                function renderData() {
                    var array = new Array;
                    for (var k in data) {
                        array.push(Object.values(data[k]));
                    }
                    return array;
                }

                function time() {
                    var array = new Array;
                    var newData = renderData();
                    for (var k in newData) {
                        array.push(newData[k][0])
                    }
                    return array;
                }

                function count() {
                    var array = new Array;
                    var newData = renderData();
                    for (var k in newData) {
                        array.push(newData[k][1])
                    }
                    return array;
                }

                function update(chart) {
                    chart.data.labels = time();
                    chart.data.datasets[0].data = count();
                    chart.update();
                }

                update(myLineChart);

            }
        }


        

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

