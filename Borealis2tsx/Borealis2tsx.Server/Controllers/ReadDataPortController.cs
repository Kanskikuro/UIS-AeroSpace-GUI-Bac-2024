using System.IO.Ports;
using System.Runtime.InteropServices.JavaScript;
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
        public DataLine Get() 
        {
            try
            {
                _logger.LogInformation("Fetching from ReadDataPortService");

                // Call the ReadDataPort method of the ReadDataPortService
                var port = new SerialPortWrapper("COM3", 115200, Parity.None, 8, StopBits.One);
                DataLine output = _readDataPortService.ReadDataPort(port);

                _logger.LogInformation("Returning port data.");
                return  output;
                // Temp[graderC] Pressure[mbar] altitude[m] accX[mg] accY[mg] accZ[mg] gyroX[degrees/s] gyroY[degrees/s] gyroZ[degrees/s] magX[µT] magY[µT] magZ[µT]
            }
            catch (Exception e)
            {
                _logger.LogError($"Error opening serial port: {e.Message}");
                return new DataLine {};
            }
        }
    }
}
