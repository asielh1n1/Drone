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
                UCDrone UCDrone = new UCDrone(_unitOfWork);
                Drone drone = UCDrone.Add("BT308", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
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
                UCDrone UCDrone = new UCDrone(_unitOfWork);
                Assert.Throws<DroneException>(() => UCDrone.Add(serialNumber, model, limitWeight, batteryCapacity, state));
            }

        }

        [Fact]
        public void LoadDrone()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCDrone UCDrone = new UCDrone(_unitOfWork);
                UCMedication UCMedication = new UCMedication(_unitOfWork);
                List<Medication> medications = new List<Medication>()
                {
                    UCMedication.Add("Simvastatina",100,"sim",""),
                    UCMedication.Add("Aspirina",100,"asp",""),
                    UCMedication.Add("Omeprazol",100,"ome",""),
                };
                Drone drone = UCDrone.Add("BT308", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
                try
                {
                    UCDrone.LoadDrone(drone.Id, medications.Select(x=>x.Id).ToArray());
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
                UCDrone UCDrone = new UCDrone(_unitOfWork);
                UCMedication UCMedication = new UCMedication(_unitOfWork);
                List<Medication> medications = new List<Medication>()
                {
                    UCMedication.Add("Simvastatina",100,"sim",""),
                    UCMedication.Add("Aspirina",100,"asp",""),
                    UCMedication.Add("Omeprazol",200,"ome",""),
                };
                Drone drone = UCDrone.Add("BT308", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
                Assert.Throws<DroneException>(() => UCDrone.LoadDrone(drone.Id, medications.Select(x => x.Id).ToArray()));
            }
        }

        [Fact]
        public void MedicationLoad()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCDrone UCDrone = new UCDrone(_unitOfWork);
                UCMedication UCMedication = new UCMedication(_unitOfWork);
                List<Medication> medications = new List<Medication>()
                {
                    UCMedication.Add("Simvastatina",100,"sim",""),
                    UCMedication.Add("Aspirina",100,"asp",""),
                    UCMedication.Add("Omeprazol",100,"ome",""),
                };
                Drone drone = UCDrone.Add("BT308", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
                UCDrone.LoadDrone(drone.Id, medications.Select(x => x.Id).ToArray());
                List<Medication> medications2 = UCDrone.MedicationLoad(drone.Id);
                Assert.Equal(3, medications2.Count);
            }
        }

        [Fact]
        public void DronesAvailable()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCDrone UCDrone = new UCDrone(_unitOfWork);
                UCDrone.Add("BT306", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
                UCDrone.Add("BT307", Constants.DroneModel.Heavyweight, 300, 75, Constants.DroneState.LOADED);
                UCDrone.Add("BT308", Constants.DroneModel.Lightweight, 300, 75, Constants.DroneState.IDLE);
                List<Drone> availableDrones = UCDrone.DronesAvailable();
                Assert.Equal(2, availableDrones.Count);
            }
        }

        [Fact]
        public void BatteryLevel()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCDrone UCDrone = new UCDrone(_unitOfWork);
                Drone drone = UCDrone.Add("BT306", Constants.DroneModel.Middleweight, 300, 75, Constants.DroneState.IDLE);
                int levelBattery = UCDrone.BatteryLevel(drone.Id);
                Assert.Equal(75, levelBattery);
            }
        }
    }

    
}