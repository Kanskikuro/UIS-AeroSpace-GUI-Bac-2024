import {useEffect, useState} from 'react';
import './App.css';
import * as dayjs from 'dayjs'
import relativeTime from 'dayjs/plugin/relativeTime';

interface ReadDataPort {
    dataline: string[];
}

dayjs.extend(relativeTime);

// component
function App() {
    // Variable declarations should always be in the top of the component
    const [Dataline, setDataline] = useState<ReadDataPort>();
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
        var elapsed : number = ((new Date()).getTime() - (startingTime).getTime())/1000;
        data.dataline.push(String(elapsed))
        setDataline(data);
        
         
    }
    
    //this is a variable inside the returns of the appcomponent 
    const contents = Dataline === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a
            href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em>
        </p>
        : <div className={'flex justify-center items-center justify-items-center content-center'}>
            <p>Started session: {Dataline.dataline[13].split(".")[0]}s ago</p>
            <table className="table table-striped text-black justify-self-center">
                <thead>
                <tr className={"bg-[#FFF]"}>
                    <th>Datetime</th>
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
                <tr>
                    <td>{dayjs().format(Dataline.dataline[0])}</td>
                    <td>{Dataline.dataline[1]}</td>
                    <td>{Dataline.dataline[2]}</td>
                    <td>{Dataline.dataline[3]}</td>
                    <td>{Dataline.dataline[4]}</td>
                    <td>{Dataline.dataline[5]}</td>
                    <td>{Dataline.dataline[6]}</td>
                    <td>{Dataline.dataline[7]}</td>
                    <td>{Dataline.dataline[8]}</td>
                    <td>{Dataline.dataline[9]}</td>
                    <td>{Dataline.dataline[10]}</td>
                    <td>{Dataline.dataline[11]}</td>
                    <td>{Dataline.dataline[12]}</td>
                </tr>
                </tbody>
            </table>
        </div>;

    // this what the component returns
    return (
        <div className={"flex w-full items-center content-center mx-auto"}>
            <h1>UiS aerospace borealis 2.0</h1>
            <h3>This component demonstrates getting data from the microcontroller.</h3>
            <div className={"flex mx-auto justify-center items-center "}>
                {contents}
            </div>
        </div>
    );
    
}

export default App;