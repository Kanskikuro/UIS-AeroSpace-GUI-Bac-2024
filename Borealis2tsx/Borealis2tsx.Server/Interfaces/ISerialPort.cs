using System.IO.Ports;
namespace Borealis2tsx.Server.Interfaces
{
    public interface ISerialPort : IDisposable
    {
        void Open();
        void Close();
        string ReadLine();
    }
    //SerialPortWrapper makes SerialPort More Testable
    //because serialport's properties are protected
    public class SerialPortWrapper : ISerialPort
    {
        private readonly SerialPort _serialPort;
        public SerialPortWrapper(string portName, int baudRate, Parity parity,int dataBits, StopBits stopBits){
            
            _serialPort = new SerialPort(
                portName,
                baudRate,
                parity,
                dataBits,
                stopBits
            );
        }
        public void Open() => _serialPort.Open();

        public void Close() => _serialPort.Close();

        public string ReadLine() => _serialPort.ReadLine();

        public void Dispose() => _serialPort.Dispose();
    }
}