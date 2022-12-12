namespace DroneApi.Util
{
    public class LoadingDrone
    {
        public int droneId { get; set; }
        public int [] medications { get; set; }
    }

    public class RegisterDrone
    {
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public int LimitWeight { get; set; }
        public int BatteryCapacity { get; set; }
        public string State { get; set; }
    }

    public class RegisterMedication
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public string Code { get; set; }
        public string? Image { get; set; }
    }
}
