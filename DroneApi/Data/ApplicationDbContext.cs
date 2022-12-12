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
        public DbSet<DeliveryDetail> DeliveryDetail { get; set; }

        public static void Seed(ApplicationDbContext db)
        {
            db.Drone.Add(new Drone("BT301", Constants.DroneModel.Middleweight, 450, 75, Constants.DroneState.IDLE));
            db.Drone.Add(new Drone("BT302", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE));
            db.Drone.Add(new Drone("BT303", Constants.DroneModel.Lightweight, 150, 60, Constants.DroneState.LOADING));
            db.Drone.Add(new Drone("BT304", Constants.DroneModel.Lightweight, 150, 100, Constants.DroneState.IDLE));
            db.Drone.Add(new Drone("BT305", Constants.DroneModel.Heavyweight, 500, 80, Constants.DroneState.DELIVERING));
            db.Drone.Add(new Drone("BT306", Constants.DroneModel.Cruiserweight, 450, 85, Constants.DroneState.DELIVERING));
            db.Drone.Add(new Drone("BT307", Constants.DroneModel.Middleweight,450, 40, Constants.DroneState.DELIVERED));
            db.Drone.Add(new Drone("BT308", Constants.DroneModel.Heavyweight, 300, 60, Constants.DroneState.LOADING));
            db.Drone.Add(new Drone("BT309", Constants.DroneModel.Lightweight, 150, 60, Constants.DroneState.LOADING));
            db.Drone.Add(new Drone("BT310", Constants.DroneModel.Lightweight, 150, 20, Constants.DroneState.LOADING));

            db.Medication.Add(new Medication() { Id = 1, Name = "Simvastatina", Code = "sim", Weight = 50 });
            db.Medication.Add(new Medication() { Id = 2, Name = "Aspirina", Code = "asp", Weight = 300 });
            db.Medication.Add(new Medication() { Id = 3, Name = "Omeprazol", Code = "ome", Weight = 100 });
            db.Medication.Add(new Medication() { Id = 4, Name = "Lexotiroxina sódica", Code = "lex_sod", Weight = 150 });
            db.Medication.Add(new Medication() { Id = 5, Name = "Ramipril", Code = "ram", Weight = 200 });
            db.Medication.Add(new Medication() { Id = 6, Name = "Amlodipina", Code = "aml", Weight = 200 });
            db.Medication.Add(new Medication() { Id = 7, Name = "Paracetamol", Code = "Par", Weight = 200 });
            db.SaveChanges();
        }

    }
}
