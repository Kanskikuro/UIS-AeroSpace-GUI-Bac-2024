using System.IO.Ports;
using Borealis2tsx.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Borealis2tsx.Server.Services;

namespace Borealis2tsx.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadDataPortController : ControllerBase
    {
        private readonly ILogger<ReadDataPortController> _logger;
        private readonly IReadDataPortService _readDataPortService;
        public ReadDataPortController(ILogger<ReadDataPortController> logger, IReadDataPortService readDataPortService)
        {
            _logger = logger;
            _readDataPortService = readDataPortService;
        }
        [HttpGet(Name = "Getreaddataport")]
        public ReadDataPort Get() 
        {
            try
            {
                _logger.LogInformation("Fetching from ReadDataPortService");

                // Call the ReadDataPort method of the ReadDataPortService
                var port = new SerialPortWrapper("COM3", 115200, Parity.None, 8, StopBits.One);
                ReadDataPort readOutput = _readDataPortService.ReadDataPort(port);

                // CSV write to file (You can change file)
                _logger.LogInformation("CSV write to file");
                using (FileStream fs = new FileStream("./output.txt", FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(DateTime.Now+"Borealis2.1");
                    }
                }

                _logger.LogInformation("Returning port data.");
                return  readOutput;
                // Temp[graderC] Pressure[mbar] altitude[m] accX[mg] accY[mg] accZ[mg] gyroX[degrees/s] gyroY[degrees/s] gyroZ[degrees/s] magX[µT] magY[µT] magZ[µT]
            }
            catch (Exception e)
            {
                _logger.LogError($"Error opening serial port: {e.Message}");
                return new ReadDataPort {};
            }
        }
    }
}
