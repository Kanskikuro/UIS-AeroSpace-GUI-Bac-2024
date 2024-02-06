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
            if (string.IsNullOrEmpty(dataLine2))
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
            string similarDataLine2 = input.Replace("\r\n", "")
                .Replace("\r", "").Replace("\n", "");
            string[] splittedDataArray = similarDataLine2.Split(" ");
            splittedDataArray[11] = splittedDataArray[11].Replace("\r\n", "")
                .Replace("\r", "")
                .Replace("\n", "");

            ReadDataPort readOutput = new ReadDataPort
            {
                Temperature = decimal.Parse(splittedDataArray[0]).ToString("0"),
                Pressure = decimal.Parse(splittedDataArray[1]).ToString("0"),
                Altitude = decimal.Parse(splittedDataArray[2]).ToString("0"),
                AccX = decimal.Parse(splittedDataArray[3]).ToString("0.00"),
                AccY = decimal.Parse(splittedDataArray[4]).ToString("0.00"),
                AccZ = decimal.Parse(splittedDataArray[5]).ToString("0.00"),
                GyroX = decimal.Parse(splittedDataArray[6]).ToString("0.00"),
                GyroY = decimal.Parse(splittedDataArray[7]).ToString("0.00"),
                GyroZ = decimal.Parse(splittedDataArray[8]).ToString("0.00"),
                MagX = decimal.Parse(splittedDataArray[9]).ToString("0.00"),
                MagY = decimal.Parse(splittedDataArray[10]).ToString("0.00"),
                MagZ = decimal.Parse(
                        splittedDataArray[11]
                            .Replace("\r\n", "")
                            .Replace("\r", "")
                            .Replace("\n", "")
                        ).ToString("0.00")
            };
            _logger.LogInformation("Returning ReadDataPort ReadOutput");
            return readOutput;
    }

}