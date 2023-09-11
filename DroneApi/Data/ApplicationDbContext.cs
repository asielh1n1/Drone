using DroneApi.Models;
using DroneCore.Entities;
using DroneCore.Util;
using Microsoft.EntityFrameworkCore;

namespace DroneApi.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("DroneDatabase");
        }

		public DbSet<Drone> Drone { get; set; }
		public DbSet<Medication> Medication { get; set; }
		public DbSet<Delivery> Delivery { get; set; }
		public DbSet<DeliveryDetailModel> DeliveryDetail { get; set; }

		public static void Seed(ApplicationDbContext db)
        {
            db.Drone.Add(new DroneModel() { Id = Guid.NewGuid().ToString(), SerialNumber = "BT301", Model = Constants.DroneModel.Middleweight, LimitWeight = 450, BatteryCapacity = 75, State =Constants.DroneState.IDLE });
            db.Drone.Add(new DroneModel() { Id = Guid.NewGuid().ToString(), SerialNumber = "BT302", Model = Constants.DroneModel.Middleweight, LimitWeight = 150, BatteryCapacity = 60, State =Constants.DroneState.IDLE });
            db.Drone.Add(new DroneModel() { Id = Guid.NewGuid().ToString(), SerialNumber = "BT303", Model = Constants.DroneModel.Middleweight, LimitWeight = 150, BatteryCapacity = 40, State =Constants.DroneState.IDLE });
            db.Drone.Add(new DroneModel() { Id = Guid.NewGuid().ToString(), SerialNumber = "BT304", Model = Constants.DroneModel.Middleweight, LimitWeight = 500, BatteryCapacity = 30, State =Constants.DroneState.IDLE });
            db.Drone.Add(new DroneModel() { Id = Guid.NewGuid().ToString(), SerialNumber = "BT305", Model = Constants.DroneModel.Middleweight, LimitWeight = 450, BatteryCapacity = 80, State =Constants.DroneState.IDLE });
            db.Drone.Add(new DroneModel() { Id = Guid.NewGuid().ToString(), SerialNumber = "BT306", Model = Constants.DroneModel.Middleweight, LimitWeight = 250, BatteryCapacity = 95, State =Constants.DroneState.IDLE });
            db.Drone.Add(new DroneModel() { Id = Guid.NewGuid().ToString(), SerialNumber = "BT307", Model = Constants.DroneModel.Middleweight, LimitWeight = 300, BatteryCapacity = 80, State =Constants.DroneState.IDLE });
            db.Drone.Add(new DroneModel() { Id = Guid.NewGuid().ToString(), SerialNumber = "BT308", Model = Constants.DroneModel.Middleweight, LimitWeight = 400, BatteryCapacity = 65, State =Constants.DroneState.IDLE });
            db.Drone.Add(new DroneModel() { Id = Guid.NewGuid().ToString(), SerialNumber = "BT309", Model = Constants.DroneModel.Middleweight, LimitWeight = 400, BatteryCapacity = 50, State =Constants.DroneState.IDLE });
            db.Drone.Add(new DroneModel() { Id = Guid.NewGuid().ToString(), SerialNumber = "BT310", Model = Constants.DroneModel.Middleweight, LimitWeight = 200, BatteryCapacity = 55, State =Constants.DroneState.IDLE });
            
            db.Medication.Add(new MedicationModel() { Id = Guid.NewGuid().ToString(), Name = "Simvastatina", Code = "sim", Weight = 50 });
            db.Medication.Add(new MedicationModel() { Id = Guid.NewGuid().ToString(), Name = "Aspirina", Code = "asp", Weight = 300 });
            db.Medication.Add(new MedicationModel() { Id = Guid.NewGuid().ToString(), Name = "Omeprazol", Code = "ome", Weight = 100 });
            db.Medication.Add(new MedicationModel() { Id = Guid.NewGuid().ToString(), Name = "Lexotiroxina sódica", Code = "lex_sod", Weight = 150 });
            db.Medication.Add(new MedicationModel() { Id = Guid.NewGuid().ToString(), Name = "Ramipril", Code = "ram", Weight = 200 });
            db.Medication.Add(new MedicationModel() { Id = Guid.NewGuid().ToString(), Name = "Amlodipina", Code = "aml", Weight = 200 });
            db.Medication.Add(new MedicationModel() { Id = Guid.NewGuid().ToString(), Name = "Paracetamol", Code = "Par", Weight = 200 });
            db.SaveChanges();
        }

    }
}
