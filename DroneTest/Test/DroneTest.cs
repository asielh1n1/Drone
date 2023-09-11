using DroneTest.Infrastructure;
using DroneCore.UseCase;
using DroneCore.Entities;
using DroneCore.Util;

namespace DroneTest.Test
{
    public class DroneTest
    {
        [Fact]
        public void RegisterDrone()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                DroneUC DroneUC = new DroneUC(_unitOfWork);
                Drone drone = DroneUC.Add("BT308", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
                Assert.NotNull(drone);
            }
        }

        [Theory]
        // Inavlid serial number
        [InlineData("", Constants.DroneModel.Heavyweight, 100, 75, Constants.DroneState.IDLE)]
        [InlineData(@"qwertyjkdfjglkdfjgfgdfgfglkfjglkdfjgdfklgjfggfgfjggdfgkfjgfgkjhfgkjdhfgjkh;gjghgifdgidfgjdfklgjfghgjdhsjdhfdfh", 
            Constants.DroneModel.Heavyweight, 100, 75, Constants.DroneState.IDLE)]
        //Invalid models
        [InlineData("BT308", "Ford", 100, 75, Constants.DroneState.IDLE)]
        //Invalid limit weight
        [InlineData("BT308", "Ford", 501, 75, Constants.DroneState.IDLE)]
        // Invalid battery capacity
        [InlineData("BT308", "Ford", 200, 105, Constants.DroneState.IDLE)]
        public void RegisterInvalidDrone(string serialNumber, string model, int limitWeight, int batteryCapacity, string state)
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                DroneUC DroneUC = new DroneUC(_unitOfWork);
                Assert.Throws<DroneException>(() => DroneUC.Add(serialNumber, model, limitWeight, batteryCapacity, state));
            }

        }

        [Fact]
        public void LoadDrone()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                DroneUC DroneUC = new DroneUC(_unitOfWork);
                MedicationUC MedicationUC = new MedicationUC(_unitOfWork);
                List<Medication> medications = new List<Medication>()
                {
                    MedicationUC.Add("Simvastatina",100,"sim",""),
                    MedicationUC.Add("Aspirina",100,"asp",""),
                    MedicationUC.Add("Omeprazol",100,"ome",""),
                };
                Drone drone = DroneUC.Add("BT308", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
                try
                {
                    DroneUC.LoadDrone(new Drone() { Id = drone.Id }, medications);
                    Assert.True(true);
                }
                catch (Exception)
                {
                    Assert.True(false);
                }
                
                
            }
        }


        [Fact]
        public void LoadFaildDrone()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                DroneUC DroneUC = new DroneUC(_unitOfWork);
                MedicationUC MedicationUC = new MedicationUC(_unitOfWork);
                List<Medication> medications = new List<Medication>()
                {
                    MedicationUC.Add("Simvastatina",100,"sim",""),
                    MedicationUC.Add("Aspirina",100,"asp",""),
                    MedicationUC.Add("Omeprazol",200,"ome",""),
                };
                Drone drone = DroneUC.Add("BT308", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
                Assert.Throws<DroneException>(() => DroneUC.LoadDrone(drone, medications));
            }
        }

        [Fact]
        public void MedicationLoad()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                DroneUC DroneUC = new DroneUC(_unitOfWork);
                MedicationUC MedicationUC = new MedicationUC(_unitOfWork);
                List<Medication> medications = new List<Medication>()
                {
                    MedicationUC.Add("Simvastatina",100,"sim",""),
                    MedicationUC.Add("Aspirina",100,"asp",""),
                    MedicationUC.Add("Omeprazol",100,"ome",""),
                };
                Drone drone = DroneUC.Add("BT308", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
                DroneUC.LoadDrone(drone, medications);
                List<Medication> medications2 = DroneUC.MedicationLoad(drone);
                Assert.Equal(3, medications2.Count);
            }
        }

        [Fact]
        public void DronesAvailable()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                DroneUC DroneUC = new DroneUC(_unitOfWork);
                DroneUC.Add("BT306", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
                DroneUC.Add("BT307", Constants.DroneModel.Heavyweight, 300, 75, Constants.DroneState.LOADED);
                DroneUC.Add("BT308", Constants.DroneModel.Lightweight, 300, 75, Constants.DroneState.IDLE);
                List<Drone> availableDrones = DroneUC.DronesAvailable();
                Assert.Equal(2, availableDrones.Count);
            }
        }

        [Fact]
        public void BatteryLevel()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                DroneUC DroneUC = new DroneUC(_unitOfWork);
                Drone drone = DroneUC.Add("BT306", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
                int levelBattery = DroneUC.BatteryLevel(new Drone() { Id = drone.Id });
                Assert.Equal(75, levelBattery);
            }
        }
    }

    
}