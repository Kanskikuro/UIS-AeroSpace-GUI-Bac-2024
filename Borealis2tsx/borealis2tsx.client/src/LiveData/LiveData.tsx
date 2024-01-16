import {useEffect, useState} from 'react';
// import './App.css';
import dayjs from 'dayjs'
import relativeTime from 'dayjs/plugin/relativeTime';
import ReadDataPort from "../interfaces/ReadDataPort.ts";
dayjs.extend(relativeTime);

// component
function LiveData() {
    // Variable declarations should always be in the top of the component
    const [DataLine, setDataLine] = useState<ReadDataPort>();
    const saveData: ReadDataPort[] = [];
    let startingTime = new Date();
    // UseEffect and other hooks should be after variables declaration but before functions
    useEffect(() => {
        const fetchData = async () => {
            await ReadDataPortLine();
        };

        const intervalId = setInterval(() => {
            fetchData()
        }, 500);

        // Cleanup the interval on component unmount
        return () => {
            clearInterval(intervalId);
        };
    }, []);

    // functions should be after Hooks or outside the component
    async function ReadDataPortLine() {
        const response = await fetch('readportdata');
        const data: ReadDataPort = await response.json();
        const elapsed: number = ((new Date()).getTime() - (startingTime).getTime()) / 1000;
        data.interval = String(elapsed) + "s";
        setDataLine(data);
        saveData.push(data)
        // console.log(saveData)
        
    }

    //this is a variable inside the returns of the appcomponent 
    const contents = DataLine === undefined
        ? <p>Loading...
        </p>
        : <div>
            <p>Started session: {DataLine.interval.split(".")[0]}s ago</p>
            <p>Date time: {DataLine.startTime}</p>
            <div className={"flex w-[750px] justify-center"}>
                <div id={"LiveDataContent"} className={'grid grid-cols-3 p-[10px] bg-blue-50 w-full text-center'}>
                    <div>
                        <b>Temp[graderC]</b>
                        <p>{DataLine.temperature ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>Pressure[mbar]</b>
                        <p>{DataLine.pressure ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>altitude[m]</b>
                        <p>{DataLine.altitude ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>accX[mg]</b>
                        <p>{DataLine.accX ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>accY[mg]</b>
                        <p>{DataLine.accY ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>accZ[mg]</b>
                        <p>{DataLine.accZ ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>gyroX[degrees/s]</b>
                        <p>{DataLine.gyroX ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>gyroY[degrees/s]</b>
                        <p>{DataLine.gyroY ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>gyroZ[degrees/s]</b>
                        <p>{DataLine.gyroZ ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>magX[µT]</b>
                        <p>{DataLine.magX ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>magY[µT]</b>
                        <p>{DataLine.magY ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>magZ[µT]</b>
                        <p>{DataLine.magZ ?? "No Data"}</p>
                    </div>
                </div>
            </div>
        </div>;

    // this what the component returns
    return (
        <div className={"ml-5"}>
            <h1>UiS aerospace borealis 2.0</h1>
            <h3 className={'text-teal-900'}>This component demonstrates getting data from the microcontroller.</h3>
            <div>
                {contents}
            </div>
        </div>
    );

}

export default LiveData;