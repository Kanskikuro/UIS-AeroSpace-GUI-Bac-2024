using System.IO.Ports;
using Borealis2tsx.Server.Interfaces;

namespace Borealis2tsx.Server.Services;

// PortHandler is responsible for writing and reading from COM port
// PortHandler can receive data from COM port, process and send data to frontend or db
// PortHandler should be able to write to COM port
public class PortHandlerBackgroundService : BackgroundService
{
    private readonly ILogger<PortHandlerBackgroundService> _logger;
    private readonly DateTime _startingTime = DateTime.Now; // time since last refresh
    public PortHandlerBackgroundService(ILogger<PortHandlerBackgroundService> logger)
    {
        _logger = logger;
    }

    public DataLine ReadDataLine(ISerialPort serialPort)
    {
        using ISerialPort port = serialPort;
        try
        {
            port.Open(); // open COM port
            // First dataLine is maybe read from the middle of the line
            string dataLine = port.ReadLine(); // dataLine is just read and "thrown away"
            string dataLine2 = port.ReadLine(); // dataLine2 is the data being sent to UI
            if (string.IsNullOrEmpty(dataLine2))
            {
                _logger.LogWarning("No data available from the serial port.");
                port.Close();
                return new DataLine{}; // returning and empty DataLine 
            }
            port.Close(); // closing COM port so other Datahandlers can use it
            return NormalizeData(dataLine2);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in ReadDataPortService: {e.Message}");
            return new DataLine();
        }
    }
    public DataLine NormalizeData(string input)
    {
        string similarDataLine2 = input.Replace("\r\n", "")
            .Replace("\r", "").Replace("\n", "");
        string[] splittedDataArray = similarDataLine2.Split(" ");
        splittedDataArray[11] = splittedDataArray[11].Replace("\r\n", "")
            .Replace("\r", "")
            .Replace("\n", "");

        DataLine output = new DataLine
        {
            //Function / loop?
            Time = DateTimeOffset.Now,
            Temperature = double.Parse(splittedDataArray[0]),
            Pressure = string.IsNullOrWhiteSpace(splittedDataArray[1]) ? null : double.Parse(splittedDataArray[1]) ,
            Altitude = string.IsNullOrWhiteSpace(splittedDataArray[2])?null:double.Parse(splittedDataArray[1]),
            AccX = string.IsNullOrWhiteSpace(splittedDataArray[3])?null:double.Parse(splittedDataArray[1]),
            AccY = string.IsNullOrWhiteSpace(splittedDataArray[4])?null:double.Parse(splittedDataArray[1]),
            AccZ = string.IsNullOrWhiteSpace(splittedDataArray[5])?null:double.Parse(splittedDataArray[1]),
            GyroX = string.IsNullOrWhiteSpace(splittedDataArray[6])?null:double.Parse(splittedDataArray[1]),
            GyroY = string.IsNullOrWhiteSpace(splittedDataArray[7])?null:double.Parse(splittedDataArray[1]),
            GyroZ = string.IsNullOrWhiteSpace(splittedDataArray[8])?null:double.Parse(splittedDataArray[1]),
            MagX = string.IsNullOrWhiteSpace(splittedDataArray[9])?null:double.Parse(splittedDataArray[1]),
            MagY = string.IsNullOrWhiteSpace(splittedDataArray[10])?null:double.Parse(splittedDataArray[1]),
            MagZ = string.IsNullOrWhiteSpace(splittedDataArray[11].Replace("\r\n", "")
                    .Replace("\r", "")
                    .Replace("\n", ""))
                    ?null:double.Parse(splittedDataArray[1])
        };
        _logger.LogInformation("Returning ReadDataPort ReadOutput");
        return output;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var port = new SerialPortWrapper("COM3", 115200, Parity.None, 8, StopBits.One);
            _logger.LogInformation("Reading from dataport and writing to csv");

            DataLine DataToStoredCSV = ReadDataLine(port);
            string filteredDataline = DataToStoredCSV.Temperature + ", " +
                                      DataToStoredCSV.Pressure + ", " +
                                      DataToStoredCSV.Altitude + ", " +
                                      DataToStoredCSV.AccX + ", " +
                                      DataToStoredCSV.AccY + ", " +
                                      DataToStoredCSV.AccZ + ", " +
                                      DataToStoredCSV.GyroX + ", " +
                                      DataToStoredCSV.GyroY + ", " +
                                      DataToStoredCSV.GyroZ + ", " +
                                      DataToStoredCSV.MagX + ", " +
                                      DataToStoredCSV.MagY + ", " +
                                      DataToStoredCSV.MagZ + ", " +
                                      DataToStoredCSV.Time;
            using (FileStream fs = new FileStream("./output.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(filteredDataline);
                }
            }

            Console.WriteLine(filteredDataline);
            // CSV write to file (You can change file)
            _logger.LogInformation("CSV write to file");

            await Task.Delay(80, stoppingToken);
        }
    }
}