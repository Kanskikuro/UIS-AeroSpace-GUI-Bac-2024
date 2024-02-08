import ReactApexChart from 'react-apexcharts';
import AltitudeInterface from "../../interfaces/AltitudeInterface.ts";

function AltitudeChart(props: { data: AltitudeInterface[] }) {
    let chartOptions: {
        xaxis: { categories: string[] };
        series: { data: number[]; name: string }[];
        chart: { type: string }
    };
    chartOptions = {
        // Define your chart options here
        chart: {
            id: 'Altitude',
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
                name: 'Altitude data',
                data: props.data.map(data => data.altitude)
            },
        ],
        xaxis: {
            categories: props.data.map(data => data.time.toLocaleTimeString()),
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

export default AltitudeChart