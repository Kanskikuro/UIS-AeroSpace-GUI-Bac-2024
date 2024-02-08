import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-quartz.css';
import {AgGridReact} from 'ag-grid-react';
import {useMemo, useState} from 'react';
import Dataline from "../../interfaces/Dataline.ts";

const LiveDataTable = (props: { data: Dataline[] }) => {
    const containerStyle = useMemo(() => ({width: '100%', height: '100%'}), []);
    const gridStyle = useMemo(() => ({height: '100%', width: '100%'}), []);
    const [columnDefs, setColumnDefs] = useState([
        {field: 'time', sort: 'desc', width: '200px'},
        {field: 'temperature'},
        {field: 'pressure'},
        {field: 'altitude'},
        {field: 'accX'},
        {field: 'accY'},
        {field: 'accZ'},
        {field: 'gyroX'},
        {field: 'gyroY'},
        {field: 'gyroZ'},
        {field: 'magX'},
        {field: 'magY'},
        {field: 'magZ'},
    ]);
    const defaultColDef = useMemo(() => {
        return {
            width: 100,
        };
    }, []);

    return (
        <div style={containerStyle}>
            <div
                style={gridStyle}
                className={"ag-theme-quartz"}
            >
                <AgGridReact
                    rowData={props.data}
                    columnDefs={columnDefs}
                    defaultColDef={defaultColDef}
                />
            </div>
        </div>
    );
};
export default LiveDataTable;