using System;
using System.IO.Ports;
using System.Runtime.InteropServices.JavaScript;
using System.Threading;
using System.Threading.Tasks;
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
        {
            SerialPort port = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
            try
            {
                port.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ReadDataPort {};
            }
            // First dataLine is maybe read from the middle of the line
            // dataLine is just read and "thrown away"
            // dataLine2 is the data being sent to UI
            string dataLine = port.ReadLine();
            string dataLine2 = port.ReadLine();
            port.Close();
            string[] splittedDataArray = dataLine2.Split(" ");
            return new ReadDataPort
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
                MagZ = splittedDataArray[11],
            };
            // Temp[graderC] Pressure[mbar] altitude[m] accX[mg] accY[mg] accZ[mg] gyroX[degrees/s] gyroY[degrees/s] gyroZ[degrees/s] magX[µT] magY[µT] magZ[µT]  
        }
    }
}
