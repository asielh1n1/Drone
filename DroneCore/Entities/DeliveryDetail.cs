using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneCore.Entities
{
    public class DeliveryDetail
    {
        public int Id { get; set; }
        public Guid DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
    }
}
