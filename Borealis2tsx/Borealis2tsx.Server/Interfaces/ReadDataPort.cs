using CsvHelper.Configuration.Attributes;

namespace Borealis2tsx.Server.Interfaces
{
    /*TODO: 
    This has to be changed once we know what kind of data we need
    from Telemetry group.
    Until then be careful with how you use this class.
    Use it cleverly in areas where it is not critical, nor time consuming
    to change if we change the class */
    public class ReadDataPort
    {
        // TODO assign Launch ID not generate a new one for each call
        // [Name("LaunchId")]
        // public Guid LaunchId { get; set; } =  Guid.NewGuid();

        //Lets name to be date by default
        //possible feature to pick between Launch, TestLaunch?
        [Name("LaunchName")]
        public string LaunchName { get; set; } = DateTime.Now.ToString();

        [Name("startTime")]
        public string StartTime { get; set; } = DateTime.Now.ToString();
        
        [Name("temperature")]
        public string Temperature { get; set; } = "0.0";
        
        [Name("pressure")]
        public string Pressure { get; set; } = "0.0";
        
        [Name("altitude")]
        public string Altitude { get; set; } = "0.0";
        
        [Name("accX")]
        public string AccX { get; set; } = "0.0";
        
        [Name("accY")]
        public string AccY { get; set; } = "0.0";
        
        [Name("accZ")]
        public string AccZ { get; set; } = "0.0";
        
        [Name("gyroX")]
        public string GyroX { get; set; } = "0.0";
        
        [Name("gyroY")]
        public string GyroY { get; set; } = "0.0";
        
        [Name("gyroZ")]
        public string GyroZ { get; set; } = "0.0";
        
        [Name("magX")]
        public string MagX { get; set; } = "0.0";
        
        [Name("magY")]
        public string MagY { get; set; } = "0.0";
        
        [Name("magZ")]
        public string MagZ { get; set; } = "0.0";
        
        [Name("interval")]
        public string Interval { get; set; } = "0s";
    }
}
