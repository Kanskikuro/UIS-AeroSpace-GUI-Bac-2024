import ReactApexChart from 'react-apexcharts';
import AccInterface from "../../interfaces/AccInterface.ts";

function AccChart(props: { data: AccInterface[] }) {
    let chartOptions: {
        xaxis: { categories: string[] };
        series: { data: number[]; name: string }[];
        chart: { type: string }
    };
    chartOptions = {
        // Define your chart options here
        chart: {
            id: 'realtime',
            type: 'line',
            stroke: {
                curve: 'smooth'
            },
            toolbar: {
                show: false
            },
            zoom: {
                enabled: false
            }
        },
        series: [
            {
                name: 'AccX value',
                data: props.data.map(data => data.accX),
            },
            {
                name: 'AccY value',
                data: props.data.map(data => data.accY),
            },
            {
                name: 'AccZ value',
                data: props.data.map(data => data.accZ),
            },
        ],
        xaxis: {
            categories: props.data.map(data => data.startTime.toLocaleTimeString()),
            tickAmount: 10,
        },
    };
    
    return (
        <div>
            <ReactApexChart
                options={chartOptions}
                series={chartOptions.series}
                type="line"
                height={350}
            />
        </div>
    )
}

export default AccChart