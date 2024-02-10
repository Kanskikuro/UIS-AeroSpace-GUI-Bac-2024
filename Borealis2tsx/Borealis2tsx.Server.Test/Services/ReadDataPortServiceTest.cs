using System.IO.Ports;
using Borealis2tsx.Server.Services;
using Borealis2tsx.Server.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Borealis2tsx.Server.Tests.Services
{
    //Quick Moq templates from ChatGPT
    //Adjustments to make it work
    public class ReadDataPortServiceTests
    {
        [Fact]
        public void ReadDataPort_SerialPortReturnsData_InCorrectOrder()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ReadDataPortService>>();
            var service = new ReadDataPortService(mockLogger.Object);
            string testInput = " 1.1 2.2 3.3 11.1 22.2 33.3\r\n";
            string testInput2 = "25.5 1013.25 150.0 0.1 0.2 0.3 1.1 2.2 3.3 11.1 22.2 33.3\r\n";

            // Mock the SerialPort behavior
            var mockSerialPort = new Mock<ISerialPort>();
            mockSerialPort.SetupSequence(p => p.ReadLine())
                .Returns(testInput)
                .Returns(testInput2);

            // Act
            var result = service.ReadDataLine(mockSerialPort.Object);

            // Assert
            // Adjust the expected result based on your service logic
            // Temp[graderC] Pressure[mbar] altitude[m] accX[mg] accY[mg] accZ[mg] 
            // gyroX[degrees/s] gyroY[degrees/s] gyroZ[degrees/s] magX[µT] magY[µT] magZ[µT]
            Assert.NotNull(result);
            Assert.Equal(25.5, result.Temperature);
            Assert.Equal(1013.25, result.Pressure);
            Assert.Equal(150.0, result.Altitude);
            Assert.Equal(0.1, result.AccX);
            Assert.Equal(0.2, result.AccY);
            Assert.Equal(0.3, result.AccZ);
            Assert.Equal(1.1, result.GyroX);
            Assert.Equal(2.2, result.GyroY);
            Assert.Equal(3.3, result.GyroZ);
            Assert.Equal(11.1, result.MagX);
            Assert.Equal(22.2, result.MagY);
            Assert.Equal(33.3, result.MagZ);
        }

        [Fact]
        public void ReadDataPort_IfSerialPortCloseOpens()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ReadDataPortService>>();
            var service = new ReadDataPortService(mockLogger.Object);

            // Mock the SerialPort to throw an exception
            var mockSerialPort = new Mock<ISerialPort>();

            // Act
            var result = service.ReadDataLine(mockSerialPort.Object);

            // Verify that Open is called once
            mockSerialPort.Verify(p => p.Open(), Times.Once);

            // Verify that Close is called once
            mockSerialPort.Verify(p => p.Close(), Times.Once);
        }
    }
}