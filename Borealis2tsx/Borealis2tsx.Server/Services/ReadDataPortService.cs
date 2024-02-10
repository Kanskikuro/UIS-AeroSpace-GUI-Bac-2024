using System.IO.Ports;
using Borealis2tsx.Server.Interfaces;

namespace Borealis2tsx.Server.Services;

public interface IReadDataPortService
{
    DataLine ReadDataLine(ISerialPort serialPort);
    string ReadDataLines(ISerialPort serialPort);
}

public class ReadDataPortService : IReadDataPortService
{
    private readonly ILogger<ReadDataPortService> _logger;

    public ReadDataPortService(ILogger<ReadDataPortService> logger)
    {
        _logger = logger;
    }

    public DataLine ReadDataLine(ISerialPort serialPort)
    {
        using ISerialPort port = serialPort;
        try
        {
            port.Open();
            // First dataLine is maybe read from the middle of the line
            string dataLine = port.ReadLine(); // dataLine is just read and "thrown away"
            string dataLine2 = port.ReadLine(); // dataLine2 is the data being sent to UI
            if (string.IsNullOrEmpty(dataLine2))
            {
                _logger.LogWarning("No data available from the serial port.");
                port.Close();
                return new DataLine();
            }
            port.Close();
            return NormalizeData(dataLine2);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in ReadDataPortService: {e.Message}");
            return new DataLine();
        }
    }
    public string ReadDataLines(ISerialPort serialPort)
    {
        using ISerialPort port = serialPort;
        try
        {
            port.Open();
            string dataLines = "";
            int limit = 20;
            for (int i = 0; i < limit; i++)
            {
                // First dataLine is maybe read from the middle of the line
                port.ReadLine(); // dataLine is just read and "thrown away"
                port.ReadLine(); // dataLine is just read and "thrown away"
                string dataLine2 = port.ReadLine();
                if (dataLine2.Split(" ").Length == 12 && !string.IsNullOrEmpty(dataLine2))
                {
                    DataLine normalizedDataline = NormalizeData(dataLine2);
                    string filteredDataline = normalizedDataline.Temperature + ", " +
                                              normalizedDataline.Pressure + ", " +
                                              normalizedDataline.Altitude + ", " +
                                              normalizedDataline.AccX + ", " +
                                              normalizedDataline.AccY + ", " +
                                              normalizedDataline.AccZ + ", " +
                                              normalizedDataline.GyroX + ", " +
                                              normalizedDataline.GyroY + ", " +
                                              normalizedDataline.GyroZ + ", " +
                                              normalizedDataline.MagX + ", " +
                                              normalizedDataline.MagY + ", " +
                                              normalizedDataline.MagZ + ", " +
                                              normalizedDataline.Time;
                    Thread.Sleep(80);
                    dataLines += filteredDataline + (i >= limit-1 ? "" : "\n");
                }
            }
            port.Close();
            return dataLines;
        }
        catch (Exception e)
        {
            string emptyDataline = null + ", " +
                                      null + ", " +
                                      null + ", " +
                                      null + ", " +
                                      null + ", " +
                                      null + ", " +
                                      null + ", " +
                                      null + ", " +
                                      null + ", " +
                                      null + ", " +
                                      null + ", " +
                                      null + ", " +
                                      DateTime.Now.ToString();
            _logger.LogError($"Error in ReadDataPortService: {e.Message}");
            return emptyDataline;
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
        Console.WriteLine(splittedDataArray);


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
}