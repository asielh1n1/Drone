using DroneCore.Entities;
using DroneCore.Interfaces;
using DroneCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneCore.UseCase
{
    public class DroneUC
    {
        private IUnitOfWork _unitOfWork;

        public DroneUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public List<Drone> ListDrone()
        {
            return _unitOfWork.Drone.Find().ToList();
        }
        /// <summary>
        /// Register a new drone
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="model">Lightweight, Middleweight, Cruiserweight, Heavyweight</param>
        /// <param name="limitWeight">500gr max</param>
        /// <param name="batteryCapacity">percentage</param>
        /// <param name="state">IDLE, LOADING, LOADED, DELIVERING, DELIVERED, RETURNING</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Drone Add(string serialNumber, string model, int limitWeight, int batteryCapacity, string state)
        {
            if (string.IsNullOrEmpty(serialNumber))
                throw new DroneException("The serial number field cannot be empty or null.");
            if(serialNumber.Count() > 100)
                throw new DroneException("The maximum number of characters allowed is 100.");
            if (limitWeight>500)
                throw new DroneException("The maximum amount of weight allowed is 500 g.");
            if (batteryCapacity < 0 || batteryCapacity>100)
                throw new DroneException("The battery percentage must be between 0 and 100.");
            var models = typeof(Constants.DroneModel).GetAllPublicConstantValues<string>();
            if(!models.Any(x=>x==model))
                throw new DroneException("The model is invalid");
            Drone drone = new Drone(serialNumber, model, limitWeight, batteryCapacity, state);
            _unitOfWork.Drone.Add(drone);
            _unitOfWork.Save();
            return drone;
        }
		/// <summary>
		/// Loading a drone with medication items
		/// </summary>
		/// <param name="drone"></param>
		/// <param name="medications"></param>
		/// <exception cref="DroneException"></exception>
		public void LoadDrone(Drone drone, List<Medication>medications)
        {
            Drone droneModel = _unitOfWork.Drone.FindOne(drone.Id);
            if(drone == null)
                throw new DroneException("The drone was not found");
            if (medications.Sum(x => x.Weight) > droneModel.LimitWeight)
                throw new DroneException("The weight of the medicines exceeds the limit allowed for the drone");
            if (droneModel.BatteryCapacity<25)
                throw new DroneException("Impossible to load the drone with medicines, the battery is less than 25%.");
            var delivery = new Delivery(droneModel, medications);
            _unitOfWork.Delivery.Add(delivery);
			droneModel.State = Constants.DeliveryState.LOADING;
			// You specified the identifier of the current delivery
			droneModel.CurrentDelivery = delivery.Id;
            _unitOfWork.Drone.Update(droneModel);
            _unitOfWork.Save();
        }
        /// <summary>
        /// Checking loaded medication items for a given drone
        /// </summary>
        /// <param name="droneId">Drone identifier</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Medication> MedicationLoad(Drone drone)
        {
            Drone droneModel = _unitOfWork.Drone.FindOne(drone.Id);
            if (droneModel == null)
                throw new DroneException("The drone was not found");
            if (droneModel.State == Constants.DroneState.IDLE)
                throw new DroneException("The drone is not loaded with medicines");
			Delivery delivery = _unitOfWork.Delivery.FindOne(droneModel.CurrentDelivery);
            return delivery.Medications;
        }
        /// <summary>
        /// Checking available drones for loading
        /// </summary>
        /// <returns></returns>
        public List<Drone> DronesAvailable()
        {
            return  _unitOfWork.Drone.Find(Constants.DroneState.IDLE);
        }
        /// <summary>
        /// Check drone battery level for a given drone;
        /// </summary>
        /// <param name="droneId">Drone identifier</param>
        /// <returns></returns>
        /// <exception cref="DroneException"></exception>
        public int BatteryLevel(Drone drone)
        {
            Drone droneModel = _unitOfWork.Drone.FindOne(drone.Id);
            if (droneModel == null)
                throw new DroneException("The drone was not found");
            return droneModel.BatteryCapacity;
        }
    }
}
