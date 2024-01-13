using System.IO.Ports;
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
        public IEnumerable<ReadDataPort> Get()
        {
            Console.WriteLine("Serial read init");
            SerialPort port = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
            port.Handshake = Handshake.None;
            port.Open();
            for(int i = 0; i < 100;  i++){
                Console.WriteLine(port.ReadLine());
            }
            string dataLine = port.ReadLine();
            port.Close();
            return Enumerable.Range(1, 5).Select(index => new ReadDataPort
            {
                Dataline = dataLine.Split(" ")
            })
            .ToArray();
            // Temp[graderC] Pressure[mbar] altitude[m] accX[mg] accY[mg] accZ[mg] gyroX[degrees/s] gyroY[degrees/s] gyroZ[degrees/s] magX[µT] magY[µT] magZ[µT]  
        }
    }
}
