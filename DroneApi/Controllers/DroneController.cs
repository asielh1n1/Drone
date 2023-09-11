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
        private DroneUC _DroneUC;
        private MedicationUC _MedicationUC;
        public DroneController(IUnitOfWork unitWork, ILogger<DroneController> logger)
        {
            _logger = logger;
            _DroneUC = new DroneUC(unitWork);
			_MedicationUC = new MedicationUC(unitWork);
        }
        /// <summary>
        /// List models drones
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("drone-models")]
        public ActionResult<ApiResult> DroneModels()
        {
            return ApiResult.Success(typeof(Constants.DroneModel).GetAllPublicConstantValues<string>());
        }

        /// <summary>
        /// Drone status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("drone-states")]
        public ActionResult<ApiResult> DroneStates()
        {
            return ApiResult.Success(typeof(Constants.DroneState).GetAllPublicConstantValues<string>());
        }

        /// <summary>
        /// List of all drones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("drone-list")]
        public ActionResult<ApiResult> DroneList()
        {
            return ApiResult.Success(_DroneUC.ListDrone());
        }

        /// <summary>
        /// Register a new drone
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-drone")]
        public ActionResult<ApiResult> RegisterDrone(RegisterDrone drone)
        {
            try
            {
                Drone drone1 = _DroneUC.Add(drone.SerialNumber, drone.Model, drone.LimitWeight, drone.BatteryCapacity,drone.State);
                return ApiResult.Success(drone1);
            }
            catch (DroneException e)
            {
                return BadRequest(ApiResult.Error(ApiResult.ApiResultStatus.Error, e.Message));
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
                return ApiResult.FatalError();
            }
        }


        /// <summary>
        /// Loading a drone with medication items
        /// </summary>
        /// <param name="loadingDrone"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("loading-drone")]
        public ActionResult<ApiResult> LoadingDrone(LoadingDrone loadingDrone)
        {
            try
            {
                List<Medication> medications = _MedicationUC.List().Where(x=>loadingDrone.medications.Any(y=> y==x.Id)).ToList();
                _DroneUC.LoadDrone(
                    new Drone() { Id = loadingDrone.droneId },
					medications
				);
                return ApiResult.Success();
            }
            catch (DroneException e)
            {
				return BadRequest(ApiResult.Error(ApiResult.ApiResultStatus.Error, e.Message));
			}
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
				return ApiResult.FatalError();
			}
        }

        /// <summary>
        /// Checking loaded medication items for a given drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("checking-loaded-medication")]
        public ActionResult<ApiResult> CheckingLoadedMedication(string droneId)
        {
            try
            {
                List<DroneCore.Entities.Medication> medications = _DroneUC.MedicationLoad( new Drone() { Id = droneId });
                return ApiResult.Success(medications);
            }
            catch (DroneException e)
            {
				return BadRequest(ApiResult.Error(ApiResult.ApiResultStatus.Error, e.Message));
			}
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
				return ApiResult.FatalError();
			}
        }

        /// <summary>
        /// Checking available drones for loading
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("checking-available-drones")]
        public ActionResult<ApiResult> CheckingAvailableDrones()
        {
            try
            {
                List<Drone> drones = _DroneUC.DronesAvailable();
                return ApiResult.Success(drones);
            }
            catch (DroneException e)
            {
				return BadRequest(ApiResult.Error(ApiResult.ApiResultStatus.Error, e.Message));
			}
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
				return ApiResult.FatalError();
			}
        }

        /// <summary>
        /// Check drone battery level for a given drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("check-drone-battery")]
        public ActionResult<ApiResult> CheckDroneBattery(string droneId)
        {
            try
            {
                int batteryLevel = _DroneUC.BatteryLevel(new Drone() { Id = droneId });
                return ApiResult.Success(new { batteryLevel });
            }
            catch (DroneException e)
            {
				return BadRequest(ApiResult.Error(ApiResult.ApiResultStatus.Error, e.Message));
			}
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
				return ApiResult.FatalError();
			}
        }
    }
}
