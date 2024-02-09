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
            Time = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.ff"),
            Temperature = decimal.Parse(splittedDataArray[0]).ToString("0")?? null,
            Pressure = decimal.Parse(splittedDataArray[1]).ToString("0")?? null,
            Altitude = decimal.Parse(splittedDataArray[2]).ToString("0")?? null,
            AccX = decimal.Parse(splittedDataArray[3]).ToString("0.00")?? null,
            AccY = decimal.Parse(splittedDataArray[4]).ToString("0.00")?? null,
            AccZ = decimal.Parse(splittedDataArray[5]).ToString("0.00")?? null,
            GyroX = decimal.Parse(splittedDataArray[6]).ToString("0.00")?? null,
            GyroY = decimal.Parse(splittedDataArray[7]).ToString("0.00")?? null,
            GyroZ = decimal.Parse(splittedDataArray[8]).ToString("0.00")?? null,
            MagX = decimal.Parse(splittedDataArray[9]).ToString("0.00") ?? null,
            MagY = decimal.Parse(splittedDataArray[10]).ToString("0.00")?? null,
            MagZ = decimal.Parse(
                splittedDataArray[11]
                    .Replace("\r\n", "")
                    .Replace("\r", "")
                    .Replace("\n", "")
            ).ToString("0.00")?? null
        };
        _logger.LogInformation("Returning ReadDataPort ReadOutput");
        return output;
    }
}