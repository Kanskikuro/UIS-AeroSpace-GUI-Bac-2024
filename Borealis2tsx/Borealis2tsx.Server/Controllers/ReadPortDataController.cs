using System;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices.JavaScript;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;

namespace Borealis2tsx.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadPortDataController : ControllerBase
    {
        private readonly ILogger<ReadPortDataController> _logger;
        public ReadPortDataController(ILogger<ReadPortDataController> logger)
        {
            _logger = logger;
        }
        [HttpGet(Name = "GetReadPortData")]
        public ReadDataPort Get() 
        /////////////////////Reading data from port///////////////////////////
        {
            _logger.LogInformation("Entering GetReadPortData controller action.");

            //using "using" to ensure that the resources are prorperly released
            using SerialPort port = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
            try
            {
                port.Open();
                _logger.LogInformation("Serial port opened successfully.");
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                _logger.LogError($"Error opening serial port: {e.Message}");
                return new ReadDataPort {};
            }

            // First dataLine is maybe read from the middle of the line
            string dataLine = port.ReadLine(); // dataLine is just read and "thrown away"
            string dataLine2 = port.ReadLine();// dataLine2 is the data being sent to UI
            port.Close();

            ////////////////////end of reading from port///////////////////
            ///
            ///////////////////Sending Data//////////////////////////


            // data sent to csv
            string timestampFormat = "yyyy-MM-ddTHH:mm:ss";
            string similarDataLine2 = DateTime.Now.ToString(timestampFormat) + " " + dataLine2.Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + " 0s";

            // data for json, to React
            string[] splittedDataArray = similarDataLine2.Split(" ");
            splittedDataArray[11] = splittedDataArray[11].Replace("\r\n", "")
                .Replace("\r", "")
                .Replace("\n", "");
            
            ReadDataPort newOutputLine = new ReadDataPort
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
            
            // CSV write to file (You can change file)
            using (FileStream fs = new FileStream("./output.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(similarDataLine2);
                }
            }

            _logger.LogInformation("Returning telemetry data.");
            return newOutputLine;
            // Temp[graderC] Pressure[mbar] altitude[m] accX[mg] accY[mg] accZ[mg] gyroX[degrees/s] gyroY[degrees/s] gyroZ[degrees/s] magX[µT] magY[µT] magZ[µT]

            ///////////////////end of sending data///////////////////////  
        }
    }
}
