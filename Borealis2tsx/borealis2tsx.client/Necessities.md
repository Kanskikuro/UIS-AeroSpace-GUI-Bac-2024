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
[Tailwind documentation](https://tailwindcss.com/docs/installation)

## C# .Net
This application uses C# .Net as a backend service. It uses system.IO.Ports to communicate between serial port and UI.
### Serial port
[Serial Port](https://learn.microsoft.com/en-us/dotnet/api/system.io.ports.serialport?view=dotnet-plat-ext-8.0)