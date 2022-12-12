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
    public class UCDrone
    {
        private IUnitOfWork _unitOfWork;

        public UCDrone(IUnitOfWork unitOfWork)
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
        /// <param name="droneId">Drone identifier</param>
        /// <param name="medications">List of medicines</param>
        /// <exception cref="Exception"></exception>
        public void LoadDrone(int droneId, int [] medicationsId)
        {
            Drone drone = _unitOfWork.Drone.FindOne(x => x.Id == droneId);
            if(drone == null)
                throw new DroneException("The drone was not found");
            List<Medication> medications = _unitOfWork.Medication.Find(x => medicationsId.Any(y => y == x.Id)).ToList();
            if(medications.Count == 0 || medications.Count != medicationsId.Length)
                throw new DroneException("The weight of the medicines exceeds the limit allowed for the drone");
            if (medications.Sum(x => x.Weight) > drone.LimitWeight)
                throw new DroneException("The weight of the medicines exceeds the limit allowed for the drone");
            if (drone.BatteryCapacity<25)
                throw new DroneException("Impossible to load the drone with medicines, the battery is less than 25%.");
            var deliveryId = Guid.NewGuid();
            _unitOfWork.Delivery.Add(new Delivery()
            {
                Id = deliveryId,
                CreatedDate = DateTime.Now,
                DroneId = droneId,
                State = Constants.DeliveryState.LOADING,
            });
            foreach (var item in medications)
            {
                _unitOfWork.DeliveryDetail.Add(new DeliveryDetail()
                {
                    DeliveryId = deliveryId,
                    MedicationId = item.Id,
                });
            }
            drone.State = Constants.DeliveryState.LOADING;
            drone.CurrentDelivery = deliveryId;
            _unitOfWork.Save();
        }
        /// <summary>
        /// Checking loaded medication items for a given drone
        /// </summary>
        /// <param name="droneId">Drone identifier</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Medication> MedicationLoad(int droneId)
        {
            Drone drone = _unitOfWork.Drone.FindOne(x => x.Id == droneId);
            if (drone == null)
                throw new DroneException("The drone was not found");
            if (drone.State == Constants.DroneState.IDLE)
                throw new DroneException("The drone is not loaded with medicines");
            List<DeliveryDetail> deliveryDetails = _unitOfWork.DeliveryDetail.Find(x => x.DeliveryId == drone.CurrentDelivery).ToList();
            List<Medication> medications = _unitOfWork.DeliveryDetail.Find(x => x.DeliveryId == drone.CurrentDelivery, "Medication").Select(x => x.Medication).ToList();
            return medications;
        }
        /// <summary>
        /// Checking available drones for loading
        /// </summary>
        /// <returns></returns>
        public List<Drone> DronesAvailable()
        {
            return  _unitOfWork.Drone.Find(x => x.State == Constants.DroneState.IDLE).ToList();
        }
        /// <summary>
        /// Check drone battery level for a given drone;
        /// </summary>
        /// <param name="droneId">Drone identifier</param>
        /// <returns></returns>
        /// <exception cref="DroneException"></exception>
        public int BatteryLevel(int droneId)
        {
            Drone drone = _unitOfWork.Drone.FindOne(x => x.Id == droneId);
            if (drone == null)
                throw new DroneException("The drone was not found");
            return drone.BatteryCapacity;
        }
    }
}
