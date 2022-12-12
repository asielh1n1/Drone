using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneCore.Entities
{
    public class Drone
    {
        public Drone(string serialNumber, string model, int limitWeight, int batteryCapacity, string state)
        {
            SerialNumber = serialNumber;
            Model = model;
            LimitWeight = limitWeight;
            BatteryCapacity = batteryCapacity;
            State = state;
        }

        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public int LimitWeight { get; set; }
        public int BatteryCapacity { get; set; }
        public string State { get; set; }
        public Guid? CurrentDelivery { get; set; }
    }
}
