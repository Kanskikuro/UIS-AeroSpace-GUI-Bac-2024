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
            Time = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.ff"),
            Temperature = decimal.Parse(splittedDataArray[0]).ToString("0") ?? null,
            Pressure = decimal.Parse(splittedDataArray[1]).ToString("0") ?? null,
            Altitude = decimal.Parse(splittedDataArray[2]).ToString("0") ?? null,
            AccX = decimal.Parse(splittedDataArray[3]).ToString("0.00") ?? null,
            AccY = decimal.Parse(splittedDataArray[4]).ToString("0.00") ?? null,
            AccZ = decimal.Parse(splittedDataArray[5]).ToString("0.00") ?? null,
            GyroX = decimal.Parse(splittedDataArray[6]).ToString("0.00") ?? null,
            GyroY = decimal.Parse(splittedDataArray[7]).ToString("0.00") ?? null,
            GyroZ = decimal.Parse(splittedDataArray[8]).ToString("0.00") ?? null,
            MagX = decimal.Parse(splittedDataArray[9]).ToString("0.00") ?? null,
            MagY = decimal.Parse(splittedDataArray[10]).ToString("0.00") ?? null,
            MagZ = decimal.Parse(
                splittedDataArray[11]
                    .Replace("\r\n", "")
                    .Replace("\r", "")
                    .Replace("\n", "")
            ).ToString("0.00") ?? null
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