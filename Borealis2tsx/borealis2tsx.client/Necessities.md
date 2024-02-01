# React + TypeScript + Vite + .Net

This file provides the necessary information to get the app up and running.
The app is a webapp running on React typescript for frontend, and C# .Net as a backend service.

## React
This application uses React typescript for it's large amount of libraries and UI frameworks
as opensource, to use for components in the application.
### Get Node.js running
To get react up and running you need:
- Install [Node.js](https://nodejs.org/en/download/current) (v21.5.0)
- Go to Borealis2tsx project, open terminal and execute to enter client folder.
```console
cd .\borealis2tsx.client\
```
- In client folder (with the node modules): run npm install to install all the react dependencies. 
Running npm install the program should install tailwind and other react libraries which you can find in package.json "devDependencies".
```console
npm install
```

### Tailwind 
Tailwind is a easier way to handle css, only in typescript.

[Tailwind documentation](https://tailwindcss.com/docs/)

### Dayjs 
Dayjs is used to manipulate dates and play with them just as mathematics.

[Dayjs documentation](https://day.js.org/docs/en/installation/typescript)

### Ag-grid 
Ag-grid is a table used to by big industrial firms to show data.
It's very easy to use, has plenty of features, and highly flexible.

[Ag-grid documentation](https://www.ag-grid.com/react-data-grid/getting-started/)

### Apex-charts 
Apex-charts is used to create graphs.
[Apex-charts documentation](https://apexcharts.com/javascript-chart-demos/line-charts/basic/)

### React Three Fiber 
React Three Fiber and React Three Drei is used for the 3D model.

[Video guide](https://www.youtube.com/watch?v=QaRIHrRclVk)


## C# .Net
This application uses C# .Net as a backend service. It uses system.IO.Ports to communicate between serial port and UI.

### Serial port
[Serial Port](https://learn.microsoft.com/en-us/dotnet/api/system.io.ports.serialport?view=dotnet-plat-ext-8.0)
reads the data from the microcontroller and sends it to react as a json.

### CSV management
For reading csv [CSV helper](https://joshclose.github.io/CsvHelper/getting-started/) is the optimal solution, since its quick and it does not store anything in memory.

For writing to csv [CSV appendText](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendtext?view=net-8.0)
is the best solution from the way we call our data.
The data is called once every second, 
where the data returned is a line of text from the microcontroller.
AppendText offers a solution where the data is inserted at the last line of the file, 
which csv helper does not offer,

