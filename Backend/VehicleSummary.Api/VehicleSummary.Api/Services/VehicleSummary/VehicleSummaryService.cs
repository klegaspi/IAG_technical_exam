using System;
using System.Threading.Tasks;
using VehicleSummary.Api.Mapping;

namespace VehicleSummary.Api.Services.VehicleSummary
{
    public interface IVehicleSummaryService
    {
        Task<VehicleSummaryResponse> GetSummaryByMakeAsync(string make);
    }
    
    public class VehicleSummaryService : IVehicleSummaryService
    {
        private readonly IVehicleApiModelsService _vehicleApiModelsService;

        public VehicleSummaryService(IVehicleApiModelsService vehicleApiModelsService)
        {
            _vehicleApiModelsService = vehicleApiModelsService;
        }

        public async Task<VehicleSummaryResponse> GetSummaryByMakeAsync(string make)
        {
            if (string.IsNullOrWhiteSpace(make))
            {
                throw new ArgumentNullException(nameof(make));
            }

            var vehicleAPIModelsResponse = await _vehicleApiModelsService.GetConsolidatedVehicleModelsAsync(make);

            return vehicleAPIModelsResponse.ToVehicleSummaryResponse();
        }
    }
}