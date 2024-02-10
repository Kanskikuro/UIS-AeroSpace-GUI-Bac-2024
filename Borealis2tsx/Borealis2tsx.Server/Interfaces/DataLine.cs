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
        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;
        
        [Name("temperature")]
        public double ? Temperature { get; set; }
        
        [Name("pressure")]
        public double? Pressure { get; set; }
        
        [Name("altitude")]
        public double? Altitude { get; set; }
        
        [Name("accX")]
        public double? AccX { get; set; }
        
        [Name("accY")]
        public double? AccY { get; set; }
        
        [Name("accZ")]
        public double? AccZ { get; set; }
        
        [Name("gyroX")]
        public double? GyroX { get; set; }
        
        [Name("gyroY")]
        public double? GyroY { get; set; }
        
        [Name("gyroZ")]
        public double? GyroZ { get; set; }
        
        [Name("magX")]
        public double? MagX { get; set; }
        
        [Name("magY")]
        public double? MagY { get; set; }
        
        [Name("magZ")]
        public double? MagZ { get; set; }
    }
}
