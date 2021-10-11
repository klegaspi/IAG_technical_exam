using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using VehicleSummary.Api.Services.VehicleSummary;

namespace VehicleSummary.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class VehicleChecksController : ControllerBase
    {
        private readonly IVehicleSummaryService _vehicleSummaryService;

        public VehicleChecksController(IVehicleSummaryService vehicleSummaryService)
        {
            _vehicleSummaryService = vehicleSummaryService;
            
        }

        /// <summary>
        /// Gets models for a given make
        /// </summary>
        /// <param name="make"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/vehicle-checks/makes/{make}")]
        public async Task<ActionResult<VehicleSummaryResponse>> GetMakesAsync(string make)
        {
            if (string.IsNullOrWhiteSpace(make))
            {
                return BadRequest("Make cannot be empty");
            }

            VehicleSummaryResponse response;
            try
            {
                response = await _vehicleSummaryService.GetSummaryByMakeAsync(make);

                if (response == null)
                {
                    return NotFound();
                }
            }
            catch (FlurlHttpException e) when (e.Message.Contains("404"))
            {
                return NotFound();
            }

            return response;
        }
    }
}