using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneCore.Entities
{
    public class Delivery
    {
        public Guid Id { get; set; }
        public int DroneId { get; set; }
        public Drone Drone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string State { get; set; }
        public List<DeliveryDetail>DeliveryDetails { get; set; }
    }
}
