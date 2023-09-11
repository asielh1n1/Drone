using DroneCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneCore.Entities
{
    public class Delivery
    {
        public string Id { get; set; }
        public Drone Drone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string State { get; set; }
        public List<Medication> Medications { get; set; }

		public Delivery(Drone drone, List<Medication> medications)
		{
			Id = Guid.NewGuid().ToString();
			Drone = drone;
			CreatedDate = DateTime.Now;
			State = Constants.DeliveryState.LOADING;
			Medications = medications;
		}

		public Delivery() { }
	}
}
