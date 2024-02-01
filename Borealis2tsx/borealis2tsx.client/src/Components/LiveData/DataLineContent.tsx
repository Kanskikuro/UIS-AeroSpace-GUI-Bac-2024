import ReadDataPort from "../../interfaces/ReadDataPort.ts";

function DataLineContent(props: { DataLine: ReadDataPort | undefined }) {
    const DataLine: ReadDataPort | undefined = props.DataLine
    return (
        <div>
            <div className={"flex w-full justify-center"}>
                <div id={"LiveDataContent"} className={'grid grid-cols-3 p-[10px] bg-blue-50 w-full text-center'}>
                    <div>
                        <b>Temp[graderC]</b>
                        <p>{DataLine?.temperature ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>Pressure[mbar]</b>
                        <p>{DataLine?.pressure ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>altitude[m]</b>
                        <p>{DataLine?.altitude ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>accX[mg]</b>
                        <p>{DataLine?.accX ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>accY[mg]</b>
                        <p>{DataLine?.accY ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>accZ[mg]</b>
                        <p>{DataLine?.accZ ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>gyroX[degrees/s]</b>
                        <p>{DataLine?.gyroX ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>gyroY[degrees/s]</b>
                        <p>{DataLine?.gyroY ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>gyroZ[degrees/s]</b>
                        <p>{DataLine?.gyroZ ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>magX[µT]</b>
                        <p>{DataLine?.magX ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>magY[µT]</b>
                        <p>{DataLine?.magY ?? "No Data"}</p>
                    </div>
                    <div>
                        <b>magZ[µT]</b>
                        <p>{DataLine?.magZ ?? "No Data"}</p>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default DataLineContent;