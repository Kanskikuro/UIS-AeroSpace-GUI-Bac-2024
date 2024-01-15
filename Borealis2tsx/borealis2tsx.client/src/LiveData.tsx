import {useEffect, useState} from 'react';
import './App.css';
import dayjs from 'dayjs'
import relativeTime from 'dayjs/plugin/relativeTime';

interface ReadDataPort {
    dataline: string[];
}

dayjs.extend(relativeTime);

// component
function LiveData() {
    // Variable declarations should always be in the top of the component
    const [Dataline, setDataline] = useState<ReadDataPort>();
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
        data.dataline.push(String(elapsed))
        setDataline(data);
        saveData.push(data)
        console.log(saveData)
    }
    
    //this is a variable inside the returns of the appcomponent 
    const contents = Dataline === undefined
        ? <p>Loading...
        </p>
        : <div>
            <p>Started session: {Dataline.dataline[13].split(".")[0]}s ago</p>
            <p>Date time: {dayjs().format(Dataline.dataline[0])}</p>
            <div id={"LiveDataContent"}>
                <div>
                    <b>Temp[graderC]</b>
                    <p>{Dataline.dataline[1]}</p>
                </div>
                <div>
                    <b>Pressure[mbar]</b>
                    <p>{Dataline.dataline[2]}</p>
                </div>
                <div>
                    <b>altitude[m]</b>
                    <p>{Dataline.dataline[3]}</p>
                </div>
                <div>
                    <b>accX[mg]</b>
                    <p>{Dataline.dataline[4]}</p>
                </div>
                <div>
                    <b>accY[mg]</b>
                    <p>{Dataline.dataline[5]}</p>
                </div>
                <div>
                    <b>accZ[mg]</b>
                    <p>{Dataline.dataline[6]}</p>
                </div>
                <div>
                    <b>gyroX[degrees/s]</b>
                    <p>{Dataline.dataline[7]}</p>
                </div>
                <div>
                    <b>gyroY[degrees/s]</b>
                    <p>{Dataline.dataline[8]}</p>
                </div>
                <div>
                    <b>gyroZ[degrees/s]</b>
                    <p>{Dataline.dataline[9]}</p>
                </div>
                <div>
                    <b>magX[µT]</b>
                    <p>{Dataline.dataline[10]}</p>
                </div>
                <div>
                    <b>magY[µT]</b>
                    <p>{Dataline.dataline[11]}</p>
                </div>
                <div>
                    <b>magZ[µT]</b>
                    <p>{Dataline.dataline[12]}</p>
                </div>
            </div>
        </div>;

    // this what the component returns
    return (
        <div className={"flex flex-col w-full items-center content-center mx-auto"}>
            <h1>UiS aerospace borealis 2.0</h1>
            <h3>This component demonstrates getting data from the microcontroller.</h3>
            <div>
                {contents}
            </div>
        </div>
    );
    
}

export default LiveData;