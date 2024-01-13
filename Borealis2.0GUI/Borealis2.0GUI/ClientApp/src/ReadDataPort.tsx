function ReadPortData (){
    const SerialPort = require("serialport")
    const Readline = require("@serialport/parser-readline")
    // Defining the serial port
    const port = new SerialPort("Com3", { baudRate: 115200,});

    const parser = new Readline();
    port.pipe(parser);

    const dataLines = []
    parser.on("data", (line) => {
        console.log(line);
        dataLines.push(line)
    });

    // write function
    // port.write("STARTING READ")
    return (
        <div>
            {
                dataLines[0].split().map((item) => {
                    return <p>{item}</p>
                })
            }
        </div>

    )
}

export default ReadPortData
