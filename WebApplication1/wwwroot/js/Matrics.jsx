
const textInputStyle = {
    width: "40px",
    float: "left"
}

const textOutputStyle =
{
    float: "left"
}

var BarChart;

var modal = document.getElementById('modalConteiner');
var span = document.getElementById('closeModal');

span.onclick = function () {
    modal.style.display = 'none';
    BarChart.destroy();
}

function onCalculateButtonClick() {
    Metrics();
    CreateChart();
    modal.style.display = "block";
}
function Metrics() {

    var url = apiUrl + "/GetMetrics";
    fetch(url).then(res => res.json())
        .then(
            (result) => {
                var response = JSON.stringify(result);
                document.getElementById('metricsResult').innerHTML = "Rolling Retention 7 days = " + response 
            });
}

function CreateChart() {

    var dataHeaders = [];
    var lifeData = []
    var ctx = document.getElementById('BartChartCanv');
    var url = apiUrl + "/GetLifeCycle";
    fetch(url).then(res => res.json())
        .then(
            (result) => {
                var i = 0;
                for (key in result) {
                    dataHeaders[i] = key;
                    lifeData[i] = result[key];
                    i++;
                };
                drawChart();
            });

    drawChart = function(){
        BarChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: dataHeaders,
                datasets: [{
                    label: 'days quantity',
                    data: lifeData,
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
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    }
}


function CalculateForm() {
    return (
        <div>
            <input type="button" value="Calculate" style={buttonStyle} onClick={onCalculateButtonClick} />
            </div>
        )
}


ReactDOM.render(
        <CalculateForm />,
    document.getElementById("metrics")
);
