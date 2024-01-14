using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Borealis2tsx.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadPortDataController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ReadPortDataController> _logger;

        public ReadPortDataController(ILogger<ReadPortDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetReadPortData")]
        public ReadDataPort Get()
        {
            SerialPort port = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
            port.Handshake = Handshake.None;
            port.Open();
            string dataLine = port.ReadLine();
            string dataLine2 = port.ReadLine();
            port.Close();
            /*
            for(int i = 0; i < 100;  i++){
                Console.WriteLine(port.ReadLine());
            }
            */
            return new ReadDataPort
            {
                Dataline = dataLine2.Split(" ")
            };
            // Temp[graderC] Pressure[mbar] altitude[m] accX[mg] accY[mg] accZ[mg] gyroX[degrees/s] gyroY[degrees/s] gyroZ[degrees/s] magX[µT] magY[µT] magZ[µT]  
        }
    }
}
