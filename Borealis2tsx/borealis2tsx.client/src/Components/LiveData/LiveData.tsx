import {useEffect, useState} from 'react';
import dayjs from 'dayjs'
import relativeTime from 'dayjs/plugin/relativeTime';
import Dataline from "../../interfaces/Dataline.ts";
import AccInterface from "../../interfaces/AccInterface.ts";
import AccChart from "../Charts/AccChart.tsx";
import DataLineContent from "./DataLineContent.tsx";
import LiveDataTable from "../Tables/LiveDataTable.tsx";
import Rocket3Model from "../3dModel/Rocket3Model.tsx";
import AltitudeChart from "../Charts/AltitudeChart.tsx";
import AltitudeInterface from "../../interfaces/AltitudeInterface.ts";
dayjs.extend(relativeTime);

// component
function LiveData() {
    // Variable declarations should always be in the top of the component
    const [DataLine, setDataLine] = useState<Dataline>();
    const [saveData, setSaveData] = useState<Dataline[]>([]);
    const [saveAccData, setSaveAccData] = useState<AccInterface[]>([]);
    const [saveAltitudeData, setSaveAltitudeData] = useState<AltitudeInterface[]>([]);
    // UseEffect and other hooks should be after variables declaration but before functions
    useEffect(() => {
        const fetchData = async () => {
            await ReadDataPortLine();
        };

        const intervalId = setInterval(() => {
            fetchData()
        }, 700); // IMPORTANT: keep this above 1s so we have a primary key for the data,
        // primary key being time.

        // Cleanup the interval on component unmount
        return () => {
            clearInterval(intervalId);
        };
    }, [saveAccData]);

    // function shows the last 50 elements read since last refresh of the page.
    // if you rerender the page the values disappear, since it's not connected to a db
    // It's beneficial to have it locally since values are quickly produced, and are only meant for live read
    // there will be another page for reading data from db
    function updateLimitedData (data: any, storedData: Array<any>, setLimitedData: Function, limit: number): void{
        if (saveData.length >= limit){
            const [firstElement, ...rest] = storedData;
            rest.push(data)
            setLimitedData(rest)
        }else{
            setLimitedData(storedData => [...storedData, data]);
        }
        return;
    }
    // functions should be after Hooks or outside the component
    async function ReadDataPortLine() {
        const response = await fetch('readdataport');
        const data: Dataline = await response.json();
        setDataLine(data);
        const updateLimit: number = 60
        updateLimitedData(data, saveData, setSaveData, updateLimit)
        const AccLine: AccInterface = { 
            time: new Date(data.time), 
            accX: Number(data.accX), 
            accY: Number(data.accY), 
            accZ: Number(data.accZ)
        }
        updateLimitedData(AccLine, saveAccData, setSaveAccData, updateLimit)
        const AltitudeLine: AltitudeInterface = { 
            time: new Date(data.time), 
            altitude: Number(data.altitude)
        }
        updateLimitedData(AltitudeLine, saveAltitudeData, setSaveAltitudeData, updateLimit)
    }
    
    // this what the component returns
    return (
        <div className={"ml-5"}>
            <h1 className={'uppercase'}>UiS aerospace borealis 2.1</h1>
            <div>
                {DataLine === undefined ? <p>Loading</p> : 
                    <div className={'grid grid-cols-3'}>
                        <div className={'px-2'}>
                            <DataLineContent DataLine={DataLine} />
                            <AccChart data={saveAccData} />
                            <AltitudeChart data={saveAltitudeData} />
                        </div>
                        
                        <div className={'h-[300px] px-2 col-span-2'}>
                            <div className={'h-[650px]'}>
                                <Rocket3Model/>
                            </div>
                            <LiveDataTable data={saveData} />
                        </div>
                        
                        <div className={'px-2'}>
                            
                        </div>
                    </div>
                }
            </div>
        </div>
    );

}

export default LiveData;