using DroneApi.Util;
using DroneCore.Entities;
using DroneCore.Interfaces;
using DroneCore.UseCase;
using DroneCore.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DroneApi.Controllers
{
    [Route("api/drone")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        private readonly ILogger _logger;
        private UCDrone _UCDrone;
        public DroneController(IUnitOfWork unitWork, ILogger<DroneController> logger)
        {
            _logger = logger;
            _UCDrone = new UCDrone(unitWork);
        }
        /// <summary>
        /// List models drones
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("drone-models")]
        public ApiResult DroneModels()
        {
            return new ApiResult(Constant.ApiResult.Success,typeof(Constants.DroneModel).GetAllPublicConstantValues<string>());
        }

        /// <summary>
        /// Drone status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("drone-states")]
        public ApiResult DroneStates()
        {
            return new ApiResult(Constant.ApiResult.Success, typeof(Constants.DroneState).GetAllPublicConstantValues<string>());
        }

        /// <summary>
        /// List of all drones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("drone-list")]
        public ApiResult DroneList()
        {
            return new ApiResult(Constant.ApiResult.Success, _UCDrone.ListDrone());
        }

        /// <summary>
        /// Register a new drone
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-drone")]
        public ApiResult RegisterDrone(RegisterDrone drone)
        {
            try
            {
                Drone drone1 = _UCDrone.Add(drone.SerialNumber, drone.Model, drone.LimitWeight, drone.BatteryCapacity,drone.State);
                return new ApiResult(Constant.ApiResult.Success, drone1);
            }
            catch (DroneException e)
            {
                HttpContext.Response.StatusCode = Constant.StatusCode.Error;
                return new ApiResult(Constant.ApiResult.Error, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
                return new ApiResult(Constant.ApiResult.Error, Constant.FatalError);
            }
        }


        /// <summary>
        /// Loading a drone with medication items
        /// </summary>
        /// <param name="loadingDrone"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("loading-drone")]
        public ApiResult LoadingDrone(LoadingDrone loadingDrone)
        {
            try
            {
                _UCDrone.LoadDrone(loadingDrone.droneId, loadingDrone.medications);
                return new ApiResult(Constant.ApiResult.Success, null);
            }
            catch (DroneException e)
            {
                HttpContext.Response.StatusCode = Constant.StatusCode.Error;
                return new ApiResult(Constant.ApiResult.Error, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
                return new ApiResult(Constant.ApiResult.Error, Constant.FatalError);
            }
        }

        /// <summary>
        /// Checking loaded medication items for a given drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("checking-loaded-medication")]
        public ApiResult CheckingLoadedMedication(int droneId)
        {
            try
            {
                List<DroneCore.Entities.Medication> medications = _UCDrone.MedicationLoad(droneId);
                return new ApiResult(Constant.ApiResult.Success, medications);
            }
            catch (DroneException e)
            {
                HttpContext.Response.StatusCode = Constant.StatusCode.Error;
                return new ApiResult(Constant.ApiResult.Error, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
                return new ApiResult(Constant.ApiResult.Error, Constant.FatalError);
            }
        }

        /// <summary>
        /// Checking available drones for loading
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("checking-available-drones")]
        public ApiResult CheckingAvailableDrones()
        {
            try
            {
                List<Drone> drones = _UCDrone.DronesAvailable();
                return new ApiResult(Constant.ApiResult.Success, drones);
            }
            catch (DroneException e)
            {
                HttpContext.Response.StatusCode = Constant.StatusCode.Error;
                return new ApiResult(Constant.ApiResult.Error, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
                return new ApiResult(Constant.ApiResult.Error, Constant.FatalError);
            }
        }

        /// <summary>
        /// Check drone battery level for a given drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("check-drone-battery")]
        public ApiResult CheckDroneBattery(int droneId)
        {
            try
            {
                int batteryLevel = _UCDrone.BatteryLevel(droneId);
                return new ApiResult(Constant.ApiResult.Success, new { batteryLevel= batteryLevel });
            }
            catch (DroneException e)
            {
                HttpContext.Response.StatusCode = Constant.StatusCode.Error;
                return new ApiResult(Constant.ApiResult.Error, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
                return new ApiResult(Constant.ApiResult.Error, Constant.FatalError);
            }
        }
    }
}
