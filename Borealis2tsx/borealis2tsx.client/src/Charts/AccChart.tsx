import ReactApexChart from 'react-apexcharts';
import AccInterface from "../interfaces/AccInterface.ts";

function AccChart(props: { data: AccInterface[] }) {
    console.log(props)
    
    let chartOptions: {
        xaxis: { categories: string[] };
        series: { data: number[]; name: string }[];
        chart: { type: string }
    };
    chartOptions = {
        // Define your chart options here
        chart: {
            type: 'line',
        },
        series: [
            {
                name: 'AccX value',
                data: props.data.map(data => data.accX),
            },
        ],
        xaxis: {
            categories: props.data.map(data => data.time),
        },
    };
    return (
        <div>
            {props.data.length ?? ''}
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