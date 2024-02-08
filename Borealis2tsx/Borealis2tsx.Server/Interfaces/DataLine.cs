using CsvHelper.Configuration.Attributes;

namespace Borealis2tsx.Server.Interfaces
{
    /*TODO: 
    This has to be changed once we know what kind of data we need
    from Telemetry group.
    Until then be careful with how you use this class.
    Use it cleverly in areas where it is not critical, nor time consuming
    to change if we change the class */
    public class DataLine
    {
        // TODO assign Launch ID not generate a new one for each call
        // [Name("LaunchId")]
        // public Guid LaunchId { get; set; } =  Guid.NewGuid();

        //Lets name to be date by default
        //possible feature to pick between Launch, TestLaunch?
        [Name("time")]
        public string Time { get; set; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.ff");
        
        [Name("temperature")]
        public string? Temperature { get; set; }
        
        [Name("pressure")]
        public string? Pressure { get; set; }
        
        [Name("altitude")]
        public string? Altitude { get; set; }
        
        [Name("accX")]
        public string? AccX { get; set; }
        
        [Name("accY")]
        public string? AccY { get; set; }
        
        [Name("accZ")]
        public string? AccZ { get; set; }
        
        [Name("gyroX")]
        public string? GyroX { get; set; }
        
        [Name("gyroY")]
        public string? GyroY { get; set; }
        
        [Name("gyroZ")]
        public string? GyroZ { get; set; }
        
        [Name("magX")]
        public string? MagX { get; set; }
        
        [Name("magY")]
        public string? MagY { get; set; }
        
        [Name("magZ")]
        public string? MagZ { get; set; }
    }
}
