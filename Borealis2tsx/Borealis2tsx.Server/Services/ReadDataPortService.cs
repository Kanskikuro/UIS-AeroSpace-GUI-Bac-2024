using System;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices.JavaScript;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Borealis2tsx.Server.Interface;
namespace Borealis2tsx.Server.Services;
public interface IReadDataPortService
{
    ReadDataPort ReadDataPort();
}

public class ReadDataPortService : IReadDataPortService
{
    private readonly ILogger<ReadDataPortService> _logger;

    public ReadDataPortService(ILogger<ReadDataPortService> logger)
    {
        _logger = logger;
    }

    public ReadDataPort ReadDataPort()
    {
        using (SerialPort port = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One))
        {
            try
            {
                port.Open();
                _logger.LogInformation("Serial port opened");

                
                // First dataLine is maybe read from the middle of the line
                string dataLine = port.ReadLine(); // dataLine is just read and "thrown away"
                string dataLine2 = port.ReadLine();// dataLine2 is the data being sent to UI
                port.Close();
                _logger.LogInformation("Serial port closed");

                //Data part
                string timestampFormat = "yyyy-MM-ddTHH:mm:ss";
                string similarDataLine2 = DateTime.Now.ToString(timestampFormat) + " " + dataLine2.Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + " 0s";
                string[] splittedDataArray = similarDataLine2.Split(" ");
                splittedDataArray[11] = splittedDataArray[11].Replace("\r\n", "")
                    .Replace("\r", "")
                    .Replace("\n", "");
                
                ReadDataPort ReadOutput = new ReadDataPort
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
                return ReadOutput;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error reading telemetry data: {e.Message}");
                return new ReadDataPort {};
            }
        }
    }
}