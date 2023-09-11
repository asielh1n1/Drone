namespace DroneTest.Models
{
	public class DeliveryDetailModel
	{
		public string Id { get; set; }
		public string DeliveryModelId { get;set; }
		public DeliveryModel DeliveryModel { get; set; }
		public string MedicationModelId { get;set; }
		public MedicationModel MedicationModel { get; set; }
	}
}
