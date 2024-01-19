# Uis Areospace Borealis 2.1 GUI

## Borealis 2.1
Borealis 2.1 is a collaborative effort within UIS Aerospace, developed as part of a bachelor's thesis project. The primary objective of Borealis 2.1 is to build upon the lessons learned from Borealis 1.0, aiming to be a significant improvement. One key lesson from Borealis 1.0 was the need to automate data recording to prevent oversight, especially given the loss of locally saved data when the previous model crashed at a speed of 700m/s.

## Description
The Borealis 2.1 GUI is designed to receive inputs from the Borealis 2.1 model rocket and present the collected data in a user-friendly interface.

### Technology stack
For Borealis 1.0, Python with PyQt was predominantly used. In Borealis 2.1, we have chosen to utilize .Net and React due to their extensive toolsets for building graphical user interfaces.

Frontend:
1. React (Node.js, Vite.te)
  - Tailwind
  - Daysjs
Backend:
1. .Net
   - Serial Port
   - Csv Helper
   - File Manager
3. Entity Franework

## How to Install and Run the Project

1. Clone this repository.
    ```console
    git clone https://github.com/Kanskikuro/UIS-AeroSpace-GUI-Bac-2024.git
    ```
2. Install .Net 8 and Node.js.

3. Refer to [this file](https://github.com/Kanskikuro/UIS-AeroSpace-GUI-Bac-2024/blob/main/Borealis2tsx/borealis2tsx.client/Necessities.md) for instructions on running the application.

Since the application is still in development, you need to run the following commands in the console. This is necessary because npm install only installs dependencies for the client, not the server.

```console
dotnet restore
dotnet run
```

## How to Use the Project

## Credit
Uis Areospace
Erlend Tøssebro
