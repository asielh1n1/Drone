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
        private UCMedication _UCMedication;
        public MedicationController(IUnitOfWork unitWork, ILogger<MedicationController> logger)
        {
            _logger = logger;
            _UCMedication = new UCMedication(unitWork);
        }
        /// <summary>
        /// Add a new medication
        /// </summary>
        /// <param name="medication"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("add")]
        public ApiResult RegisterMedication(RegisterMedication medication)
        {
            try
            {
                Medication medication1 = _UCMedication.Add(medication.Name, medication.Weight, medication.Code, medication.Image);
                return new ApiResult(Constant.ApiResult.Success, medication1);
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
        /// List of medicines
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public ApiResult List()
        {
            try
            {
                List<Medication> medications = _UCMedication.List();
                return new ApiResult(Constant.ApiResult.Success, medications);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred. {0}. {1}", e.Message, e.StackTrace);
                return new ApiResult(Constant.ApiResult.Error, Constant.FatalError);
            }
        }
    }
}
