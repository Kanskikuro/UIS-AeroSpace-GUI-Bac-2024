using System.IO.Ports;
using Borealis2tsx.Server.Interfaces;

namespace Borealis2tsx.Server.Services;
public interface IReadDataPortService
{
    ReadDataPort ReadDataPort(ISerialPort serialPort);
}

public class ReadDataPortService : IReadDataPortService
{
    private readonly ILogger<ReadDataPortService> _logger;

    public ReadDataPortService(ILogger<ReadDataPortService> logger)
    {
        _logger = logger;
    }

    public ReadDataPort ReadDataPort(ISerialPort serialPort)
    {
        using ISerialPort port = serialPort;
        try
        {
            port.Open();
            // First dataLine is maybe read from the middle of the line
            string dataLine = port.ReadLine(); // dataLine is just read and "thrown away"
            string dataLine2 = port.ReadLine();// dataLine2 is the data being sent to UI

            if (string.IsNullOrEmpty(dataLine)||string.IsNullOrEmpty(dataLine2))
            {
                _logger.LogWarning("No data available from the serial port.");
                port.Close();
                return new ReadDataPort();
            }

            port.Close();

            return NormalizeData(dataLine2);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in ReadDataPortService: {e.Message}");
            return new ReadDataPort();
        }
    } 

    public ReadDataPort NormalizeData(string input)
    {
            //Data part
            // string timestampFormat = "yyyy-MM-ddTHH:mm:ss";
            string similarDataLine2 = input.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
            string[] splittedDataArray = similarDataLine2.Split(" ");
            splittedDataArray[11] = splittedDataArray[11].Replace("\r\n", "")
                .Replace("\r", "")
                .Replace("\n", "");

            ReadDataPort readOutput = new ReadDataPort
            {
                Temperature = splittedDataArray[0],
                Pressure = splittedDataArray[1],
                Altitude = splittedDataArray[2],
                AccX = splittedDataArray[3],
                AccY = splittedDataArray[4],
                AccZ = splittedDataArray[5],
                GyroX = splittedDataArray[6],
                GyroY = splittedDataArray[7],
                GyroZ = splittedDataArray[8],
                MagX = splittedDataArray[9],
                MagY = splittedDataArray[10],
                MagZ = splittedDataArray[11]
                    .Replace("\r\n", "")
                    .Replace("\r", "")
                    .Replace("\n", ""),
            };
            _logger.LogInformation("Returning ReadDataPort ReadOutput");
            return readOutput;
    }

}