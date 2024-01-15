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
            try
            {
                port.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ReadDataPort
                {
                    Dataline = ["0.0","0.0","0.0","0.0","0.0","0.0","0.0","0.0","0.0","0.0","0.0","0.0","0.0"]
                };
            }
            // First dataLine is maybe read from the middle of the line
            // dataLine is just read and "thrown away"
            // dataLine2 is the data being sent to UI
            string dataLine = port.ReadLine();
            string datetime = DateTime.Now.ToString().Replace(" ", "T");
            string dataLine2 = datetime + " " + port.ReadLine();
            port.Close();
            return new ReadDataPort
            {
                Dataline = dataLine2.Split(" ")
            };
            // Temp[graderC] Pressure[mbar] altitude[m] accX[mg] accY[mg] accZ[mg] gyroX[degrees/s] gyroY[degrees/s] gyroZ[degrees/s] magX[µT] magY[µT] magZ[µT]  
        }
    }
}
