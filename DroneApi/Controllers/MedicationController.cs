using DroneApi.Util;
using DroneCore.Entities;
using DroneCore.Interfaces;
using DroneCore.UseCase;
using DroneCore.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DroneApi.Controllers
{
    [Route("api/medication")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private readonly ILogger _logger;
        private MedicationUC _MedicationUC;
        public MedicationController(IUnitOfWork unitWork, ILogger<MedicationController> logger)
        {
            _logger = logger;
            _MedicationUC = new MedicationUC(unitWork);
        }
        /// <summary>
        /// Add a new medication
        /// </summary>
        /// <param name="medication"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("add")]
        public ActionResult<ApiResult> RegisterMedication(RegisterMedication medication)
        {
            try
            {
                Medication medication1 = _MedicationUC.Add(medication.Name, medication.Weight, medication.Code, medication.Image);
                return ApiResult.Success(medication1);
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
        /// List of medicines
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public ApiResult List()
        {
            try
            {
                List<Medication> medications = _MedicationUC.List();
                return ApiResult.Success(medications);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
				return ApiResult.FatalError();
			}
        }
    }
}
