namespace Borealis2tsx.Server
{
    /*TODO: this has to be changed once we know what kind of data we need
    from Telemetry group. Until then be careful with how you use this class.
    Use it cleverly in areas where it is not critical, nor time consuming
    to change if we change the class */
    public class ReadDataPort
    {
        public string StartTime { get; set; } = DateTime.Now.ToString().Replace(" ", "T");
        public string Temperature { get; set; } = "0.0";
        public string Pressure { get; set; } = "0.0";
        public string Altitude { get; set; } = "0.0";
        public string AccX { get; set; } = "0.0";
        public string AccY { get; set; } = "0.0";
        public string AccZ { get; set; } = "0.0";
        public string GyroX { get; set; } = "0.0";
        public string GyroY { get; set; } = "0.0";
        public string GyroZ { get; set; } = "0.0";
        public string MagX { get; set; } = "0.0";
        public string MagY { get; set; } = "0.0";
        public string MagZ { get; set; } = "0.0";
        public string Interval { get; set; } = "0s";
    }
}
