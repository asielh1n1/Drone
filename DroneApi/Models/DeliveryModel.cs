using DroneCore.Entities;

namespace DroneApi.Models
{
	public class DeliveryModel:Delivery
	{
		public string DroneModelId { get; set; }
		public List<MedicationModel> Medications { get; set; }
	}
}
