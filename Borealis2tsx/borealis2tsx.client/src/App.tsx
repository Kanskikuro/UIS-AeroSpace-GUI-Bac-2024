import { useEffect, useState } from 'react';
import './App.css';

interface Forecast {
    dataline: string[];
}

function App() {
    const [Dataline, setDataline] = useState<Forecast[]>();

    useEffect(() => {
        populateWeatherData();
    }, []);
    
    const contents = Dataline === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <div className={'flex justify-center items-center justify-items-center content-center'}>
            <table className="table table-striped text-black justify-self-center">
                <thead>
                    <tr className={"bg-[#FFF]"}>
                        <th>Temp[graderC]</th>
                        <th>Pressure[mbar]</th>
                        <th>altitude[m]</th>
                        <th>accX[mg]</th>
                        <th>accY[mg]</th>
                        <th>accZ[mg]</th>
                        <th>gyroX[degrees/s]</th>
                        <th>gyroY[degrees/s]</th>
                        <th>gyroZ[degrees/s]</th>
                        <th>magX[µT]</th>
                        <th>magY[µT]</th>
                        <th>magZ[µT]</th>
                    </tr>
                </thead>
                <tbody>
                    {Dataline.map((data, index: number) =>
                        <tr className={`${index % 2 === 0 ? "bg-[#F2F2F2]" : "bg-[#FFF]" }`}>
                            <td>{data.dataline[0]}</td>
                            <td>{data.dataline[1]}</td>
                            <td>{data.dataline[2]}</td>
                            <td>{data.dataline[3]}</td>
                            <td>{data.dataline[4]}</td>
                            <td>{data.dataline[5]}</td>
                            <td>{data.dataline[6]}</td>
                            <td>{data.dataline[7]}</td>
                            <td>{data.dataline[8]}</td>
                            <td>{data.dataline[9]}</td>
                            <td>{data.dataline[10]}</td>
                            <td>{data.dataline[11]}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>;

    return (
        <div className={"flex w-full items-center content-center mx-auto"}>
            <h1>UiS aerospace borealis 2.0</h1>
            <h3>This component demonstrates getting data from the microcontroller.</h3>
            <div className={"flex mx-auto justify-center items-center "}>
                {contents}
            </div>
        </div>
    );

    async function populateWeatherData() {
        const response = await fetch('readportdata');
        const data = await response.json();
        console.log(data)
        console.log(data[0])
        console.log(data[0].dataline, typeof data[0])
        console.log(data[0].dataline)
        setDataline(data); 
    }
}

export default App;